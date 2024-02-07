using CNSys;
using CNSys.Data;
using EAU.Common;
using EAU.Documents.Domain;
using EAU.KAT.Documents.Domain;
using EAU.Nomenclatures;
using EAU.Nomenclatures.Models;
using EAU.Security;
using EAU.Services.ServiceInstances.Models;
using EAU.Services.ServiceInstances.Repositories;
using EAU.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Clients;
using WAIS.Integration.EPortal.Models;

namespace EAU.Services.ServiceInstances
{
    /// <summary>
    /// Реализация на интерфейс за работа с инстанции на услуги.
    /// </summary>
    public class ServiceInstanceService : IServiceInstanceService
    {
        private readonly IServiceInstanceRepository ServiceInstanceRepository;
        private readonly IDbContextOperationExecutor DBContextOperationExecutor;
        private readonly IDocumentTypes DocumentTypes;
        private readonly IServices Services;
        private readonly IEAUUserAccessor _userAccessor;
        private readonly IWAISIntegrationServiceClientsFactory _waisIntegrationServiceClientsFactory;
        private readonly ILogger<ServiceInstanceService> _logger;

        public ServiceInstanceService(IServiceInstanceRepository serviceInstanceRepository,
            IDbContextOperationExecutor dBContextOperationExecutor,
            IDocumentTypes documentTypes,
            IEAUUserAccessor userAccessor,
            IWAISIntegrationServiceClientsFactory waisIntegrationServiceClientsFactory,
            IServices services,
            ILogger<ServiceInstanceService> logger)
        {
            ServiceInstanceRepository = serviceInstanceRepository;
            Services = services;
            DocumentTypes = documentTypes;
            _userAccessor = userAccessor;
            _waisIntegrationServiceClientsFactory = waisIntegrationServiceClientsFactory;
            DBContextOperationExecutor = dBContextOperationExecutor;
            _logger = logger;
        }

        #region IServiceInstanceService

        public async Task<OperationResult<ServiceInstance>> CreateAsync(ServiceInstanceCreateRequest request, CancellationToken cancellationToken)
        {
            var srvInstance = new ServiceInstance()
            {
                ApplicantID = request.DocumentProcess.ApplicantID,
                CaseFileURI = request.DocumentProcess.CaseFileURI,
                ServiceID = request.DocumentProcess.ServiceID,
                ServiceInstanceDate = DateTime.Now,
                Status = GetStatus(request.ServiceInstance),
                StatusDate = request.ServiceInstance.ReportData,
                AdditionalData = await GetAdditionalDataAsync(request.ServiceInstance, cancellationToken)
            };

            await ServiceInstanceRepository.CreateAsync(srvInstance, cancellationToken);

            return new OperationResult<ServiceInstance>(OperationResultTypes.SuccessfullyCompleted)
            {
                Result = srvInstance
            };
        }

        public Task<OperationResult<ServiceInstance>> UpdateAsync(ServiceInstanceInfo backendServiceInstance, CancellationToken cancellationToken)
        {
            return DBContextOperationExecutor.ExecuteAsync(async (dbcontext, token) =>
            {
                var serviceInstance = (await ServiceInstanceRepository.SearchAsync(new ServiceInstanceSearchCriteria()
                {
                    CaseFileURI = backendServiceInstance.CaseFileURI.ToString(),
                    LoadOption = new ServiceInstanceLoadOption()
                    {
                        LoadWithLock = true
                    }
                }, token)).Single();

                if (serviceInstance.StatusDate < backendServiceInstance.ReportData)
                {
                    serviceInstance.Status = GetStatus(backendServiceInstance);
                    serviceInstance.AdditionalData = await GetAdditionalDataAsync(backendServiceInstance, cancellationToken);
                    serviceInstance.StatusDate = backendServiceInstance.ReportData;

                    await ServiceInstanceRepository.UpdateAsync(serviceInstance, token);
                }

                return new OperationResult<ServiceInstance>(OperationResultTypes.SuccessfullyCompleted)
                {
                    Result = serviceInstance
                };
            }, cancellationToken);
        }

        public async Task<IEnumerable<ServiceInstance>> SearchAsync(ServiceInstanceSearchCriteria criteria, CancellationToken cancellationToken)
        {
            var state = criteria.ExtractState();
            var serviceInstances = await ServiceInstanceRepository.SearchAsync(state, criteria, cancellationToken);
            criteria.Count = state.Count;

            await Services.EnsureLoadedAsync(cancellationToken);

            foreach (var serviceInstance in serviceInstances)
            {
                serviceInstance.NomService = Services.Search().SingleOrDefault(s => s.ServiceID == serviceInstance.ServiceID);
            }

            return serviceInstances;
        }

        public async Task<OperationResult<Stream>> DownloadDocumentContentAsync(long serviceInstanceID, string documentURI, CancellationToken cancellationToken)
        {
            var serviceInstance = (await SearchAsync(new ServiceInstanceSearchCriteria()
            {
                ServiceInstanceIDs = new List<long>() { serviceInstanceID },
                ApplicantID = _userAccessor.User.LocalClientID
            }, cancellationToken)).SingleOrDefault();

            //Нямате достъп до избраната преписка по услуга. Заявлението за услугата е подадено от друг потребител.
            EnsureExistingServiceAndCheckAccess(serviceInstanceID, serviceInstance);

            var caseFile = await _waisIntegrationServiceClientsFactory.GetDocumentServiceClient().GetCaseFileAsync(new WAIS.Integration.EPortal.Models.URI(serviceInstance.CaseFileURI), cancellationToken);

            //В преписката няма документ с въведеното УРИ.
            if (!caseFile.Documents.Any(d => d.DocumentURI.ToString() == documentURI))
                return new OperationResult<Stream>("GL_00023_E", "GL_00023_E");

            var document = await _waisIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(new WAIS.Integration.EPortal.Models.URI(documentURI), cancellationToken);

            return new OperationResult<Stream>(OperationResultTypes.SuccessfullyCompleted)
            {
                Result = document.FileContentStream.Content
            };
        }

        #endregion

        #region Helpers

        private async Task<AdditionalData> GetAdditionalDataAsync(ServiceInstanceInfo serviceInstance, CancellationToken cancellationToken)
        {
            await Services.EnsureLoadedAsync(cancellationToken);
            await DocumentTypes.EnsureLoadedAsync(cancellationToken);

            var service = Services.Search().Single(s => s.SunauServiceUri == serviceInstance.SunauServiceUri);
            var appTypeURI = serviceInstance.CaseFile.Documents.First(d => d.DocumentURI.ToString() == serviceInstance.CaseFileURI.ToString()).DocumentTypeURI;

            var status = GetStatus(serviceInstance);
            AdditionalData additionalData = new AdditionalData();

            additionalData["subStatus"] = serviceInstance.Status.ToString();

            if (serviceInstance.ExecutedStages != null && serviceInstance.ExecutedStages.Count > 0)
            {
                var lastStage = serviceInstance.ExecutedStages.OrderByDescending(st => st.ActualCompletionDate).First();
                additionalData["lastStage"] = lastStage.Name;
                additionalData["lastStageActualCompletionDate"] = lastStage.ActualCompletionDate?.ToString("dd.MM.yyyy");
            }

            if (status == ServiceInstanceStatuses.Completed || status == ServiceInstanceStatuses.Rejected)
            {
                return additionalData;
            }

            var currentStage = serviceInstance.UnexecutedStages.First();

            additionalData["currentStage"] = currentStage.Name;
            additionalData["currentStageActualCompletionDate"] = currentStage.ActualCompletionDate?.ToString("dd.MM.yyyy");

            if (CanWithdrawService(service, serviceInstance, currentStage))
            {
                _logger.LogInformation($"update on caseFile URI: {serviceInstance.CaseFileURI.ToString()}, current stageURI: {currentStage.StageURI}, еnabledStagesForWithdrawService contains current stage: {service.AdditionalConfiguration["еnabledStagesForWithdrawService"].Contains(currentStage.StageURI)}, hasOutstandingConditionsForWithdrawServiceMessage: {serviceInstance.CaseFile.Documents.Any(d => d.DocumentTypeURI == DocumentTypeUris.OutstandingConditionsForWithdrawServiceMessage)} and hasWithdrawServiceDocument: {serviceInstance.CaseFile.Documents.Any(d => d.DocumentTypeURI == DocumentTypeUris.ApplicationForWithdrawService)}");
                
                additionalData["showWithdrawServiceBtn"] = bool.TrueString;
            }

            var application = serviceInstance.CaseFile.Documents.OrderByDescending(d => d.RegistrationTime).First(d => d.DocumentTypeURI == appTypeURI);
            var irregularitiesInstructions = serviceInstance.CaseFile.Documents.OrderByDescending(d => d.RegistrationTime).FirstOrDefault(d => d.DocumentTypeURI == DocumentTypeUris.RemovingIrregularitiesInstructionsUri);

            //Проверяваме дали има "Указания за отстраняване на нередовностите", след което няма потвърждаване на получаването или документ прекратяващ услугата.
            if (irregularitiesInstructions != null &&
                serviceInstance.Status == ServiceInstanceStatus.WaitCorrectionsApplication)
            {
                additionalData["removingIrregularitiesInstructionURI"] = irregularitiesInstructions.DocumentURI.ToString();
            }

            var paymentInstruction = serviceInstance.CaseFile.Documents.OrderByDescending(d => d.RegistrationTime).FirstOrDefault(d => d.DocumentTypeURI == DocumentTypeUris.PaymentInstructions);

            //Проверяваме дали има "Указание за заплащане", след което няма потвърждаване на заплащаненето или документ прекратяващ услугата
            if (paymentInstruction != null &&
                !serviceInstance.CaseFile.Documents.Any(d => (d.DocumentTypeURI == DocumentTypeUris.ReceiptAcknowledgedPaymentForMOI ||
                                              d.DocumentTypeURI == DocumentTypeUris.OutstandingConditionsForStartOfServiceMessage ||
                                              d.DocumentTypeURI == DocumentTypeUris.TerminationOfServiceMessage) &&
                                             d.RegistrationTime > paymentInstruction.RegistrationTime))
            {
                additionalData["paymentInstructionURI"] = paymentInstruction.DocumentURI.ToString();
            }

            var lastDocument = serviceInstance.CaseFile.Documents.OrderByDescending(d => d.RegistrationTime).FirstOrDefault();

            //Проверяваме дали има "Данни за печат на СРМПС", след което няма потвърждаване на заплащаненето или документ прекратяващ услугата
            if (lastDocument != null
                && lastDocument.DocumentTypeURI == DocumentTypeUrisKAT.DataForPrintSRMPS
                && serviceInstance.CaseFile.Documents.Count(x => x.DocumentTypeURI == DocumentTypeUrisKAT.DataForPrintSRMPS) == 1)
            {
                additionalData["additionalApplicationURI"] = lastDocument.DocumentURI.ToString();
            }

            //Проверяваме дали има "Декларация по чл. 141, ал.2 от Закона за движение по пътищата", след което няма потвърждаване на заплащаненето или документ прекратяващ услугата
            if (lastDocument != null && lastDocument.DocumentTypeURI == DocumentTypeUrisKAT.DeclarationForLostSRPPS
                && serviceInstance.CaseFile.Documents.Count(x => x.DocumentTypeURI == DocumentTypeUrisKAT.DeclarationForLostSRPPS) == 1)
            {
                additionalData["additionalApplicationURI"] = lastDocument.DocumentURI.ToString();
            }

            return additionalData;
        }

        private void EnsureExistingServiceAndCheckAccess(long serviceInstanceID, ServiceInstance serviceInstance)
        {
            if (serviceInstance == null)
                throw new NoDataFoundException(serviceInstanceID.ToString(), "ServiceInstance");

            if (serviceInstance.ApplicantID != _userAccessor.User.LocalClientID)
                throw new AccessDeniedException(serviceInstanceID.ToString(), "ServiceInstance", _userAccessor.User.LocalClientID);
        }

        private bool CanWithdrawService(Service service, ServiceInstanceInfo serviceInstance, StageInstanceInfo currentStage)
        {
            if (service.AdditionalConfiguration == null)
            {
                return false;
            }

            if (!service.AdditionalConfiguration.ContainsKey("еnabledStagesForWithdrawService"))
            {
                return false;
            }

            if (!service.AdditionalConfiguration["еnabledStagesForWithdrawService"].Contains(currentStage.StageURI))
            {
                return false;
            }

            if (serviceInstance.CaseFile.Documents == null && serviceInstance.CaseFile.Documents.Count() == 0)
            {
                return false;
            }

            var lastDocumentTypeURI = serviceInstance.CaseFile.Documents.OrderByDescending(d => d.RegistrationTime).First().DocumentTypeURI;

            if (lastDocumentTypeURI == DocumentTypeUris.OutstandingConditionsForWithdrawServiceMessage
                    || (!serviceInstance.CaseFile.Documents.Any(d => d.DocumentTypeURI == DocumentTypeUris.ApplicationForWithdrawService) &&
                        !serviceInstance.CaseFile.Documents.Any(d => d.DocumentTypeURI == DocumentTypeUris.OutstandingConditionsForWithdrawServiceMessage)))
            {
                return true;
            }

            return false;
        }

        private ServiceInstanceStatuses GetStatus(ServiceInstanceInfo serviceInstance)
        {
            switch (serviceInstance.Status)
            {
                case ServiceInstanceStatus.NotCompleted:
                case ServiceInstanceStatus.WaitingResponse:
                case ServiceInstanceStatus.WaitCorrectionsApplication:
                case ServiceInstanceStatus.WaitPayment:
                    return ServiceInstanceStatuses.InProcess;
                case ServiceInstanceStatus.CancelIssuingAdministrativeAct:
                case ServiceInstanceStatus.Cancelled:
                case ServiceInstanceStatus.Termination:
                case ServiceInstanceStatus.OutstandingConditions:
                    return ServiceInstanceStatuses.Rejected;
                case ServiceInstanceStatus.Completed:
                    return ServiceInstanceStatuses.Completed;
                default:
                    throw new ArgumentException(string.Format("Unsaported service instance status {0}.", serviceInstance.Status.ToString()));
            }
        }

        #endregion
    }
}
