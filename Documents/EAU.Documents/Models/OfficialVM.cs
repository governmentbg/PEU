using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class OfficialVM : DigitalSignatureContainerVM
    {
        public enum OfficialChoiceType
        {
            /// <summary>
            /// PersonNames.
            /// </summary>
            PersonNames,

            /// <summary>
            /// ForeignCitizenNames.
            /// </summary>
            ForeignCitizenNames
        }

        public bool HasAuthorQuality 
        {
            get; 
            set; 
        }

        public string ElectronicDocumentAuthorQuality 
        { 
            get; 
            set; 
        }

        public OfficialChoiceType Choise
        {
            get;
            set;
        }

        public string Position
        {
            get; 
            set;
        }

        public PersonNames PersonNames
        {
            get;
            set;
        }

        public ForeignCitizenNames ForeignCitizenNames
        {
            get;
            set;
        }
    }
}
