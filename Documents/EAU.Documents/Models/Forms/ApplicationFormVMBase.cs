using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Models.Forms
{
    public abstract class ApplicationFormVMBase : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        #region Properties

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        //TODO:Да се провери дали се ползва и да се махне
        public ElectronicServiceApplicantContactData ElectronicServiceApplicantContactData { get; set; }

        public ServiceTermTypeAndApplicantReceiptVM ServiceTermTypeAndApplicantReceipt { get; set; }

        public DeclarationsVM Declarations { get; set; }

        public ElectronicAdministrativeServiceHeaderVM ElectronicAdministrativeServiceHeader { get; set; }
        
        public bool? AuthorHasNonHandedSlip { get; set; }

        public bool? AuthorHasNonPaidSlip { get; set; }

        /// <summary>
        /// Заявленията не съдържат в тях УРИ-та, те имат само УРИ на коригиращо заявление.
        /// </summary>
        public override DocumentURI DocumentURI { get => null; set { } }

        #endregion

        #region Constructors

        public ApplicationFormVMBase()
        {
        }

        #endregion

        #region Virtual Methods

        #endregion

        #region Override

        public override string DocumentTypeName
        {
            get
            {
                return ElectronicAdministrativeServiceHeader == null ? null :
                    ElectronicAdministrativeServiceHeader.DocumentTypeName;
            }
            set
            {
                if (ElectronicAdministrativeServiceHeader == null)
                    ElectronicAdministrativeServiceHeader = new ElectronicAdministrativeServiceHeaderVM();

                ElectronicAdministrativeServiceHeader.DocumentTypeName = value;
            }
        }

        public override DocumentTypeURI DocumentTypeURI
        {
            get
            {
                return ElectronicAdministrativeServiceHeader == null ? null :
                    ElectronicAdministrativeServiceHeader.DocumentTypeURI;
            }
            set
            {
                if (ElectronicAdministrativeServiceHeader == null)
                    ElectronicAdministrativeServiceHeader = new ElectronicAdministrativeServiceHeaderVM();

                ElectronicAdministrativeServiceHeader.DocumentTypeURI = value;
            }
        }

        #endregion
    }
}
