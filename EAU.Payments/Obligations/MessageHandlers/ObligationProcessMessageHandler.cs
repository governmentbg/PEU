using EAU.Security;
using Rebus.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.Obligations.MessageHandlers
{
    /// <summary>
    /// Съобщение заобработване на задължение.
    /// </summary>
    public class ObligationProcessMessage
    {
        /// <summary>
        /// Идентификатор на задължение.
        /// </summary>
        public long ObligationID { get; set; }

        /// <summary>
        ///  Идентификатор на заявка за плащане.
        /// </summary>
        public long PaymentRequestID { get; set; }
    }

    public class ObligationProcessMessageHandler : IHandleMessages<ObligationProcessMessage>
    {
        private readonly IObligationService _obligationService;
        private readonly IEAUUserImpersonation UserImpersonation;

        public ObligationProcessMessageHandler(IObligationService obligationService, IEAUUserImpersonation userImpersonation)
        {
            _obligationService = obligationService;
            UserImpersonation = userImpersonation;
        }

        public async Task Handle(ObligationProcessMessage message)
        {
            using (UserImpersonation.Impersonate(EAUSystemUserAccessor.SystemUser))
            {
                await _obligationService.ProcessObligation(message.ObligationID, message.PaymentRequestID, CancellationToken.None);
            }
        }
    }
}
