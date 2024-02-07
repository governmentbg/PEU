using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// Вид на заявлението според обстоятелството дали се подава за първи път или е
    /// последващ документ
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// Първоначално заявление за предоставяне на електронна административна услуга.
        /// </summary>
        [XmlEnum("0006-000121")]
        AppForFirstReg = 0,

        /// <summary>
        /// Заявление за промяна или допълване на данни в първоначално подадено заявление.
        /// </summary>
        [XmlEnum("0006-000122")]
        AppForChangeData = 1,

        /// <summary>
        /// Заявление за отстраняване на нередовности или предоставяне на информация.
        /// </summary>
        [XmlEnum("0006-000123")]
        AppForRemoveInvalidData = 2,

        /// <summary>
        /// Заявление за отказ от заявена услуга.
        /// </summary>
        [XmlEnum("0006-000124")]
        AppForWithdrawService = 3
    }

    /// <summary>
    /// Водеща част на заявление за предоставяне на електронна административна услуга, когато се изисква идентификация на заявителя.
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    [XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000152")]
    public class ElectronicAdministrativeServiceHeader 
    {
        public string SUNAUServiceURI
        {
            get;
            set;
        }

        public DocumentTypeURI DocumentTypeURI
        {
            get;
            set;
        }

        public string DocumentTypeName
        {
            get;
            set;
        }

        public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData
        {
            get;
            set;
        }

        public ElectronicServiceApplicant ElectronicServiceApplicant
        {
            get;
            set;
        }

        public ElectronicServiceApplicantContactData ElectronicServiceApplicantContactData
        {
            get;
            set;
        }
        
        public ApplicationType ApplicationType
        {
            get;
            set;
        }

        public string SUNAUServiceName
        {
            get;
            set;
        }

        public DocumentURI DocumentURI
        {
            get;
            set;
        }

        public bool SendApplicationWithReceiptAcknowledgedMessage
        {
            get;
            set;
        }
    }
}
