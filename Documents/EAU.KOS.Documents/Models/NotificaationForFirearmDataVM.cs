using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KOS.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KOS.Documents.Models
{
    public class NotificationForFirearmDataVM
    {
		public PoliceDepartment IssuingPoliceDepartment { get; set; }
		
		public PersonalInformationVM ApplicantInformation { get; set; }

		public string PurchaserUIC { get; set; }
		
        public bool? AgreementToReceiveERefusal { get; set; }

		public List<TechnicalSpecificationOfWeapon> TechnicalSpecificationsOfWeapons { get; set; }

		public PersonAddress PersistedPersonAddress { get; set; }
	}
}