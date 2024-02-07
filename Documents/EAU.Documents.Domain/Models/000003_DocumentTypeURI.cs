using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// УРИ на документ, вписан в регистъра на информационните обекти
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000003")]
    public class DocumentTypeURI : RegisterObjectURI
    {
        #region Constructors

        public DocumentTypeURI()
        {
        }

        public DocumentTypeURI(int registerIndex, int batchNumber)
            : base(registerIndex, batchNumber)
        {
        }

        #endregion
    }
}
