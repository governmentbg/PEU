using EAU.Security;
using Rebus.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing.ReMessageHandlers
{
    public class SigningProcessCompleteMessage
    {
        public Guid? ProcessID { get; set; }
    }

    public class SigningProcessCompleteMessageHandler : IHandleMessages<SigningProcessCompleteMessage>
    {
        private readonly ISigningProcessesService ProcessingService;
        private readonly IEAUUserImpersonation UserImpersonation;

        public SigningProcessCompleteMessageHandler(ISigningProcessesService processingService, IEAUUserImpersonation userImpersonation)
        {
            ProcessingService = processingService;
            UserImpersonation = userImpersonation;
        }

        public async Task Handle(SigningProcessCompleteMessage message)
        {
            using (UserImpersonation.Impersonate(new EAUPrincipal(Principal.Anonymous, EAUPrincipal.SystemLocalUserID.ToString())))
            {
                await ProcessingService.CompleteSigningProcessAsync(message.ProcessID.Value, CancellationToken.None);
            }
        }
    }
}
