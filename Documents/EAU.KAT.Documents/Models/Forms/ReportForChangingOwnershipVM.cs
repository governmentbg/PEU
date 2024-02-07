using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    /// Справка за промяна на собственост на ПС
    /// </summary>
    public class ReportForChangingOwnershipVM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public override string DocumentTypeName
        {
            get;
            set;
        }

        public AISCaseURI AISCaseURI
        {
            get;
            set;
        }

        public ElectronicServiceApplicant ElectronicServiceApplicant
        {
            get;
            set;
        }

        public VehicleRegistrationData VehicleRegistrationData
        {
            get;
            set;
        }       

        public ReportForChangingOwnershipOldOwnersDataVM OldOwnersData
        {
            get;
            set;
        }

        public ReportForChangingOwnershipNewOwnersDataVM NewOwnersData
        {
            get;
            set;
        }

        public List<Status> LocalTaxes
        {
            get;
            set;
        }


        public ReportForChangingOwnershipGuaranteeFund GuaranteeFund
        {
            get;
            set;
        }

        public List<Status> PeriodicTechnicalCheck
        {
            get;
            set;
        }

        public DateTime DocumentReceiptOrSigningDate
        {
            get;
            set;
        }

        public string AdministrativeBodyName
        {
            get;
            set;
        }
    }
}
