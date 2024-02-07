using EAU.Security;
using EAU.Services.DocumentProcesses;
using Rebus.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Services.MessageHandlers
{
    public class DocumentSendMessage
    {
        public long DocumentProcessID { get; set; }
    }

    public class DocumentSendMessageHandler : IHandleMessages<DocumentSendMessage>
    {
        private readonly IDocumentProcessService ProcessService;
        private readonly IEAUUserImpersonation UserImpersonation;

        public DocumentSendMessageHandler(IDocumentProcessService processService, IEAUUserImpersonation userImpersonation)
        {
            ProcessService = processService;
            UserImpersonation = userImpersonation;
        }

        public async Task Handle(DocumentSendMessage message)
        {
            using (UserImpersonation.Impersonate(EAUSystemUserAccessor.SystemUser))
            {
                await ProcessService.SendAsync(message.DocumentProcessID, CancellationToken.None);
            }
        }
    }
}
