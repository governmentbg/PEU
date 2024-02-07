using System;

namespace EAU.Documents.Models
{
    public class TimeStampInfo
    {   
        public SigningCertificateData SigningCertificateData
        {
            get;
            set;
        }

        public DateTime TimeStampTime
        {
            get;           
            set;
        }
    }
}
