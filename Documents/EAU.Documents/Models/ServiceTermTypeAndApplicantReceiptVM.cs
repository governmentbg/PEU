using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class ServiceTermTypeAndApplicantReceiptVM
    {
        public ServiceApplicantReceiptDataVM ServiceApplicantReceiptData
        {
            get;
            set;
        }
               
        public ServiceTermType? ServiceTermType
        {
            get;
            set;
        }
    }
}