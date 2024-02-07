using System;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000008")]
    [Serializable]
    public partial class PersonBasicData 
    {
        private PersonNames namesField;
        private PersonIdentifier identifierField;
        private IdentityDocumentBasicData identityDocumentField;
        public PersonNames Names
        {
            get
            {
                return this.namesField;
            }
            set
            {
                this.namesField = value;
            }
        }
        public PersonIdentifier Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }
        public IdentityDocumentBasicData IdentityDocument
        {
            get
            {
                return this.identityDocumentField;
            }
            set
            {
                this.identityDocumentField = value;
            }
        }
    }
}
