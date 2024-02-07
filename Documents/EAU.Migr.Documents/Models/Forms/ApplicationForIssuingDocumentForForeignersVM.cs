using EAU.Documents.Models.Forms;

namespace EAU.Migr.Documents.Models.Forms
{
    public class ApplicationForIssuingDocumentForForeignersVM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public ApplicationForIssuingDocumentForForeignersDataVM Circumstances { get; set; }
    }
}