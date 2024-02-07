using CNSys;
using CNSys.Data;
using EAU.Documents.Domain.Validations;
using EAU.Payments.Obligations.Models;
using EAU.Payments.Obligations.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.Obligations
{
    /// <summary>
    /// Реализация на интерфейс за работа със задължения.
    /// </summary>
    public class ObligationService : IObligationService
    {
        private readonly IObligationChannelProvider _provider;
        private readonly IObligationRepository _obligationRepository;
        private readonly IPaymentRequestService _paymentRequestService;
        private readonly IStringLocalizer _localizer;

        public ObligationService(IObligationChannelProvider provider, IObligationRepository obligationRepository, ILogger<ObligationService> logger, 
            IPaymentRequestService paymentRequestService, IStringLocalizer localizer)
        {
            _provider = provider;
            _obligationRepository = obligationRepository;
            _paymentRequestService = paymentRequestService;
            _localizer = localizer;
        }

        public async Task<OperationResult<Obligation>> CreateAsync(ObligationRequest obligationRequest, CancellationToken cancellationToken)
        {
            //проверка да не позволява повече от едно неизтекло задължение.
            var existingObligation = (await _obligationRepository.SearchAsync(PagedDataState.CreateMaxPagedDataState(),
                new ObligationRepositorySearchCriteria()
                {
                    ApplicantID = obligationRequest.ApplicantID,
                    IsApplicantAnonimous = obligationRequest.ApplicantID == null,
                    ObligationIdentifiersSearchCriteria = new List<ObligationIdentifiersSearchCriteria>(){
                        new ObligationIdentifiersSearchCriteria()
                        {
                            ObligationIdentifier = obligationRequest.ObligationIdentifier,
                            ObligationDate = obligationRequest.ObligationDate
                        }},
                    IsActive = true,
                    Type = obligationRequest.Type
                }, cancellationToken)).SingleOrDefault();

            //За един акт/услуга може да има само един неизтекъл запис.
            if (existingObligation != null)
            {
                return new OperationResult<Obligation>(OperationResultTypes.SuccessfullyCompleted)
                {
                    Result = existingObligation
                };
            }
            else
            {
                var obligationChannelService = _provider.GetObligationChannelService(obligationRequest.Type.Value);

                var chanelCriteria = obligationRequest.ObligationSearchCriteria != null ? ToObligationChannelSearchCriteria(obligationRequest.ObligationSearchCriteria)
                    : new ObligationChannelSearchCriteria { ObligationIdentifier = obligationRequest.ObligationIdentifier };

                //Изчитане на услуги от външна система (АНД / Услуги)
                var outerObligationRes = await obligationChannelService.SearchAsync(chanelCriteria, cancellationToken);

                if (!outerObligationRes.IsSuccessfullyCompleted)
                {
                    return new OperationResult<Obligation>(outerObligationRes.Errors);
                }

                List<Obligation> items = new List<Obligation>();
                foreach (var groupSet in outerObligationRes.Result)
                {
                    var found = groupSet.Obligations.Where(t => string.Compare(t.ObligationIdentifier, obligationRequest.ObligationIdentifier) == 0).SingleOrDefault();
                    if (found != null)
                        items.Add(found);
                }

                if (items.Count > 1)
                    throw new Exception(string.Format("Obligations with same ObligationIdentifier found {0}", obligationRequest.ObligationIdentifier));

                var outerObligation = items.SingleOrDefault();

                if (outerObligation != null)
                {
                    outerObligation.Status = ObligationStatuses.InProcess;
                    outerObligation.ApplicantID = obligationRequest.ApplicantID;
                    outerObligation.ObligedPersonName = obligationRequest.ObligedPersonName;
                    outerObligation.ObligedPersonIdent = obligationRequest.ObligedPersonIdent;
                    outerObligation.ObligedPersonIdentType = obligationRequest.ObligedPersonIdentType;

                    if (string.IsNullOrEmpty(outerObligation.ObligedPersonIdent) && outerObligation.ObligedPersonIdentType == null)
                    {                        
                        var validator = new CnsysValidatorBase();
                        if (string.IsNullOrEmpty(chanelCriteria.Uic))
                        {
                            if (validator.ValidateEGN(chanelCriteria.ObligedPersonIdent))
                            {
                                outerObligation.ObligedPersonIdentType = ObligedPersonIdentTypes.EGN;
                                outerObligation.ObligedPersonIdent = chanelCriteria.ObligedPersonIdent;
                            }
                            else if (validator.ValidateLNCH(chanelCriteria.ObligedPersonIdent))
                            {
                                outerObligation.ObligedPersonIdentType = ObligedPersonIdentTypes.LNC;
                                outerObligation.ObligedPersonIdent = chanelCriteria.ObligedPersonIdent;
                            }
                        }
                        else
                        {
                            if (validator.ValidateUICBulstat(chanelCriteria.Uic))
                            {
                                outerObligation.ObligedPersonIdentType = ObligedPersonIdentTypes.BULSTAT;
                                outerObligation.ObligedPersonIdent = chanelCriteria.Uic;
                            }
                        }
                    }

                    // Проверка за задължение по КАТ АНД А-Х51 (търсене по номер/серия на Фиш/НП/Споразумение + дължима сума)
                    var additionalDataObligedPersonIdentType = outerObligation.AdditionalData.ContainsKey("obligedPersonIdentType") ?
                            outerObligation.AdditionalData["obligedPersonIdentType"] : null;

                    if (additionalDataObligedPersonIdentType != null &&
                        obligationRequest.Type == ObligationTypes.AND && obligationRequest.ObligationSearchCriteria.DocumentType != null &&
                        !string.IsNullOrEmpty(obligationRequest.ObligationSearchCriteria.DocumentNumber) && obligationRequest.ObligationSearchCriteria.InitialAmount != null && 
                        ((obligationRequest.ObligationSearchCriteria.DocumentType == KATDocumentTypes.TICKET &&
                            !string.IsNullOrEmpty(obligationRequest.ObligationSearchCriteria.DocumentSeries))
                        ||
                        (obligationRequest.ObligationSearchCriteria.DocumentType != KATDocumentTypes.TICKET &&
                            string.IsNullOrEmpty(obligationRequest.ObligationSearchCriteria.DocumentSeries))
                        ))
                    {
                        var additionalDataObligedPersonIdent = outerObligation.AdditionalData.ContainsKey("obligedPersonIdent") ?
                            outerObligation.AdditionalData["obligedPersonIdent"] : null;

                        if (additionalDataObligedPersonIdent != null && additionalDataObligedPersonIdent.Length >= 6 && outerObligation.ObligedPersonIdent != null && 
                            outerObligation.ObligedPersonIdent.Length >= 6)
                        {
                            var additionalDataDocumentType = outerObligation.AdditionalData.ContainsKey("documentType") ?
                                outerObligation.AdditionalData["documentType"] : "";
                            
                            var additionalDataDocumentNumber = outerObligation.AdditionalData.ContainsKey("documentNumber") ?
                                outerObligation.AdditionalData["documentNumber"] : "";

                            var documentTypeValue = "";
                            if (!string.IsNullOrEmpty(additionalDataDocumentType))
                            {                                                                
                                // Get the DocumentType resource description/value
                                documentTypeValue = _localizer["GL_" + additionalDataDocumentType + "_L"].Value;
                            }

                            // Replace documentType & docNumber in General Error message (GL_OBLIGOR_ID_MISMATCH_E)
                            var errorMsg = _localizer["GL_OBLIGOR_ID_MISMATCH_E"].Value.Replace("{TYPE}", documentTypeValue).Replace("{NUMBER}", additionalDataDocumentNumber);

                            if (additionalDataObligedPersonIdentType == "person")
                            {
                                // Сверяване на първите 6 цифри на ЕГН
                                if (string.Compare(outerObligation.ObligedPersonIdent.Substring(0, 6), additionalDataObligedPersonIdent.Substring(0, 6)) != 0)
                                {
                                    // Идентификаторът на задълженото лице не съответства на поддържания в АИС на МВР идентификатор за задължено лице по {TYPE} : {NUMBER}.
                                    return new OperationResult<Obligation>(errorMsg, errorMsg);
                                }
                            }
                            else if (additionalDataObligedPersonIdentType == "company")
                            {
                                // Сверяване на ЕИК (всички 9 цифри)
                                if (string.Compare(outerObligation.ObligedPersonIdent, additionalDataObligedPersonIdent) != 0)
                                {
                                    // Идентификаторът на задълженото лице не съответства на поддържания в АИС на МВР идентификатор за задължено лице по {TYPE} : {NUMBER}.
                                    return new OperationResult<Obligation>(errorMsg, errorMsg);
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(string.Format("outerObligation.AdditionalData[\"obligedPersonIdent\"] or outerObligation.ObligedPersonIdent are either null or are less than 6 characters long. " +
                                "Error occurred for the following Obligation Identifier: {0}", obligationRequest.ObligationIdentifier));
                        }
                    }
                                        
                    await _obligationRepository.CreateAsync(outerObligation, cancellationToken);

                    return new OperationResult<Obligation>(OperationResultTypes.SuccessfullyCompleted) { Result = outerObligation };
                }
                else
                {
                    return new OperationResult<Obligation>("GL_NO_OBLIGATION_E", "GL_NO_OBLIGATION_E"); //Задължението не съществува
                }
            }
        }

        public async Task ProcessObligation(long obligationID, long paymentRequestID, CancellationToken cancellationToken)
        {
            var obligation = (await _obligationRepository.SearchAsync(new ObligationRepositorySearchCriteria()
            {
                ObligationIDs = new List<long>() { obligationID },
                LoadOption = new ObligationLoadOption()
                {
                    LoadPaymentRequests = true
                }
            })).Single();

            var oblChannelService = _provider.GetObligationChannelService(obligation.Type.Value);
            if (obligation.Status == ObligationStatuses.Paid)
            {
                var result = await oblChannelService.ProcessObligation(obligation, paymentRequestID, cancellationToken);
                if (result.IsSuccessfullyCompleted)
                {
                    obligation.Status = ObligationStatuses.Processed;
                    await _obligationRepository.UpdateAsync(obligation);
                }
                else
                {
                    if (result.Errors?.Count == 1 && result.Errors.Single().Code == "INVALID_PAYMENT")
                    {
                        // special case for INVALID_PAYMENT
                        var paymentRequest = obligation.PaymentRequests.Single(t => t.PaymentRequestID == paymentRequestID);
                        await _paymentRequestService.PaymentDuplicate(paymentRequest, cancellationToken);
                    }
                    else
                    {
                        throw new NotSupportedException($"Unable to ProcessObligation: obligationID: {obligationID}; paymentRequestID: {paymentRequestID}; Errors: {string.Join(";", result.Errors)};");
                    }                    
                }
            }
            else if (obligation.Status == ObligationStatuses.Processed)
            {

            }
            else
            {
                throw new NotSupportedException("Unable to ProcessObligation");
            }
        }

        public async Task<OperationResult<IEnumerable<ObligationSearchResult>>> SearchAsync(PagedDataState state, ObligationSearchCriteria criteria, CancellationToken cancellationToken)
        {
            IEnumerable<ObligationSearchResult> result;

            if (criteria.Mode == ObligationSearchModes.MyPayments)
            {
                var myPaymentsObligations = new ObligationSearchResult();

                //Моите плащания.
                var inerCriteria = new ObligationRepositorySearchCriteria
                {
                    Statuses = new List<ObligationStatuses>() { ObligationStatuses.Paid, ObligationStatuses.Processed },
                    ApplicantID = criteria.ApplicantID,
                    ServiceInsanceID = criteria.ServiceInstanceID,
                    LoadOption = new ObligationLoadOption()
                    {
                        LoadPaymentRequests = true
                    }
                };
                var createdObligations = await _obligationRepository.SearchAsync(state, inerCriteria, cancellationToken);

                myPaymentsObligations.Obligations = createdObligations.ToList();
                result = new List<ObligationSearchResult> { myPaymentsObligations };
            }
            else if (criteria.Mode == ObligationSearchModes.AND
                || criteria.Mode == ObligationSearchModes.ServiceInstances)
            {
                //АНД/Инстанции на услуги плащания
                var channelCriteria = ToObligationChannelSearchCriteria(criteria);
                var obligationChannelService = _provider.GetObligationChannelService(criteria.Type.Value);

                //Изчитане на услуги от външна система (АНД / Услуги)
                var outerObligationsRes = await obligationChannelService.SearchAsync(channelCriteria, cancellationToken);

                if (!outerObligationsRes.IsSuccessfullyCompleted)
                {
                    return new OperationResult<IEnumerable<ObligationSearchResult>>(outerObligationsRes.Errors);
                }

                // add local obligations
                foreach (var obligationsGroup in outerObligationsRes.Result)
                {
                    obligationsGroup.Obligations = await MergeOuterAndLocalDbObligationsAsync(criteria, obligationsGroup.Obligations.ToList(), cancellationToken);
                }

                result = outerObligationsRes.Result;
            }
            else
            {
                throw new NotSupportedException("Invalid input mode");
            }

            return new OperationResult<IEnumerable<ObligationSearchResult>>(OperationResultTypes.SuccessfullyCompleted) { Result = result };
        }

        private async Task<List<Obligation>> MergeOuterAndLocalDbObligationsAsync(ObligationSearchCriteria criteria, List<Obligation> outerObligations, CancellationToken cancellationToken)
        {
            List<Obligation> localDbObligations = null;
            if (outerObligations != null && outerObligations.Any())
            {
                var repositoryCriteria = new ObligationRepositorySearchCriteria()
                {
                    ApplicantID = criteria.ApplicantID,
                    IsApplicantAnonimous = criteria.ApplicantID == null,
                    ObligationIdentifiersSearchCriteria = outerObligations.Select(t =>
                            new ObligationIdentifiersSearchCriteria()
                            {
                                ObligationIdentifier = t.ObligationIdentifier,
                                ObligationDate = t.ObligationDate
                            }).ToList(),
                    Type = criteria.Type,
                    IsActive = true,
                    LoadOption = new ObligationLoadOption()
                    {
                        LoadPaymentRequests = true
                    }
                };

                //Изчитане на вече създадени задължения
                localDbObligations = (await _obligationRepository.SearchAsync(repositoryCriteria, cancellationToken))?.ToList();
            }
            //Ако има създадени задължения изчитаме създадени заявки за плащане.
            if (localDbObligations != null && localDbObligations.Any())
            {
                var newObligations = outerObligations.Where(obl => !localDbObligations.Any(cOlb => cOlb.ObligationIdentifier == obl.ObligationIdentifier));

                var result = new List<Obligation>();
                result.AddRange(localDbObligations);
                result.AddRange(newObligations);
                return result;
            }
            else
            {
                return outerObligations;
            }
        }

        private ObligationChannelSearchCriteria ToObligationChannelSearchCriteria(ObligationSearchCriteria criteria)
        {
            return new ObligationChannelSearchCriteria
            {
                ObligedPersonIdent = criteria.ObligedPersonIdent,
                DrivingLicenceNumber = criteria.DrivingLicenceNumber,
                ServiceInstanceID = criteria.ServiceInstanceID,
                PaymentInstructionURI = criteria.PaymentInstructionURI,
                PersonalDocumentNumber = criteria.PersonalDocumentNumber,
                Uic = criteria.Uic,
                ForeignVehicleNumber = criteria.ForeignVehicleNumber,
                ANDSourceId = criteria.ANDSourceId,
                DocumentType = criteria.DocumentType,
                DocumentSeries = criteria.DocumentSeries,
                DocumentNumber = criteria.DocumentNumber,
                InitialAmount = criteria.InitialAmount
            };
        }
    }
}
