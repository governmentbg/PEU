using System;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// Основни данни за юридическо лице.
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000013")]
    [Serializable]
    public partial class EntityBasicData 
    {
        public string Name
        {
            get;
            set;
        }

        public string Identifier
        {
            get;
            set;
        }
    }
}
