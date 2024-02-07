using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models.Forms;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Справка за промяна на собственост на ПС V2
    /// </summary>
    public class ReportForChangingOwnershipV2VM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public AISCaseURIVM AISCaseURI { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

		public ReportForChangingOwnershipV2ChangeWithBarterVM VehicleRegistrationChangeWithBarter { get; set; }

		public List<ReportForChangingOwnershipV2ChangeVM> VehicleRegistrationChange { get; set; }

        public DateTime? DocumentReceiptOrSigningDate { get; set; }

        public string AdministrativeBodyName { get; set; }

        public XMLDigitalSignature XMLDigitalSignature { get; set; }
    }

	/// <summary>
	/// Промяна на собственост на регистрирани ППС, собственост на едно ЮЛ или ФЛ
	/// </summary>
	public class ReportForChangingOwnershipV2ChangeVM
	{
		public ReportForChangingOwnershipV2VehicleDataVM VehicleRegistrationData { get; set; }

		public List<PersonEntityDataVM> CurrentOwners { get; set; }

		public List<PersonEntityDataVM> NewOwners { get; set; }
	}

	/// <summary>
	/// Замяна на ППС между двама собственици
	/// </summary>
	public class ReportForChangingOwnershipV2ChangeWithBarterVM
    {
		public ReportForChangingOwnershipV2VehicleDataVM FirstVehicleData { get; set; }

		public ReportForChangingOwnershipV2VehicleDataVM SecondVehicleData { get; set; }

		public List<PersonEntityDataVM> FirstVehicleOwners { get; set; }

		public List<PersonEntityDataVM> SecondVehicleOwners { get; set; }
	}

	public class ReportForChangingOwnershipV2VehicleDataVM
	{
		public VehicleRegistrationData RegistrationData { get; set; }

		public List<Status> LocalTaxes { get; set; }

		public ReportForChangingOwnershipV2VehicleDataPeriodicTechnicalCheck PeriodicTechnicalCheck { get; set; }

		public ReportForChangingOwnershipV2VehicleDataGuaranteeFund GuaranteeFund { get; set; }
	}
}
