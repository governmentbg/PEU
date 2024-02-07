using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public enum ChoiceType
    {
        /// <summary>
        /// DocumentUri.
        /// </summary>
        DocumentUri,

        /// <summary>
        /// String.
        /// </summary>
        TextUri
    }

    public class AISCaseURIVM
    {
        public ChoiceType Choise
        {
            get;
            set;
        }

        public DocumentURI DocumentUri 
        { 
            get; 
            set; 
        }

        public string TextUri 
        { 
            get; 
            set; 
        }
    }
}
