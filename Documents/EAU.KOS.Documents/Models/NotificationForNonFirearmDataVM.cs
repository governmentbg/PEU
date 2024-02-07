using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.KOS.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KOS.Documents.Models
{
    public class NotificationForNonFirearmDataVM
    {
		public PoliceDepartment IssuingPoliceDepartment { get; set; }
		
		public WeaponNoticeType WeaponNoticeType { get; set; }

		public PersonalInformationVM ApplicantInformation { get; set; }

		public string PurchaserInformation { get; set; }

		public List<TechnicalSpecificationOfWeapon> TechnicalSpecificationsOfWeapons { get; set; }

		public PersonAddress PersistedPersonAddress { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}