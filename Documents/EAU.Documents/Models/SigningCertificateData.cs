using System;

namespace EAU.Documents.Models
{
    public class SigningCertificateData
    {
        public string Issuer { get; set; }
        public string SerialNumber { get; set; }
        public string Subject { get; set; }
        public string SubjectAlternativeName { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
