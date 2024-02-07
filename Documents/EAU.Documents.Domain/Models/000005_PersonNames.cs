using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000005")]
    [System.SerializableAttribute()]
    public class PersonNames
    {
        public string First
        {
            get;
            set;
        }

        public string Middle
        {
            get;
            set;
        }

        public string Last
        {
            get;
            set;
        }

        public string Pseudonim
        {
            get;
            set;
        }

        public void CopyTo(PersonNames pn)
        {
            pn.First = this.First;
            pn.Middle = this.Middle;
            pn.Last = this.Last;
            pn.Pseudonim = this.Pseudonim;
        }

        public void CopyFrom(PersonNames pn)
        {
            this.First = !string.IsNullOrEmpty(pn.First) ? pn.First : null;
            this.Middle = !string.IsNullOrEmpty(pn.Middle) ? pn.Middle : null;
            this.Last = !string.IsNullOrEmpty(pn.Last) ? pn.Last : null;
            this.Pseudonim = !string.IsNullOrEmpty(pn.Pseudonim) ? pn.Pseudonim : null;
        }
    }
}
