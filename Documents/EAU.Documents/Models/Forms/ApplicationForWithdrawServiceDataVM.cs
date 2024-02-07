using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models.Forms
{
    public class ApplicationForWithdrawServiceDataVM
    {
        public string CaseFileURI { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }

        public string IssuingDocument { get; set; }

        public string RefusalReasons { get; set; }
    }
}
