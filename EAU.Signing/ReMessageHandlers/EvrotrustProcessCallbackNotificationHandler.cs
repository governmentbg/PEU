using EAU.Security;
using Rebus.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing.ReMessageHandlers
{
    public class EvrotrustProcessCallbackNotificationMessage
    {
        public string TransactionID { get; set; }

        public int? Status { get; set; }
    }

    public class EvrotrustProcessCallbackNotificationHandler : IHandleMessages<EvrotrustProcessCallbackNotificationMessage>
    {
        private readonly IEAUUserImpersonation _userImpersonation;
        private readonly IEvrotrustProcessorService _evrotrustProcessorService;

        public EvrotrustProcessCallbackNotificationHandler(IEAUUserImpersonation userImpersonation, IEvrotrustProcessorService evrotrustProcessorService)
        {
            _userImpersonation = userImpersonation;
            _evrotrustProcessorService = evrotrustProcessorService;
        }

        public async Task Handle(EvrotrustProcessCallbackNotificationMessage message)
        {
            using (_userImpersonation.Impersonate(new EAUPrincipal(Principal.Anonymous, EAUPrincipal.SystemLocalUserID.ToString())))
            {
                await _evrotrustProcessorService.ProcessRemoteCallbackNotificationAsync(message.TransactionID, message.Status, CancellationToken.None);
            }
        }
    }
}
