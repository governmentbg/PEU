using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000007")]
    public class ForeignCitizenNames 
    {
        public string FirstCyrillic
        {
            get;
            set;
        }

        public string LastCyrillic
        {
            get;
            set;
        }

        public string OtherCyrillic
        {
            get;
            set;
        }

        public string PseudonimCyrillic
        {
            get;
            set;
        }

        public string FirstLatin
        {
            get;
            set;
        }

        public string LastLatin
        {
            get;
            set;
        }

        public string OtherLatin
        {
            get;
            set;
        }

        public string PseudonimLatin
        {
            get;
            set;
        }
    }
}
