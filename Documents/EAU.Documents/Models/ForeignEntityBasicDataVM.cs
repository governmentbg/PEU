namespace EAU.Documents.Models
{
    public class ForeignEntityBasicDataVM
    {
        public enum ForeignEntityChoiceType
        {
            RegisterData = 1,
            OtherData = 2
        }

        public string ForeignEntityName
        {
            get;
            set;
        }

        public string CountryISO3166TwoLetterCode
        {
            get;
            set;
        }

        public string CountryNameCyrillic
        {
            get;
            set;
        }

        public ForeignEntityChoiceType SelectedChoiceType
        {
            get;
            set;
        }

        public string ForeignEntityRegister
        {
            get;
            set;
        }

        public string ForeignEntityIdentifier
        {
            get;
            set;
        }

        public string ForeignEntityOtherData
        {
            get;
            set;
        }
    }
}
