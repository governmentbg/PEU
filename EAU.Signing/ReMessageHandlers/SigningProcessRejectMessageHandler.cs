using EAU.Security;
using Rebus.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing.ReMessageHandlers
{
    public class SigningProcessRejectMessage
    {
        public Guid? ProcessID { get; set; }
    }

    public class SigningProcessRejectMessageHandler : IHandleMessages<SigningProcessRejectMessage>
    {
        private readonly ISigningProcessesService ProcessingService;
        private readonly IEAUUserImpersonation UserImpersonation;

        public SigningProcessRejectMessageHandler(ISigningProcessesService processingService, IEAUUserImpersonation userImpersonation)
        {
            ProcessingService = processingService;
            UserImpersonation = userImpersonation;
        }

        public async Task Handle(SigningProcessRejectMessage message)
        {
            using (UserImpersonation.Impersonate(new EAUPrincipal(Principal.Anonymous, EAUPrincipal.SystemLocalUserID.ToString())))
            {
                await ProcessingService.RejectSigningProcessAsync(message.ProcessID.Value, CancellationToken.None);
            }   
        }
    }
}
