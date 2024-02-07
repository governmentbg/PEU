using EAU.Documents.Domain.Models;
using EAU.Documents.Models;

namespace EAU.Migr.Documents.Models
{
    public class ApplicationForIssuingDocumentForForeignersDataVM
    {
        public string BirthDate { get; set; }

        public PersonAddress Address { get; set; }

        public string CertificateFor { get; set; }

        public CitizenshipVM Citizenship { get; set; }

        public DocumentMustServeToVM DocumentMustServeTo { get; set; }

        public PersonAddress PersistedAddress { get; set; }
    }
}