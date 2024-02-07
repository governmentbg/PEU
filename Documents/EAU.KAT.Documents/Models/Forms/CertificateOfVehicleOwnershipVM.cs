using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Удостоверение за бивша/настояща собственост на ПС
    /// </summary>
    public class CertificateOfVehicleOwnershipVM : SigningDocumentFormVMBase<OfficialVM>
    {
        #region Propertioes

        public AISCaseURIVM AISCaseURI
        {
            get;
            set;
        }
        public System.DateTime? DocumentReceiptOrSigningDate
        {
            get;
            set;
        }
        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData
        {
            get;
            set;
        }
        public ElectronicServiceApplicantVM ElectronicServiceApplicant
        {
            get;
            set;
        }
        public string CertificateOfVehicleOwnershipHeader
        {
            get;
            set;
        }
        public string CertificateNumber
        {
            get;
            set;
        }
        //public PersonAddress PermanentAddress
        //{
        //    get;
        //    set;
        //}
        public PoliceDepartment PoliceDepartment
        {
            get;
            set;
        }
        public DocumentFor? CertificateKind
        {
            get;
            set;
        }
        public VehicleDataVM VehicleData
        {
            get;
            set;
        }
        public List<VehicleOwnerInformationItemVM> VehicleOwnerInformationCollection
        {
            get;
            set;
        }
        public System.DateTime? KATVerificationDateTime
        {
            get;
            set;
        }
        public OwnershipCertificateReason? OwnershipCertificateReason
        {
            get;
            set;
        }
        public string AdministrativeBodyName
        {
            get;
            set;
        }
        public PoliceDepartment IssuingPoliceDepartment
        {
            get;
            set;
        }
        #endregion
    }
}
