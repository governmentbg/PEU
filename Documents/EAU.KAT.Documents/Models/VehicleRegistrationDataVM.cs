using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class VehicleRegistrationDataVM
    {
        //Регистрационнен номер на ППС
        public string RegistrationNumber { get; set; }

        //Рама (VIN) на ППС
        public string IdentificationNumber { get; set; }

        //Номер на СРМПС
        public string RegistrationCertificateNumber { get; set; }

        public RegistrationCertificateTypeNomenclature? RegistrationCertificateType { get; set; }

        public bool? AvailableDocumentForPaidAnnualTax { get; set; }
    }
}
