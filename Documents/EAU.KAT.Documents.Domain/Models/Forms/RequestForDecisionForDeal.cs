using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Искане на разрешение за сделка
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3305")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3305", IsNullable = false)]    
    public partial class RequestForDecisionForDeal : IApplicationForm
    {
        private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
        private ServiceTermType? serviceTermTypeField;
        private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
        private List<Declaration> declarationsField;
        private List<AttachedDocument> attachedDocumentsField;
        private RequestForDecisionForDealData requestForDecisionForDealDataField;
        private string notaryIdentityNumberField;
        private ElectronicAdministrativeServiceFooter electronicAdministrativeServiceFooterField;
        public ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader
        {
            get
            {
                return this.electronicAdministrativeServiceHeaderField;
            }
            set
            {
                this.electronicAdministrativeServiceHeaderField = value;
            }
        }
        public ServiceTermType? ServiceTermType
        {
            get
            {
                return this.serviceTermTypeField;
            }
            set
            {
                this.serviceTermTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ServiceTermTypeSpecified
        {
            get
            {
                return ServiceTermType.HasValue;
            }
        }
        public ServiceApplicantReceiptData ServiceApplicantReceiptData
        {
            get
            {
                return this.serviceApplicantReceiptDataField;
            }
            set
            {
                this.serviceApplicantReceiptDataField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Declaration> Declarations
        {
            get
            {
                return this.declarationsField;
            }
            set
            {
                this.declarationsField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<AttachedDocument> AttachedDocuments
        {
            get
            {
                return this.attachedDocumentsField;
            }
            set
            {
                this.attachedDocumentsField = value;
            }
        }
        public RequestForDecisionForDealData RequestForDecisionForDealData
        {
            get
            {
                return this.requestForDecisionForDealDataField;
            }
            set
            {
                this.requestForDecisionForDealDataField = value;
            }
        }
        public string NotaryIdentityNumber
        {
            get
            {
                return this.notaryIdentityNumberField;
            }
            set
            {
                this.notaryIdentityNumberField = value;
            }
        }
        public ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter
        {
            get
            {
                return this.electronicAdministrativeServiceFooterField;
            }
            set
            {
                this.electronicAdministrativeServiceFooterField = value;
            }
        }
    }
}
