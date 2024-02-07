using CNSys;
using CNSys.Data;
using EAU.Common;
using EAU.Payments.Obligations.MessageHandlers;
using EAU.Payments.Obligations.Models;
using EAU.Payments.Obligations.Repositories;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.PaymentRequests.Repositories;
using EAU.Payments.RegistrationsData.Models;
using EAU.Payments.RegistrationsData.Repositories;
using EAU.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.PaymentRequests
{
    /// <summary>
    /// Реализация на интерфейс за работа със заявки за плащане.
    /// </summary>
    public class PaymentRequestService : IPaymentRequestService
    {
        private IPaymentChannelProvider _paymentChannelProvider { get; set; }
        private readonly IObligationRepository _obligationRepository;
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IRegistrationDataRepository _registrationDataRepository;
        private readonly IDbContextOperationExecutor _dbContextOperationExecutor;
        private readonly IActionDispatcher _actionDispatcher;
        private readonly IEAUUserAccessor _userAccessor;

        public PaymentRequestService(IPaymentChannelProvider provider,
            IObligationRepository obligationRepository,
            IPaymentRequestRepository paymentRequestRepository,
            IRegistrationDataRepository registrationDataRepository,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IActionDispatcher actionDispatcher,
            IEAUUserAccessor userAccessor
            )
        {
            _paymentChannelProvider = provider;
            _obligationRepository = obligationRepository;
            _paymentRequestRepository = paymentRequestRepository;
            _registrationDataRepository = registrationDataRepository;
            _dbContextOperationExecutor = dbContextOperationExecutor;
            _actionDispatcher = actionDispatcher;
            _userAccessor = userAccessor;
        }

        public Task<OperationResult<Models.PaymentRequest>> PaymentCancelled(Models.PaymentRequest request, CancellationToken cancellationToken)
        {
            return PaymentChangeStatus(request, PaymentRequestStatuses.Cancelled, cancellationToken);
        }

        public Task<OperationResult<Models.PaymentRequest>> PaymentExpired(Models.PaymentRequest request, CancellationToken cancellationToken)
        {
            return PaymentChangeStatus(request, PaymentRequestStatuses.Expired, cancellationToken);
        }

        public async Task<OperationResult<Models.PaymentRequest>> PaymentPaid(Models.PaymentRequest request, CancellationToken cancellationToken)
        {
            var res = await _dbContextOperationExecutor.ExecuteAsync((Func<IDbContext, CancellationToken, Task<OperationResult<Models.PaymentRequest>>>)(async (dbContext, innerToken) =>
            {
                var obl = Enumerable.Single<Obligation>((await _obligationRepository.SearchAsync(new ObligationRepositorySearchCriteria()
                {
                    ObligationIDs = new List<long>() { request.ObligationID.Value }
                }, (CancellationToken)cancellationToken)));

                if (obl.Status != ObligationStatuses.Paid && obl.Status != ObligationStatuses.Processed)
                {
                    obl.Status = ObligationStatuses.Paid;
                    await _obligationRepository.UpdateAsync(obl, cancellationToken);
                }

                var paymentRes = await PaymentChangeStatus(request, PaymentRequestStatuses.Paid, cancellationToken);

                await _actionDispatcher.SendAsync(new ObligationProcessMessage()
                {
                    ObligationID = request.ObligationID.Value,
                    PaymentRequestID = request.PaymentRequestID.Value
                });

                return paymentRes;
            }), cancellationToken);


            if (res.OperationResultType == OperationResultTypes.CompletedWithError)
                return res;

            return new OperationResult<Models.PaymentRequest>(OperationResultTypes.SuccessfullyCompleted) { Result = res.Result };
        }

        public async Task<OperationResult<Models.PaymentRequest>> StartPaymentAsync(long obligaionID, StartPaymentRequest request, CancellationToken cancellationToken)
        {
            #region Проверки

            if (request == null
                || request.RegistrationDataType == null
                || request.Amount <= 0)
            {
                return new OperationResult<Models.PaymentRequest>("GL_INVALID_INPUT_PARAMS_E", "GL_INVALID_INPUT_PARAMS_E");
            }

            #endregion

            var res = await _dbContextOperationExecutor.ExecuteAsync((Func<IDbContext, CancellationToken, Task<OperationResult<Models.PaymentRequest>>>)(async (dbContext, innerToken) =>
            {
                var obligation = (await _obligationRepository.SearchAsync(new ObligationRepositorySearchCriteria()
                {
                    ObligationIDs = new List<long>() { obligaionID },
                    WithLock = true
                })).SingleOrDefault();                

                EnsureExistingObligationAndCheckAccess(obligaionID, obligation);

                if (request.Amount != obligation.DiscountAmount &&
                    request.Amount != obligation.Amount)
                {
                    return new OperationResult<Models.PaymentRequest>("GL_INVALID_AMOUNT_E", "GL_INVALID_AMOUNT_E"); //Сумата, която искате да платите не съответства на тази в задължението
                }

                RegistrationDataSearchCriteria registrationDataSearchCriteria = new RegistrationDataSearchCriteria() { Type = request.RegistrationDataType };

                if (request.RegistrationDataType.Value == RegistrationDataTypes.PepOfDaeu)
                {
                    if (string.IsNullOrEmpty(obligation.PepCin))
                    {
                        return new OperationResult<Models.PaymentRequest>("GL_PEP_KIN_MISSING_E", "GL_PEP_KIN_MISSING_E"); //Не е въведен КИН на ПЕП
                    }

                    registrationDataSearchCriteria.Cin = obligation.PepCin;
                }

                var registrationData = (await _registrationDataRepository.SearchAsync(registrationDataSearchCriteria)).Single();

                var createdPaymentRequest = (await _paymentRequestRepository.SearchAsync(new PaymentRequestSearchCriteria()
                {
                    ObligationIDs = new List<long>() { obligaionID },
                    RegistrationDataID = registrationData.RegistrationDataID
                }, cancellationToken)).SingleOrDefault(pr => pr.Status == PaymentRequestStatuses.New || pr.Status == PaymentRequestStatuses.Sent);

                if (createdPaymentRequest != null)
                {
                    return new OperationResult<Models.PaymentRequest>(OperationResultTypes.SuccessfullyCompleted) { Result = createdPaymentRequest};
                }

                var paymentRequest = new Models.PaymentRequest()
                {
                    ObligationID = obligaionID,
                    Status = PaymentRequestStatuses.New,
                    Amount = request.Amount,
                    RegistrationDataType = registrationData.Type,
                    RegistrationDataID = registrationData.RegistrationDataID,
                    ObligedPersonName = !string.IsNullOrEmpty(request.ObligedPersonName) ? request.ObligedPersonName : obligation.ObligedPersonName,
                    ObligedPersonIdent = obligation.ObligedPersonIdent,
                    ObligedPersonIdentType = obligation.ObligedPersonIdentType,
                    PayerIdent = request.PayerIdent,
                    PayerIdentType = request.PayerIdentType,
                    AdditionalData = new Utilities.AdditionalData()
                };

                paymentRequest.AdditionalData.Add("portalUrl", registrationData.PortalUrl);
                paymentRequest.AdditionalData.Add("okCancelUrl", request.OkCancelUrl);

                //Създаване на нова заявка за плащане.
                await _paymentRequestRepository.CreateAsync(paymentRequest, cancellationToken);

                var paymentChannelService = _paymentChannelProvider.GetPaymentChannelService(request.RegistrationDataType.Value);
                var payInChannelResult = (await paymentChannelService.PayAsync(paymentRequest, obligation, registrationData, cancellationToken));

                OperationResult<Models.PaymentRequest> result = new OperationResult<Models.PaymentRequest>();

                if (payInChannelResult.OperationResultType == OperationResultTypes.CompletedWithError)
                {
                    result.SetAsUnsuccessfull(payInChannelResult.Errors);

                    await _paymentRequestRepository.DeleteAsync(paymentRequest.PaymentRequestID.Value);
                }
                else
                {
                    paymentRequest.Status = PaymentRequestStatuses.Sent;

                    if (!paymentRequest.SendDate.HasValue)
                    {
                        paymentRequest.SendDate = DateTime.Now;
                    }

                    //Ъпдейт на статус и обект, които са променени в Pay метода
                    await _paymentRequestRepository.UpdateAsync(paymentRequest, cancellationToken);

                    result = new OperationResult<Models.PaymentRequest>(OperationResultTypes.SuccessfullyCompleted)
                    {
                        Result = paymentRequest
                    };
                }

                return result;

            }), cancellationToken);

            return res;
        }

        public Task<IEnumerable<Models.PaymentRequest>> SearchAsync(PaymentRequestSearchCriteria criteria, CancellationToken cancellationToken)
        {
            return _paymentRequestRepository.SearchAsync(criteria, cancellationToken);
        }

        public Task<OperationResult<Models.PaymentRequest>> PaymentDuplicate(Models.PaymentRequest request, CancellationToken cancellationToken)
        {
            return PaymentChangeStatus(request, PaymentRequestStatuses.Duplicate, cancellationToken);
        }

        #region Helpers

        private async Task<OperationResult<Models.PaymentRequest>> PaymentChangeStatus(Models.PaymentRequest request, PaymentRequestStatuses status, CancellationToken cancellationToken)
        {
            var createdPaymentRequests = (await _paymentRequestRepository.SearchAsync(new PaymentRequestSearchCriteria() { PaymentRequestIDs = new List<long>() { request.PaymentRequestID.Value } }, cancellationToken)).ToList();
            if (createdPaymentRequests == null || createdPaymentRequests.Count != 1)
            {
                throw new NotSupportedException("There should be exactly one PaymentRequests");
            }
            var paymentRequestForUpdate = createdPaymentRequests.Single();
            if (paymentRequestForUpdate.Status != status)
            {
                paymentRequestForUpdate.Status = status;

                if (status == PaymentRequestStatuses.Paid
                    && !paymentRequestForUpdate.PayDate.HasValue)
                {
                    paymentRequestForUpdate.PayDate = DateTime.Now;
                }

                paymentRequestForUpdate.AdditionalData = request.AdditionalData;

                await _paymentRequestRepository.UpdateAsync(paymentRequestForUpdate, cancellationToken);
            }

            var result = new OperationResult<Models.PaymentRequest>(OperationResultTypes.SuccessfullyCompleted);
            result.Result = paymentRequestForUpdate;

            return result;
        }

        private void EnsureExistingObligationAndCheckAccess(long obligationID, Obligation obligation)
        {
            if (obligation == null)
                throw new NoDataFoundException(obligationID.ToString(), "Obligation");

            if (obligation.ApplicantID != null && _userAccessor.User != null
                && obligation.ApplicantID != _userAccessor.User.LocalClientID)
                throw new AccessDeniedException(obligationID.ToString(), "Obligation", _userAccessor.User.LocalClientID);
        }        

        #endregion
    }
}
