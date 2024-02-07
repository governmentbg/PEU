using CNSys;
using CNSys.Xml;
using EAU.Common;
using EAU.Documents.Domain;
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using EAU.Payments.Obligations.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using EAU.Security;
using EAU.Services.DocumentProcesses;
using EAU.Services.ServiceInstances;
using EAU.Services.ServiceInstances.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.EPortal.Clients;
using WAIS.Integration.EPortal.Models;

namespace EAU.Payments.Obligations.SerivceInstances
{
    /// <summary>
    /// Реализация на интерфейс за работа с на данни за задължения от външни системи.
    /// </summary>
    public class ServiceInstanceObligationChannelService : IObligationChannelService
    {
        private readonly IServiceInstanceService ServiceInstanceService;
        private readonly IEAUUserAccessor UserAccessor;
        private readonly IWAISIntegrationServiceClientsFactory WAISIntegrationServiceClientsFactory;
        private readonly IDocumentProcessFormService DocumentProcessFormService;
        private readonly IRegistrationDataRepository RegistrationDataRepository;
        private readonly IOptionsMonitor<GlobalOptions> OptionsMonitor;

        private readonly char _identifierSeparator = '|';

        public ServiceInstanceObligationChannelService(
            IServiceInstanceService serviceInstanceService,
            IEAUUserAccessor userAccessor,
            IOptionsMonitor<GlobalOptions> optionsMonitor,
            IWAISIntegrationServiceClientsFactory wAISIntegrationServiceClientsFactory,
            IDocumentProcessFormService documentProcessFormService,
            IRegistrationDataRepository registrationDataRepository)
        {
            ServiceInstanceService = serviceInstanceService;
            UserAccessor = userAccessor;
            OptionsMonitor = optionsMonitor;
            WAISIntegrationServiceClientsFactory = wAISIntegrationServiceClientsFactory;
            DocumentProcessFormService = documentProcessFormService;
            RegistrationDataRepository = registrationDataRepository;
        }

        public async Task<OperationResult> ProcessObligation(Obligation obligation, long activePaymentRequestID, CancellationToken cancellationToken)
        {
            var documentService = WAISIntegrationServiceClientsFactory.GetDocumentServiceClient();

            var paymentRequest = obligation.PaymentRequests.Single(pr => pr.PaymentRequestID == activePaymentRequestID);

            await documentService.RegisterElectronicPaymentAsync(new Payment()
            {
                Amount = paymentRequest.Amount.Value,
                CaseFileURI = new URI(obligation.AdditionalData["caseFileURI"]),
                Currency = "BGN",
                Invoice = obligation.ObligationIdentifier,
                Description = obligation.PaymentReason,
                PaymentDate = paymentRequest.PayDate.Value,
                ProviderKind = paymentRequest.RegistrationDataType == RegistrationsData.Models.RegistrationDataTypes.ePay ? ProviderKind.EPay : ProviderKind.PEP,
                PaymentInstructionUri = new URI(obligation.AdditionalData["paymentInstructionURI"])
            }, cancellationToken);

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public async Task<OperationResult<List<ObligationSearchResult>>> SearchAsync(ObligationChannelSearchCriteria criteria, CancellationToken cancellationToken)
        {
            long serviceInstanceID;
            string pymentInstructionURI;

            if (!string.IsNullOrEmpty(criteria.PaymentInstructionURI) && criteria.ServiceInstanceID.HasValue)
            {
                serviceInstanceID = criteria.ServiceInstanceID.Value;
                pymentInstructionURI = criteria.PaymentInstructionURI;
            }
            else
            {
                string[] oblIdentifiers = criteria.ObligationIdentifier.Split(_identifierSeparator);

                serviceInstanceID = Convert.ToInt64(oblIdentifiers[1]);
                pymentInstructionURI = oblIdentifiers[0];
            }

            var obligation = await GetObligationAsync(serviceInstanceID, pymentInstructionURI, cancellationToken);
            
            if (obligation != null )
            {
                var result = new List<ObligationSearchResult> { new ObligationSearchResult { Obligations = new List<Obligation>() { obligation } } };

                //Проверката за ExpirationDate я слагаме, защото в случай, че гръмне workflow-a и ослугата не се прекрати когато изтече, ще излезе че имаме задължение.
                if (DateTime.Now > obligation.ExpirationDate.Value)
                {
                    return new OperationResult<List<ObligationSearchResult>>("GL_EXPIRED_OBLIGATION_E", "GL_EXPIRED_OBLIGATION_E") //Срокът за извършване на плащането е изтекъл
                    {
                        Result = result
                    };
                }
                return new OperationResult<List<ObligationSearchResult>>(OperationResultTypes.SuccessfullyCompleted)
                {
                    Result = result
                };
            }
            else
                return new OperationResult<List<ObligationSearchResult>>(OperationResultTypes.SuccessfullyCompleted);

        }

        #region Helpers

        private async Task<Obligation> GetObligationAsync(long serviceInstanceID, string paymentInstructionURI, CancellationToken cancellationToken)
        {
            var serviceInstance = (await ServiceInstanceService.SearchAsync(new ServiceInstanceSearchCriteria()
            {
                ApplicantID = UserAccessor.User.LocalClientID,
                ServiceInstanceIDs = new List<long>() { serviceInstanceID }
            }, cancellationToken)).SingleOrDefault();

            if (serviceInstance == null)
            {
                throw new ArgumentException($"Applicant with UserID{UserAccessor.User.LocalClientID}, is not same applicant started the service with ServiceInstanceID {serviceInstanceID}.");
            }

            var caseFile = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetCaseFileAsync(new URI(serviceInstance.CaseFileURI), cancellationToken);

            if (!caseFile.Documents.Any(d => d.DocumentURI.ToString() == paymentInstructionURI && d.DocumentTypeURI == DocumentTypeUris.PaymentInstructions))
            {
                throw new ArgumentException($"CaseFile {serviceInstance.CaseFileURI}, doesn't contain Payment Instructions with uri {paymentInstructionURI}.");
            }

            var paymentInstruction = await GetPaymentInstructionAsync(paymentInstructionURI, cancellationToken);

            if (paymentInstruction != null)
                return await InitObligation(serviceInstanceID, serviceInstance.ServiceID.Value, serviceInstance.CaseFileURI, paymentInstructionURI, paymentInstruction);
            else
                return null;
        }

        private async Task<PaymentInstructions> GetPaymentInstructionAsync(string paymentURI, CancellationToken cancellationToken)
        {
            var instructionConten = await WAISIntegrationServiceClientsFactory.GetDocumentServiceClient().GetDocumentContentAsync(new URI(paymentURI), cancellationToken);

            if (instructionConten == null || instructionConten.FileContentStream == null)
            {
                return null;
            }

            using (instructionConten.FileContentStream.Content)
            {
                var formXml = DocumentProcessFormService.ParseXmlDocument(instructionConten.FileContentStream.Content);

                return XmlSerializerHelper.DeserializeObject<PaymentInstructions>(formXml.OuterXml);
            }
        }

        private async Task<Obligation> InitObligation(long serviceInstanceID, int serviceID, string caseFileURI, string paymentInstructionURI, PaymentInstructions instruction)
        {
            string obligedPersonName, obligedPersonIdent;
            ObligedPersonIdentTypes obligedPersonIdentType;

            if (instruction.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].ItemPersonBasicData != null)
            {
                var obligedPerson = instruction.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].ItemPersonBasicData;

                obligedPersonName = $"{obligedPerson.Names.First} {obligedPerson.Names.Middle} {obligedPerson.Names.Last}".Length > 26 ?
                    $"{obligedPerson.Names.First} {obligedPerson.Names.Middle} {obligedPerson.Names.Last}".Substring(0, 26) : $"{obligedPerson.Names.First} {obligedPerson.Names.Middle} {obligedPerson.Names.Last}";
                obligedPersonIdent = obligedPerson.Identifier.Item;
                obligedPersonIdentType = obligedPerson.Identifier.ItemElementName == PersonIdentifierChoiceType.EGN ? ObligedPersonIdentTypes.EGN : ObligedPersonIdentTypes.LNC;
            }
            else
            {
                var entity = instruction.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].ItemEntityBasicData;

                if (entity.Name.Length > 26)
                    obligedPersonName = entity.Name.Substring(0, 26);
                else
                    obligedPersonName = entity.Name;

                obligedPersonIdent = entity.Identifier;
                obligedPersonIdentType = ObligedPersonIdentTypes.BULSTAT;
            }

            var obligationDate = (new URI(paymentInstructionURI)).ReceiptOrSigningDate.Value;

            string deadlineString = new Regex(@"(\d*)D").Match(instruction.DeadlineForPayment).Groups[1].Value;
            int deadline = 7 + int.Parse(deadlineString);
            DateTime? expirationDate = instruction.DocumentReceiptOrSigningDate.GetValueOrDefault().AddDays(deadline);

            var registrationData = (await RegistrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria() { Type = RegistrationDataTypes.PepOfDaeu, IBAN = instruction.IBAN })).SingleOrDefault();

            var obligation = new Obligation()
            {
                Status = ObligationStatuses.Pending,
                ExpirationDate = expirationDate.Value.RoundToEndOfDay(),
                //Имаме валидация за ипей, която позволява само [a-zA-Zа-яА-Я0-9- ,.] следните символи за име на задължено лице
                ObligedPersonName = new Regex("[^a-zA-Zа-яА-Я0-9- ,.]*").Replace(obligedPersonName, ""),
                ObligedPersonIdent = obligedPersonIdent,
                ObligedPersonIdentType = obligedPersonIdentType,
                ObligationIdentifier = $"{paymentInstructionURI}{_identifierSeparator}{serviceInstanceID}",
                Amount = Convert.ToDecimal(instruction.Amount),
                PepCin = registrationData?.Cin,
                DiscountAmount = Convert.ToDecimal(instruction.Amount),
                ObligationDate = obligationDate,
                BankName = instruction.BankName,
                Bic = instruction.BIC,
                Iban = instruction.IBAN,
                PaymentReason = string.Format(OptionsMonitor.CurrentValue.GL_SERVICE_INSTANCE_PAYMENT_REASON, paymentInstructionURI, caseFileURI),
                ServiceInstanceID = serviceInstanceID,
                ServiceID = serviceID,
                Type = ObligationTypes.ServiceInstance
            };

            obligation.AdditionalData = new Utilities.AdditionalData();
            obligation.AdditionalData["serviceInstanceID"] = serviceInstanceID.ToString();
            obligation.AdditionalData["caseFileURI"] = caseFileURI.ToString();
            obligation.AdditionalData["paymentInstructionURI"] = paymentInstructionURI;

            return obligation;
        }

        #endregion
    }
}
