namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeVM
    {
        public string RegistrationNumber
        {
            get;
            set;
        }

        public string MakeModel
        {
            get;
            set;
        }

        public bool? AreFieldsRequired { get; set; }
    }
}
