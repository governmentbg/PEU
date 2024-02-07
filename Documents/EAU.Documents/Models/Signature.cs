using System;
using System.Collections.Generic;

namespace EAU.Documents.Models
{
    public class Signature
    {
        public bool IsValid { get; set; }

        public string Error { get; set; }

        public DateTime? SignatureTime { get; set; }

        public string SignatureUniqueID { get; set; }

        public SigningCertificateData SigningCertificateData { get; set; }

        public List<TimeStampInfo> TimeStampInfos { get; set; }
    }
}
