using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class PlaceOfBirthToggler
    {
        public enum PlaceOfBirthChoiceTypes
        {
            /// <summary>
            /// PlaceOfBirth.
            /// </summary>
            PlaceOfBirth,

            /// <summary>
            /// PlaceOfBirthAbroad.
            /// </summary>
            PlaceOfBirthAbroad
        }

        public PlaceOfBirthChoiceTypes PlaceOfBirthChoiceType
        {
            get;
            set;
        }

        public PlaceOfBirthVM PlaceOfBirth
        {
            get;
            set;
        }

        public PlaceOfBirthAbroad PlaceOfBirthAbroad
        {
            get;
            set;
        }
    }
}
