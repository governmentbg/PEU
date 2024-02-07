using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.COD.Documents.Models.Forms
{
    public class RequestForIssuingLicenseForPrivateSecurityServicesDataVM
    {
        private List<SecurityServiceTypesVM> _securityServiceTypes = new List<SecurityServiceTypesVM>()
                    {
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.PersonalSecurityServicesForPersons},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.PropertySecurityServices},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.AlarmAndSecurityActivity},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.EntityPropertySelfProtection},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.RealEstatSecurity},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.EventsSecurityServices},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.ValuablesAndCargoesSecurityServices},
                        new SecurityServiceTypesVM() {IsSelected = false, PointOfPrivateSecurityServicesLaw = PointOfPrivateSecurityServicesLaw.AgriculturalAndPropertyProtection}
                    };

        public List<SecurityServiceTypesVM> SecurityServiceTypes
        {
            get
            {
                return this._securityServiceTypes;
            }
            set
            {
                _securityServiceTypes = value;
            }
        }

        public EntityAddress EntityManagementAddress { get; set; }

        public EntityAddress CorrespondingAddress { get; set; }

        public string MobilePhone { get; set; } 

        public string SunauServiceUri { get; set; }

        public string IssuingDocumentForCOD { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}