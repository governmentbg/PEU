using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// УРИ на партида.
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000022")]
    public class RegisterObjectURI 
    {
        #region Constructors

        public RegisterObjectURI()
        {
        }

        public RegisterObjectURI(int registerIndex, int batchNumber)
        {
            RegisterIndex = registerIndex;
            BatchNumber = batchNumber;
        }

        #endregion

        #region Properties

        public int? RegisterIndex
        {
            get;
            set;
        }

        public int? BatchNumber
        {
            get;
            set;
        }

        #endregion
    }
}
