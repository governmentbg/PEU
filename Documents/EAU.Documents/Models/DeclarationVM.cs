using System.Collections.Generic;

namespace EAU.Documents.Models
{
    public class DeclarationsVM
    {
        public List<DeclarationVM> Declarations { get; set; }
    }

    public class DeclarationVM
    {
        public bool IsDeclarationFilled { get; set; }

        public string Code { get; set; }

        public string Content { get; set; }

        public string FurtherDescriptionFromDeclarer { get; set; }
    }
}