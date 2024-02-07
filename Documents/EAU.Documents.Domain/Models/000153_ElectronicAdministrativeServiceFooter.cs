using System;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// Данни за подписване на заявление за предоставяне на електронна административна услуга, подадено по електронен път.
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000153")]
    [XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000153")]
    public class ElectronicAdministrativeServiceFooter 
    {
        public DateTime? ApplicationSigningTime
        {
            get;
            set;
        }

        public XMLDigitalSignature XMLDigitalSignature
        {
            get;
            set;
        }
    }
}
