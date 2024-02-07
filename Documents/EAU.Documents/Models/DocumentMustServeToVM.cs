using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class DocumentMustServeToVM
    {
        public string ItemInRepublicOfBulgariaDocumentMustServeTo
        {
            get;
            set;
        }

        public string ItemAbroadDocumentMustServeTo
        {
            get;
            set;
        }

        public ItemChoiceType1? ItemElementName
        {
            get;
            set;
        }
    }
}

