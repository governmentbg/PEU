using EAU.Documents.Domain.Models;
using System;

namespace EAU.Documents.Models
{
    public class IdentityDocumentBasicDataVM
    {
        public string IdentityNumber { get; set; }

        public DateTime? IdentitityIssueDate { get; set; }

        public string IdentityIssuer { get; set; }

        public IdentityDocumentType? IdentityDocumentType { get; set; }
    }
}