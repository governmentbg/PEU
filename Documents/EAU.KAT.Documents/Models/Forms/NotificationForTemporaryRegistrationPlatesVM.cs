using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using System;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Уведомление за определени временни табели
    /// </summary>
    public class NotificationForTemporaryRegistrationPlatesVM : SigningDocumentFormVMBase<OfficialVM>
    {
		#region Properties

		public AISCaseURIVM AISCaseURI { get; set; }

		public string AdministrativeBodyName { get; set; }

		public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

		public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

		public EntityAddress EntityManagementAddress { get; set; }

		public int? CountOfSetsOfTemporaryPlates { get; set; }
		
		public string CountOfSetsOfTemporaryPlatesText { get; set; }

		public string RegistrationNumbersForEachSet { get; set; }

		public DateTime DocumentReceiptOrSigningDate { get; set; }

		public XMLDigitalSignature XMLDigitalSignature { get; set; }

		public PoliceDepartment IssuingPoliceDepartment { get; set; }

		#endregion
	}
}