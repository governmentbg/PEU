using CNSys;
using EAU.Common;
using EAU.Nomenclatures;
using EAU.Payments.Obligations.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using EAU.Security;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.Models;
using WAIS.Integration.MOI.Core.BDS.AND.Models;
using WAIS.Integration.MOI.KAT.AND;
using WAIS.Integration.MOI.KAT.AND.Models;

namespace EAU.Payments.Obligations.AND
{
    public class AndObligationChannelService : IObligationChannelService
    {
        private readonly IEAUUserAccessor _eAUUserAccessor;
        private readonly IOptionsMonitor<GlobalOptions> _optionsMonitor;
        private readonly IServices _services;
        private readonly IANDServicesClientFactory _iANDServicesClientFactory;
        private readonly IRegistrationDataRepository _registrationDataRepository;
        private readonly char _identifierSeparator = '|';

        private readonly static int _externalSystemEPayId = 41;
        private readonly static int _externalSystemPEPId = 42;
        private readonly static string _externalSourceSystemAISAND = "АИС АНД";
        private readonly static string _payerTypePhysical = "P";
        private readonly static string _payerTypeLegal = "L";

        public AndObligationChannelService(IEAUUserAccessor eAUUserAccessor,
            IOptionsMonitor<GlobalOptions> optionsMonitor,
            IServices services,
            IANDServicesClientFactory iANDServicesClientFactory,
            IRegistrationDataRepository registrationDataRepository)
        {
            _eAUUserAccessor = eAUUserAccessor;
            _optionsMonitor = optionsMonitor;
            _services = services;
            _iANDServicesClientFactory = iANDServicesClientFactory;
            _registrationDataRepository = registrationDataRepository;
        }

        public async Task<OperationResult> ProcessObligation(Obligation obligation, long activePaymentRequestID, CancellationToken cancellationToken)
        {
            var paymentRequest = obligation.PaymentRequests.Single(t => t.PaymentRequestID == activePaymentRequestID);

            // към АИС АНД за transactionNum слагаме основанието за плащане, което е било върнато от тях при създаването на obligation-а
            var transactionNum = obligation.PaymentReason;

            var documentIdentifier = obligation.AdditionalData.ContainsKey("documentIdentifier") ? obligation.AdditionalData["documentIdentifier"] : null;

            //TODO Да се реши в периода на миграция как ще се подадат старите Obligation-и, които нямат documentIdentifier!

            if (!string.IsNullOrEmpty(documentIdentifier))
            {
                if (obligation.ANDSourceId == ANDSourceIds.KAT)
                {
                    var andPaymentNotifRequest = new WAIS.Integration.MOI.KAT.AND.Models.PaymentNotificationPEAURequest
                    {
                        documentIdentifier = long.Parse(documentIdentifier),
                        documentType = obligation.AdditionalData.ContainsKey("documentType") ? obligation.AdditionalData["documentType"] : null,
                        paymentAmount = (double)paymentRequest.Amount,
                        paymentDate = paymentRequest.PayDate?.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        payerPin = obligation.ObligedPersonIdent,
                        payerType = obligation.ObligedPersonIdentType == ObligedPersonIdentTypes.BULSTAT ? _payerTypeLegal : _payerTypePhysical,
                        sourceSystem = _externalSourceSystemAISAND,
                        externalSystem = paymentRequest.RegistrationDataType == RegistrationDataTypes.ePay ? _externalSystemEPayId : _externalSystemPEPId,
                        transactionNumber = transactionNum,
                        username = obligation.ObligedPersonName,
                    };

                    var andResponse = await _iANDServicesClientFactory.GetANDServicesClient().SendPaymentNotificationPEAUAsync(andPaymentNotifRequest, cancellationToken);

                    if (!andResponse.IsSuccessfullyCompleted)
                    {
                        return new OperationResult<List<Obligation>>(ExtractErrors(andResponse));
                    }
                }
                else
                {
                    var nrbldAndPaymentNotifRequest = new PaymentNotificationPEAUReques
                    {
                        documentIdentifier = long.Parse(documentIdentifier),
                        documentType = obligation.AdditionalData.ContainsKey("documentType") ? obligation.AdditionalData["documentType"] : null,
                        paymentAmount = (double)paymentRequest.Amount,
                        paymentDate = paymentRequest.PayDate?.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        payerPin = obligation.ObligedPersonIdent,
                        payerType = obligation.ObligedPersonIdentType == ObligedPersonIdentTypes.BULSTAT ? _payerTypeLegal : _payerTypePhysical,
                        sourceSystem = _externalSourceSystemAISAND,
                        externalSystem = paymentRequest.RegistrationDataType == RegistrationDataTypes.ePay ? _externalSystemEPayId : _externalSystemPEPId,
                        transactionNumber = transactionNum,
                        username = obligation.ObligedPersonName,
                    };

                    var nrbldAndResponse = await _iANDServicesClientFactory.GetANDServicesClient().SendNRBLDPaymentNotificationPEAUAsync(nrbldAndPaymentNotifRequest, cancellationToken);

                    if (!nrbldAndResponse.IsSuccessfullyCompleted)
                    {
                        return new OperationResult<List<Obligation>>(ExtractErrors(nrbldAndResponse));
                    }
                }
            }
            else
            {
                var andResponse = await _iANDServicesClientFactory.GetANDServicesClient().SendPaymentNotificationAsync(new PaymentNotificationRequest()
                {
                    documentNumber = obligation.AdditionalData.ContainsKey("documentNumber") ? obligation.AdditionalData["documentNumber"] : null,
                    documentSeries = obligation.AdditionalData.ContainsKey("documentSeries") ? obligation.AdditionalData["documentSeries"] : null,
                    documentType = obligation.AdditionalData.ContainsKey("documentType") ? obligation.AdditionalData["documentType"] : null,
                    paymentAmount = (double)paymentRequest.Amount,
                    paymentDate = paymentRequest.PayDate?.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    transactionNumber = transactionNum,
                    username = obligation.ObligedPersonName
                }, cancellationToken);

                if (!andResponse.IsSuccessfullyCompleted)
                {
                    return new OperationResult(ExtractErrors(andResponse));
                }
            }

            return new OperationResult(OperationResultTypes.SuccessfullyCompleted);
        }

        public async Task<OperationResult<List<ObligationSearchResult>>> SearchAsync(ObligationChannelSearchCriteria criteria, CancellationToken cancellationToken)
        {
            var operResult = new OperationResult<List<ObligationSearchResult>>(OperationResultTypes.SuccessfullyCompleted) { Result = new List<ObligationSearchResult>() };

            if (!string.IsNullOrEmpty(criteria.ObligedPersonIdent))
            {
                if (!string.IsNullOrEmpty(criteria.DrivingLicenceNumber))
                {
                    if (criteria.ANDSourceId == null || criteria.ANDSourceId == ANDSourceIds.KAT)
                    {
                        operResult.Result.Add(await GetObligationDocumentsByLicenceNumAsync(criteria.ObligedPersonIdent, criteria.DrivingLicenceNumber, cancellationToken));
                    }
                    if (criteria.ANDSourceId == null || criteria.ANDSourceId == ANDSourceIds.BDS)
                    {
                        operResult.Result.Add(await GetNRBLDObligationDocumentsByLicenceNumAsync(criteria.ObligedPersonIdent, criteria.DrivingLicenceNumber, cancellationToken));
                    }
                }
                else if (string.IsNullOrEmpty(criteria.Uic) && !string.IsNullOrEmpty(criteria.PersonalDocumentNumber))
                {
                    if (criteria.ANDSourceId == null || criteria.ANDSourceId == ANDSourceIds.KAT)
                    {
                        operResult.Result.Add(await GetAllObligationDocumentsByEgnAndDocNumberAsync(criteria.ObligedPersonIdent, criteria.PersonalDocumentNumber, cancellationToken));
                    }
                    if (criteria.ANDSourceId == null || criteria.ANDSourceId == ANDSourceIds.BDS)
                    {
                        operResult.Result.Add(await GetAllNRBLDObligationDocumentsByEgnAndDocNumberAsync(criteria.ObligedPersonIdent, criteria.PersonalDocumentNumber, cancellationToken));
                    }
                }
                else if (!string.IsNullOrEmpty(criteria.Uic) && !string.IsNullOrEmpty(criteria.PersonalDocumentNumber))
                {
                    operResult.Result.Add(await GetAllObligationDocumentsByEikAsync(criteria.ObligedPersonIdent, criteria.PersonalDocumentNumber, criteria.Uic, cancellationToken));
                }
                else if (!string.IsNullOrEmpty(criteria.ForeignVehicleNumber))
                {
                    operResult.Result.Add(await GetAllObligationDocumentsByForeignVehicleAsync(criteria.ObligedPersonIdent, criteria.ForeignVehicleNumber, cancellationToken));
                }
            }
            else if (!string.IsNullOrEmpty(criteria.ObligationIdentifier))
            {
                throw new NotSupportedException($"{nameof(AndObligationChannelService)} cannot search by ObligationIdentifier!");
            }
            else if (criteria.DocumentType != null && criteria.DocumentType == KATDocumentTypes.TICKET && !string.IsNullOrEmpty(criteria.DocumentSeries) &&
                !string.IsNullOrEmpty(criteria.DocumentNumber) && criteria.InitialAmount != null)
            {
                operResult.Result.Add(await GetObligationDocumentsByDocDetailsAsync(criteria.DocumentType.Value.ToString(), criteria.DocumentSeries, criteria.DocumentNumber, criteria.InitialAmount.Value, cancellationToken));
            }
            else if (criteria.DocumentType != null && criteria.DocumentType != KATDocumentTypes.TICKET && string.IsNullOrEmpty(criteria.DocumentSeries) && 
                !string.IsNullOrEmpty(criteria.DocumentNumber) && criteria.InitialAmount != null)
            {
                operResult.Result.Add(await GetObligationDocumentsByDocDetailsAsync(criteria.DocumentType.Value.ToString(), null, criteria.DocumentNumber, criteria.InitialAmount.Value, cancellationToken));
            }

            return operResult;
        }

        #region Helpers

        private async Task<List<Obligation>> MapAndObligation(ServedNotServedObligationDocument[] oblDocuments, ANDSourceIds sourceId, string customerType, string userPid, int? applicantID)
        {
            var service = _services.Search().Single(s => string.Compare(s.SunauServiceUri, _optionsMonitor.CurrentValue.AND_SERVICE_SUNAU_ID) == 0);

            List<Obligation> result = new List<Obligation>();

            foreach (var oblDocument in oblDocuments)
            {
                var addData = new Utilities.AdditionalData();
                if (oblDocument.isServed)
                    addData["isServed"] = oblDocument.isServed.ToString();

                addData["discount"] = oblDocument.discount.ToString();
                addData["documentType"] = oblDocument.documentType;
                addData["documentSeries"] = oblDocument.documentSeries;
                addData["documentNumber"] = oblDocument.documentNumber;
                addData["documentIdentifier"] = oblDocument.documentIdentifier;
                addData["amount"] = oblDocument.initialAmount.ToString();
                addData["issueDate"] = oblDocument.issueDate;
                addData["obligedPersonIdent"] = userPid;
                addData["obligedPersonIdentType"] = customerType;
                addData["vehicleNumber"] = oblDocument.vehicleNumber;
                addData["breachDate"] = oblDocument.breachDate;
                addData["breachOfOrder"] = oblDocument.breachOfOrder;

                var registrationData = (await _registrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria() { Type = RegistrationDataTypes.PepOfDaeu, IBAN = oblDocument.iban })).SingleOrDefault();

                var obligation = new Obligation()
                {
                    PepCin = registrationData?.Cin,
                    DiscountAmount = (decimal?)oblDocument.totalAmount,
                    Amount = (decimal?)oblDocument.initialAmount,
                    BankName = oblDocument.bankName,
                    Iban = oblDocument.iban,
                    Bic = oblDocument.bic,
                    PaymentReason = oblDocument.paymentReason,
                    ObligationDate = DateTime.Now.TrimDateTimeToDate(),
                    Type = ObligationTypes.AND,
                    Status = ObligationStatuses.Pending,
                    ExpirationDate = DateTime.Now.RoundToEndOfDay(),
                    ServiceID = service.ServiceID,
                    ApplicantID = applicantID,

                    ObligationIdentifier = $"{sourceId.ToString()}{_identifierSeparator}{oblDocument.documentType}{_identifierSeparator}{oblDocument.documentIdentifier}",
                    AdditionalData = addData,
                    ANDSourceId = sourceId
                };

                result.Add(obligation);
            }

            return result;
        }

        private async Task<List<Obligation>> MapNRBLDAndObligation(PersonDocumentsInfoByEgnAndDocNumberResponseObligationDocument[] oblDocuments,
            ANDSourceIds sourceId, string customerType, string userPid, int? applicantID)
        {
            var service = _services.Search().Single(s => string.Compare(s.SunauServiceUri, _optionsMonitor.CurrentValue.AND_SERVICE_SUNAU_ID) == 0);

            List<Obligation> result = new List<Obligation>();

            foreach (var oblDocument in oblDocuments)
            {
                var addData = new Utilities.AdditionalData();
                if (!string.IsNullOrEmpty(oblDocument.isServed) && oblDocument.isServed.ToUpper() == "TRUE")
                {
                    addData["isServed"] = oblDocument.isServed;
                }

                addData["discount"] = oblDocument.discount.ToString();
                addData["documentType"] = oblDocument.documentType;
                addData["documentSeries"] = oblDocument.documentSeries;
                addData["documentNumber"] = oblDocument.documentNumber;
                addData["documentIdentifier"] = oblDocument.documentIdentifier;
                addData["amount"] = oblDocument.initialAmount.ToString();
                addData["issueDate"] = oblDocument.issueDate;
                addData["obligedPersonIdent"] = userPid;
                addData["obligedPersonIdentType"] = customerType;
                addData["vehicleNumber"] = oblDocument.vehicleNumber;
                addData["breachDate"] = oblDocument.breachDate;
                addData["breachOfOrder"] = oblDocument.breachOfOrder;

                var registrationData = (await _registrationDataRepository.SearchAsync(new RegistrationDataSearchCriteria()
                {
                    Type = RegistrationDataTypes.PepOfDaeu,
                    IBAN = oblDocument.iban
                })).SingleOrDefault();

                var obligation = new Obligation()
                {
                    PepCin = registrationData?.Cin,
                    DiscountAmount = (decimal?)oblDocument.totalAmount,
                    Amount = (decimal?)oblDocument.initialAmount,
                    BankName = oblDocument.bankName,
                    Iban = oblDocument.iban,
                    Bic = oblDocument.bic,
                    PaymentReason = oblDocument.paymentReason,
                    ObligationDate = DateTime.Now.TrimDateTimeToDate(),
                    Type = ObligationTypes.AND,
                    Status = ObligationStatuses.Pending,
                    ExpirationDate = DateTime.Now.RoundToEndOfDay(),
                    ServiceID = service.ServiceID,
                    ApplicantID = applicantID,

                    ObligationIdentifier = $"{sourceId.ToString()}{_identifierSeparator}{oblDocument.documentType}{_identifierSeparator}{oblDocument.documentIdentifier}",
                    AdditionalData = addData,
                    ANDSourceId = sourceId
                };

                result.Add(obligation);
            }

            return result;
        }

        private Task<ObligationSearchResult> GetObligationDocumentsByLicenceNumAsync(string obligedPersonIdent, string licenceNum, CancellationToken cancellationToken)
        {
            return CallANDServiceAsync(ANDSourceIds.KAT, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllObligationDocumentsByLicenceNumAsync(new ObligationDocumentsByLicenceNumRequest()
                {
                    egn = obligedPersonIdent,
                    licenceNum = licenceNum
                }, token);

            }, cancellationToken);
        }

        private Task<ObligationSearchResult> GetNRBLDObligationDocumentsByLicenceNumAsync(string obligedPersonIdent, string licenceNum, CancellationToken cancellationToken)
        {
            return CallNRBLDANDServiceAsync(ANDSourceIds.BDS, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllNRBLDObligationDocumentsByPersonAsync(_eAUUserAccessor?.RemoteIpAddress?.ToString(),
                    null, obligedPersonIdent, licenceNum, token);

            }, cancellationToken);
        }

        private Task<ObligationSearchResult> GetAllObligationDocumentsByEgnAndDocNumberAsync(string obligedPersonIdent, string personalDocNumber, CancellationToken cancellationToken)
        {
            return CallANDServiceAsync(ANDSourceIds.KAT, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllObligationDocumentsByEgnAndDocNumberAsync(new ObligationDocumentsByEgnAndDocNumberRequest
                {
                    egn = obligedPersonIdent,
                    docNumber = personalDocNumber
                }, token);

            }, cancellationToken);
        }

        private Task<ObligationSearchResult> GetAllNRBLDObligationDocumentsByEgnAndDocNumberAsync(string obligedPersonIdent, string personalDocNumber, CancellationToken cancellationToken)
        {
            return CallNRBLDANDServiceAsync(ANDSourceIds.BDS, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllNRBLDObligationDocumentsByPersonAsync(_eAUUserAccessor?.RemoteIpAddress?.ToString(),
                    null, obligedPersonIdent, personalDocNumber, token);
            }, cancellationToken);
        }

        private Task<ObligationSearchResult> GetAllObligationDocumentsByForeignVehicleAsync(string obligedPersonIdent, string vehicleNumber, CancellationToken cancellationToken)
        {
            return CallANDServiceAsync(ANDSourceIds.KAT, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllObligationDocumentsByForeignVehicleAsync(new ObligationDocumentsByVehicleNumRequest
                {
                    egn = obligedPersonIdent,
                    vehicleNum = vehicleNumber,
                }, token);

            }, cancellationToken);
        }

        private Task<ObligationSearchResult> GetAllObligationDocumentsByEikAsync(string obligedPersonIdent, string personalDocNumber, string eik, CancellationToken cancellationToken)
        {
            return CallANDServiceAsync(ANDSourceIds.KAT, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllObligationDocumentsByEikAsync(new ObligationDocumentsByEikRequest
                {
                    egn = obligedPersonIdent,
                    docNumber = personalDocNumber,
                    eik = eik
                }, token);

            }, cancellationToken);
        }

        private Task<ObligationSearchResult> GetObligationDocumentsByDocDetailsAsync(string documentType, string documentSeries, string documentNumber, double initialAmount, CancellationToken cancellationToken)
        {
            return CallANDServiceAsync(ANDSourceIds.KAT, (token) =>
            {
                return _iANDServicesClientFactory.GetANDServicesClient().GetAllObligationDocumentsByDocDetailsAsync(new PersonDocumentsInfoByDocDetailsRequest
                {
                    documentType = documentType,
                    documentSeries = documentSeries,
                    documentNumber = documentNumber,
                    initialAmount = initialAmount
                }, token);

            }, cancellationToken);
        }

        private async Task<ObligationSearchResult> CallANDServiceAsync(ANDSourceIds sourceId, Func<CancellationToken, Task<ServiceResult<AllObligationDocumentsResponse>>> action, CancellationToken cancellationToken)
        {
            var res = new ObligationSearchResult { UnitGroup = sourceId, Obligations = new List<Obligation>() };

            try
            {
                var andResponse = await action(cancellationToken);

                if (andResponse.IsSuccessfullyCompleted && andResponse.Response != null)
                {
                    if (andResponse.Response.status == 0)
                    {
                        if (andResponse.Response.allObligations != null && andResponse.Response.allObligations.Any())
                        {
                            res.Obligations = await MapAndObligation(andResponse.Response.allObligations, sourceId, andResponse.Response.customerType, andResponse.Response.userPid, _eAUUserAccessor.User?.LocalClientID);
                        }
                        return res;
                    }
                    else if (andResponse.Response.status == 1)
                    {
                        res.ErrorReadingData = true;
                        return res;
                    }
                    else if (andResponse.Response.status == 2)
                    {
                        res.ErrorNoDataFound = true;
                        return res;
                    }
                    else
                        throw new NotSupportedException();
                }
                else
                {
                    res.ErrorReadingData = true;
                    return res;
                }
            }
            catch (Exception)
            {
                res.ErrorReadingData = true;
                return res;
            }
        }

        private async Task<ObligationSearchResult> CallNRBLDANDServiceAsync(ANDSourceIds sourceId, Func<CancellationToken,
            Task<ServiceResult<WAIS.Integration.MOI.Core.BDS.AND.Models.PersonDocumentsInfoByEgnAndDocNumberResponse>>> action,
            CancellationToken cancellationToken)
        {
            var res = new ObligationSearchResult { UnitGroup = sourceId, Obligations = new List<Obligation>() };

            try
            {
                var nrbldAndResponse = await action(cancellationToken);

                if (nrbldAndResponse.IsSuccessfullyCompleted && nrbldAndResponse.Response != null)
                {
                    if (nrbldAndResponse.Response.status == 0)
                    {
                        if (nrbldAndResponse.Response.allObligations != null && nrbldAndResponse.Response.allObligations.Any())
                        {
                            res.Obligations = await MapNRBLDAndObligation(nrbldAndResponse.Response.allObligations, sourceId,
                                nrbldAndResponse.Response.customerType, nrbldAndResponse.Response.userPid, _eAUUserAccessor.User?.LocalClientID);
                        }
                        return res;
                    }
                    else if (nrbldAndResponse.Response.status == 1)
                    {
                        res.ErrorReadingData = true;
                        return res;
                    }
                    else if (nrbldAndResponse.Response.status == 2)
                    {
                        res.ErrorNoDataFound = true;
                        return res;
                    }
                    else
                        throw new NotSupportedException();
                }
                else
                {
                    res.ErrorReadingData = true;
                    return res;
                }
            }
            catch (Exception)
            {
                res.ErrorReadingData = true;
                return res;
            }
        }

        private ErrorCollection ExtractErrors(ServiceResult ServiceResult)
        {
            return new ErrorCollection(ServiceResult.Errors.Select(t => new TextError(t.Code, t.Message)));
        }

        #endregion
    }
}