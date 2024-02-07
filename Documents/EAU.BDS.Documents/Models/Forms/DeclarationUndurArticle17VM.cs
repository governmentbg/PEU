using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Models.Forms;

namespace EAU.BDS.Documents.Models.Forms
{
    /// <summary>
    /// Декларация по чл.17, ал.1 от Правилника за издаване на българските лични документи.
    /// </summary>
    public class DeclarationUndurArticle17VM : ApplicationFormVMBase
    {
        public const string CIRCUMSTANCES = "Circumstances";

        public DeclarationUndurArticle17Data Circumstances { get; set; }
    }
}
