using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Models
{
    public class RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM
    {
        public List<IdentityDocumentType> IdentificationDocuments { get; set; }
    }
}
