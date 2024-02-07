using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class ServiceApplicantReceiptDataVM
    {
        public ServiceResultReceiptMethods? ServiceResultReceiptMethod { get; set; }

        public ServiceApplicantReceiptDataAddress ApplicantAdress { get; set; }

        public ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM MunicipalityAdministrationAdress { get; set; }

        public ServiceApplicantReceiptDataUnitInAdministration UnitInAdministration { get; set; }

        public ServiceApplicantReceiptDataUnitInAdministration PredifinedUnitInAdministration { get; set; }

        public bool? UsePredifinedUnitInAdministration { get; set; }

        public bool? UseFilteredUnitInAdministration { get; set; }

        public bool? RestrictReceiptUnitToPermanentAddress { get; set; }

        public string PostOfficeBox { get; set; }
    }
}
