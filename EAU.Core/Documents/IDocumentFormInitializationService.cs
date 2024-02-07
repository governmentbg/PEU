using CNSys;
using EAU.Utilities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Documents
{   
    public class DocumentFormInitializationRequest
    {
        public object Form { get; set; }

        public List<object> Signatures { get; set; }

        public AdditionalData AdditionalData { get; set; }

        public DocumentModes? Mode { get; set; }

        public int? ServiceID { get; set; }
    }

    public interface IDocumentFormInitializationService
    {
        Task<OperationResult> InitializeDocumentFormAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken);
    }
}
