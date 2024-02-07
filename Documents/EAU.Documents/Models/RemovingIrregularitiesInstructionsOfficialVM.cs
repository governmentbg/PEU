using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class RemovingIrregularitiesInstructionsOfficialVM
    {
        public enum RemovingIrregularitiesInstructionsOfficialChoiceType
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

        public RemovingIrregularitiesInstructionsOfficialChoiceType Choise 
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
