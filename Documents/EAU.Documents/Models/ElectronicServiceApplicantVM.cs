namespace EAU.Documents.Models
{
    public class ElectronicServiceApplicantVM
    {
        public RecipientGroupVM RecipientGroup
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