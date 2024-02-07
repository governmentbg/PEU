using EAU.BDS.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Models
{
    public class DocumentNumber
    {
        public string Number { get; set; }
    }

    public class OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM
    {
        public string NessesaryInformation { get; set; }

        public List<DocumentNumber> DocumentNumbers { get; set; }

        public List<IssuedBulgarianIdentityDocumentInfo> DocumentsInfos { get; set; }

        public List<DataContainsInCertificateNomenclature> IncludsDataInCertificate { get; set; }
    }
}