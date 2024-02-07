using EAU.Documents.Domain.Models.Forms;
using System;
using System.Collections.Generic;

namespace EAU.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("Any", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class AnyType
    {
        private System.Xml.XmlNode[] anyField;
        private System.Xml.XmlAttribute[] anyAttrField;
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr
        {
            get
            {
                return this.anyAttrField;
            }
            set
            {
                this.anyAttrField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("ObjectIdentifier", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class ObjectIdentifierType
    {
        private IdentifierType identifierField;
        private string descriptionField;
        private DocumentationReferencesType documentationReferencesField;
        /// <remarks/>
        public IdentifierType Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }
        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        /// <remarks/>
        public DocumentationReferencesType DocumentationReferences
        {
            get
            {
                return this.documentationReferencesField;
            }
            set
            {
                this.documentationReferencesField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class IdentifierType
    {
        private QualifierType? qualifierField;
        private string valueField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public QualifierType? Qualifier
        {
            get
            {
                return this.qualifierField;
            }
            set
            {
                this.qualifierField = value;
            }
        }
        /// <remarks/>
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "anyURI")]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public enum QualifierType
    {
        /// <remarks/>
        OIDAsURI,
        /// <remarks/>
        OIDAsURN,
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class DocumentationReferencesType
    {
        private string[] documentationReferenceField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DocumentationReference", DataType = "anyURI")]
        public string[] DocumentationReference
        {
            get
            {
                return this.documentationReferenceField;
            }
            set
            {
                this.documentationReferenceField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("EncapsulatedPKIData", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class EncapsulatedPKIDataType
    {
        private string idField;
        private string encodingField;
        private byte[] valueField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get
            {
                return this.encodingField;
            }
            set
            {
                this.encodingField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType = "base64Binary")]
        public byte[] Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("Include", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class IncludeType
    {
        private string uRIField;
        private bool? referencedDataField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool? referencedData
        {
            get
            {
                return this.referencedDataField;
            }
            set
            {
                this.referencedDataField = value;
            }
        }
        /// <remarks/>
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("ReferenceInfo", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class ReferenceInfoType
    {
        private DigestMethodType digestMethodField;
        private byte[] digestValueField;
        private string idField;
        private string uRIField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public DigestMethodType DigestMethod
        {
            get
            {
                return this.digestMethodField;
            }
            set
            {
                this.digestMethodField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", DataType = "base64Binary")]
        public byte[] DigestValue
        {
            get
            {
                return this.digestValueField;
            }
            set
            {
                this.digestValueField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("XAdESTimeStamp", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class XAdESTimeStampType : GenericTimeStampType
    {
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(OtherTimeStampType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(XAdESTimeStampType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public abstract partial class GenericTimeStampType
    {
        private object[] itemsField;
        private CanonicalizationMethodType canonicalizationMethodField;
        private object[] items1Field;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Include", typeof(IncludeType))]
        [System.Xml.Serialization.XmlElementAttribute("ReferenceInfo", typeof(ReferenceInfoType))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public CanonicalizationMethodType CanonicalizationMethod
        {
            get
            {
                return this.canonicalizationMethodField;
            }
            set
            {
                this.canonicalizationMethodField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EncapsulatedTimeStamp", typeof(EncapsulatedPKIDataType))]
        [System.Xml.Serialization.XmlElementAttribute("XMLTimeStamp", typeof(AnyType))]
        public object[] Items1
        {
            get
            {
                return this.items1Field;
            }
            set
            {
                this.items1Field = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("OtherTimeStamp", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class OtherTimeStampType : GenericTimeStampType
    {
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("QualifyingProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class QualifyingPropertiesType
    {
        private SignedPropertiesType signedPropertiesField;
        private UnsignedPropertiesType unsignedPropertiesField;
        private string targetField;
        private string idField;
        /// <remarks/>
        public SignedPropertiesType SignedProperties
        {
            get
            {
                return this.signedPropertiesField;
            }
            set
            {
                this.signedPropertiesField = value;
            }
        }
        /// <remarks/>
        public UnsignedPropertiesType UnsignedProperties
        {
            get
            {
                return this.unsignedPropertiesField;
            }
            set
            {
                this.unsignedPropertiesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SignedProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SignedPropertiesType
    {
        private SignedSignaturePropertiesType signedSignaturePropertiesField;
        private SignedDataObjectPropertiesType signedDataObjectPropertiesField;
        private string idField;
        /// <remarks/>
        public SignedSignaturePropertiesType SignedSignatureProperties
        {
            get
            {
                return this.signedSignaturePropertiesField;
            }
            set
            {
                this.signedSignaturePropertiesField = value;
            }
        }
        /// <remarks/>
        public SignedDataObjectPropertiesType SignedDataObjectProperties
        {
            get
            {
                return this.signedDataObjectPropertiesField;
            }
            set
            {
                this.signedDataObjectPropertiesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SignedSignatureProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SignedSignaturePropertiesType
    {
        private System.DateTime? signingTimeField;
        private CertIDType[] signingCertificateField;
        private SignaturePolicyIdentifierType signaturePolicyIdentifierField;
        private SignatureProductionPlaceType signatureProductionPlaceField;
        private SignerRoleType signerRoleField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = false)]
        public System.DateTime? SigningTime
        {
            get
            {
                return this.signingTimeField;
            }
            set
            {
                this.signingTimeField = value;
            }
        }
        /// <remarks/>
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Cert", IsNullable = false)]
        public CertIDType[] SigningCertificate
        {
            get
            {
                return this.signingCertificateField;
            }
            set
            {
                this.signingCertificateField = value;
            }
        }
        /// <remarks/>
        public SignaturePolicyIdentifierType SignaturePolicyIdentifier
        {
            get
            {
                return this.signaturePolicyIdentifierField;
            }
            set
            {
                this.signaturePolicyIdentifierField = value;
            }
        }
        /// <remarks/>
        public SignatureProductionPlaceType SignatureProductionPlace
        {
            get
            {
                return this.signatureProductionPlaceField;
            }
            set
            {
                this.signatureProductionPlaceField = value;
            }
        }
        /// <remarks/>
        public SignerRoleType SignerRole
        {
            get
            {
                return this.signerRoleField;
            }
            set
            {
                this.signerRoleField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class CertIDType
    {
        private DigestAlgAndValueType certDigestField;
        private X509IssuerSerialType issuerSerialField;
        private string uRIField;
        /// <remarks/>
        public DigestAlgAndValueType CertDigest
        {
            get
            {
                return this.certDigestField;
            }
            set
            {
                this.certDigestField = value;
            }
        }
        /// <remarks/>
        public X509IssuerSerialType IssuerSerial
        {
            get
            {
                return this.issuerSerialField;
            }
            set
            {
                this.issuerSerialField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class DigestAlgAndValueType
    {
        private DigestMethodType digestMethodField;
        private byte[] digestValueField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public DigestMethodType DigestMethod
        {
            get
            {
                return this.digestMethodField;
            }
            set
            {
                this.digestMethodField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", DataType = "base64Binary")]
        public byte[] DigestValue
        {
            get
            {
                return this.digestValueField;
            }
            set
            {
                this.digestValueField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SignaturePolicyIdentifier", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SignaturePolicyIdentifierType
    {
        private object itemField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SignaturePolicyId", typeof(SignaturePolicyIdType))]
        [System.Xml.Serialization.XmlElementAttribute("SignaturePolicyImplied", typeof(object))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class SignaturePolicyIdType
    {
        private ObjectIdentifierType sigPolicyIdField;
        private TransformType[] transformsField;
        private DigestAlgAndValueType sigPolicyHashField;
        private AnyType[] sigPolicyQualifiersField;
        /// <remarks/>
        public ObjectIdentifierType SigPolicyId
        {
            get
            {
                return this.sigPolicyIdField;
            }
            set
            {
                this.sigPolicyIdField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
        public TransformType[] Transforms
        {
            get
            {
                return this.transformsField;
            }
            set
            {
                this.transformsField = value;
            }
        }
        /// <remarks/>
        public DigestAlgAndValueType SigPolicyHash
        {
            get
            {
                return this.sigPolicyHashField;
            }
            set
            {
                this.sigPolicyHashField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("SigPolicyQualifier", IsNullable = false)]
        public AnyType[] SigPolicyQualifiers
        {
            get
            {
                return this.sigPolicyQualifiersField;
            }
            set
            {
                this.sigPolicyQualifiersField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SignatureProductionPlace", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SignatureProductionPlaceType
    {
        private string cityField;
        private string stateOrProvinceField;
        private string postalCodeField;
        private string countryNameField;
        /// <remarks/>
        public string City
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }
        /// <remarks/>
        public string StateOrProvince
        {
            get
            {
                return this.stateOrProvinceField;
            }
            set
            {
                this.stateOrProvinceField = value;
            }
        }
        /// <remarks/>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }
        /// <remarks/>
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SignerRole", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SignerRoleType
    {
        private AnyType[] claimedRolesField;
        private EncapsulatedPKIDataType[] certifiedRolesField;
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ClaimedRole", IsNullable = false)]
        public AnyType[] ClaimedRoles
        {
            get
            {
                return this.claimedRolesField;
            }
            set
            {
                this.claimedRolesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CertifiedRole", IsNullable = false)]
        public EncapsulatedPKIDataType[] CertifiedRoles
        {
            get
            {
                return this.certifiedRolesField;
            }
            set
            {
                this.certifiedRolesField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SignedDataObjectProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SignedDataObjectPropertiesType
    {
        private DataObjectFormatType[] dataObjectFormatField;
        private CommitmentTypeIndicationType[] commitmentTypeIndicationField;
        private XAdESTimeStampType[] allDataObjectsTimeStampField;
        private XAdESTimeStampType[] individualDataObjectsTimeStampField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataObjectFormat")]
        public DataObjectFormatType[] DataObjectFormat
        {
            get
            {
                return this.dataObjectFormatField;
            }
            set
            {
                this.dataObjectFormatField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CommitmentTypeIndication")]
        public CommitmentTypeIndicationType[] CommitmentTypeIndication
        {
            get
            {
                return this.commitmentTypeIndicationField;
            }
            set
            {
                this.commitmentTypeIndicationField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AllDataObjectsTimeStamp")]
        public XAdESTimeStampType[] AllDataObjectsTimeStamp
        {
            get
            {
                return this.allDataObjectsTimeStampField;
            }
            set
            {
                this.allDataObjectsTimeStampField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IndividualDataObjectsTimeStamp")]
        public XAdESTimeStampType[] IndividualDataObjectsTimeStamp
        {
            get
            {
                return this.individualDataObjectsTimeStampField;
            }
            set
            {
                this.individualDataObjectsTimeStampField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("DataObjectFormat", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class DataObjectFormatType
    {
        private string descriptionField;
        private ObjectIdentifierType objectIdentifierField;
        private string mimeTypeField;
        private string encodingField;
        private string objectReferenceField;
        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        /// <remarks/>
        public ObjectIdentifierType ObjectIdentifier
        {
            get
            {
                return this.objectIdentifierField;
            }
            set
            {
                this.objectIdentifierField = value;
            }
        }
        /// <remarks/>
        public string MimeType
        {
            get
            {
                return this.mimeTypeField;
            }
            set
            {
                this.mimeTypeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get
            {
                return this.encodingField;
            }
            set
            {
                this.encodingField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string ObjectReference
        {
            get
            {
                return this.objectReferenceField;
            }
            set
            {
                this.objectReferenceField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("CommitmentTypeIndication", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class CommitmentTypeIndicationType
    {
        private ObjectIdentifierType commitmentTypeIdField;
        private object[] itemsField;
        private AnyType[] commitmentTypeQualifiersField;
        /// <remarks/>
        public ObjectIdentifierType CommitmentTypeId
        {
            get
            {
                return this.commitmentTypeIdField;
            }
            set
            {
                this.commitmentTypeIdField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AllSignedDataObjects", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("ObjectReference", typeof(string), DataType = "anyURI")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CommitmentTypeQualifier", IsNullable = false)]
        public AnyType[] CommitmentTypeQualifiers
        {
            get
            {
                return this.commitmentTypeQualifiersField;
            }
            set
            {
                this.commitmentTypeQualifiersField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("UnsignedProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class UnsignedPropertiesType
    {
        private UnsignedSignaturePropertiesType unsignedSignaturePropertiesField;
        private UnsignedDataObjectPropertiesType unsignedDataObjectPropertiesField;
        private string idField;
        /// <remarks/>
        public UnsignedSignaturePropertiesType UnsignedSignatureProperties
        {
            get
            {
                return this.unsignedSignaturePropertiesField;
            }
            set
            {
                this.unsignedSignaturePropertiesField = value;
            }
        }
        /// <remarks/>
        public UnsignedDataObjectPropertiesType UnsignedDataObjectProperties
        {
            get
            {
                return this.unsignedDataObjectPropertiesField;
            }
            set
            {
                this.unsignedDataObjectPropertiesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("UnsignedSignatureProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class UnsignedSignaturePropertiesType
    {
        private object[] itemsField;
        private ItemsChoiceType4[] itemsElementNameField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        [System.Xml.Serialization.XmlElementAttribute("ArchiveTimeStamp", typeof(XAdESTimeStampType))]
        [System.Xml.Serialization.XmlElementAttribute("AttrAuthoritiesCertValues", typeof(CertificateValuesType))]
        [System.Xml.Serialization.XmlElementAttribute("AttributeCertificateRefs", typeof(CompleteCertificateRefsType))]
        [System.Xml.Serialization.XmlElementAttribute("AttributeRevocationRefs", typeof(CompleteRevocationRefsType))]
        [System.Xml.Serialization.XmlElementAttribute("AttributeRevocationValues", typeof(RevocationValuesType))]
        [System.Xml.Serialization.XmlElementAttribute("CertificateValues", typeof(CertificateValuesType))]
        [System.Xml.Serialization.XmlElementAttribute("CompleteCertificateRefs", typeof(CompleteCertificateRefsType))]
        [System.Xml.Serialization.XmlElementAttribute("CompleteRevocationRefs", typeof(CompleteRevocationRefsType))]
        [System.Xml.Serialization.XmlElementAttribute("CounterSignature", typeof(CounterSignatureType))]
        [System.Xml.Serialization.XmlElementAttribute("RefsOnlyTimeStamp", typeof(XAdESTimeStampType))]
        [System.Xml.Serialization.XmlElementAttribute("RevocationValues", typeof(RevocationValuesType))]
        [System.Xml.Serialization.XmlElementAttribute("SigAndRefsTimeStamp", typeof(XAdESTimeStampType))]
        [System.Xml.Serialization.XmlElementAttribute("SignatureTimeStamp", typeof(XAdESTimeStampType))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType4[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("CertificateValues", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class CertificateValuesType
    {
        private object[] itemsField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EncapsulatedX509Certificate", typeof(EncapsulatedPKIDataType))]
        [System.Xml.Serialization.XmlElementAttribute("OtherCertificate", typeof(AnyType))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("CompleteCertificateRefs", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class CompleteCertificateRefsType
    {
        private CertIDType[] certRefsField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Cert", IsNullable = false)]
        public CertIDType[] CertRefs
        {
            get
            {
                return this.certRefsField;
            }
            set
            {
                this.certRefsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("CompleteRevocationRefs", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class CompleteRevocationRefsType
    {
        private CRLRefType[] cRLRefsField;
        private OCSPRefType[] oCSPRefsField;
        private AnyType[] otherRefsField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CRLRef", IsNullable = false)]
        public CRLRefType[] CRLRefs
        {
            get
            {
                return this.cRLRefsField;
            }
            set
            {
                this.cRLRefsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("OCSPRef", IsNullable = false)]
        public OCSPRefType[] OCSPRefs
        {
            get
            {
                return this.oCSPRefsField;
            }
            set
            {
                this.oCSPRefsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("OtherRef", IsNullable = false)]
        public AnyType[] OtherRefs
        {
            get
            {
                return this.otherRefsField;
            }
            set
            {
                this.otherRefsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class CRLRefType
    {
        private DigestAlgAndValueType digestAlgAndValueField;
        private CRLIdentifierType cRLIdentifierField;
        /// <remarks/>
        public DigestAlgAndValueType DigestAlgAndValue
        {
            get
            {
                return this.digestAlgAndValueField;
            }
            set
            {
                this.digestAlgAndValueField = value;
            }
        }
        /// <remarks/>
        public CRLIdentifierType CRLIdentifier
        {
            get
            {
                return this.cRLIdentifierField;
            }
            set
            {
                this.cRLIdentifierField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class CRLIdentifierType
    {
        private string issuerField;
        private System.DateTime issueTimeField;
        private string numberField;
        private string uRIField;
        /// <remarks/>
        public string Issuer
        {
            get
            {
                return this.issuerField;
            }
            set
            {
                this.issuerField = value;
            }
        }
        /// <remarks/>
        public System.DateTime IssueTime
        {
            get
            {
                return this.issueTimeField;
            }
            set
            {
                this.issueTimeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class OCSPRefType
    {
        private OCSPIdentifierType oCSPIdentifierField;
        private DigestAlgAndValueType digestAlgAndValueField;
        /// <remarks/>
        public OCSPIdentifierType OCSPIdentifier
        {
            get
            {
                return this.oCSPIdentifierField;
            }
            set
            {
                this.oCSPIdentifierField = value;
            }
        }
        /// <remarks/>
        public DigestAlgAndValueType DigestAlgAndValue
        {
            get
            {
                return this.digestAlgAndValueField;
            }
            set
            {
                this.digestAlgAndValueField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class OCSPIdentifierType
    {
        private ResponderIDType responderIDField;
        private System.DateTime producedAtField;
        private string uRIField;
        /// <remarks/>
        public ResponderIDType ResponderID
        {
            get
            {
                return this.responderIDField;
            }
            set
            {
                this.responderIDField = value;
            }
        }
        /// <remarks/>
        public System.DateTime ProducedAt
        {
            get
            {
                return this.producedAtField;
            }
            set
            {
                this.producedAtField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class ResponderIDType
    {
        private object itemField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ByKey", typeof(byte[]), DataType = "base64Binary")]
        [System.Xml.Serialization.XmlElementAttribute("ByName", typeof(string))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("RevocationValues", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class RevocationValuesType
    {
        private EncapsulatedPKIDataType[] cRLValuesField;
        private EncapsulatedPKIDataType[] oCSPValuesField;
        private AnyType[] otherValuesField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EncapsulatedCRLValue", IsNullable = false)]
        public EncapsulatedPKIDataType[] CRLValues
        {
            get
            {
                return this.cRLValuesField;
            }
            set
            {
                this.cRLValuesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("EncapsulatedOCSPValue", IsNullable = false)]
        public EncapsulatedPKIDataType[] OCSPValues
        {
            get
            {
                return this.oCSPValuesField;
            }
            set
            {
                this.oCSPValuesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("OtherValue", IsNullable = false)]
        public AnyType[] OtherValues
        {
            get
            {
                return this.otherValuesField;
            }
            set
            {
                this.otherValuesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("CounterSignature", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class CounterSignatureType
    {
        private SignatureType signatureField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureType Signature
        {
            get
            {
                return this.signatureField;
            }
            set
            {
                this.signatureField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#", IncludeInSchema = false)]
    public enum ItemsChoiceType4
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("##any:")]
        Item,
        /// <remarks/>
        ArchiveTimeStamp,
        /// <remarks/>
        AttrAuthoritiesCertValues,
        /// <remarks/>
        AttributeCertificateRefs,
        /// <remarks/>
        AttributeRevocationRefs,
        /// <remarks/>
        AttributeRevocationValues,
        /// <remarks/>
        CertificateValues,
        /// <remarks/>
        CompleteCertificateRefs,
        /// <remarks/>
        CompleteRevocationRefs,
        /// <remarks/>
        CounterSignature,
        /// <remarks/>
        RefsOnlyTimeStamp,
        /// <remarks/>
        RevocationValues,
        /// <remarks/>
        SigAndRefsTimeStamp,
        /// <remarks/>
        SignatureTimeStamp,
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("UnsignedDataObjectProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class UnsignedDataObjectPropertiesType
    {
        private AnyType[] unsignedDataObjectPropertyField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UnsignedDataObjectProperty")]
        public AnyType[] UnsignedDataObjectProperty
        {
            get
            {
                return this.unsignedDataObjectPropertyField;
            }
            set
            {
                this.unsignedDataObjectPropertyField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("QualifyingPropertiesReference", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class QualifyingPropertiesReferenceType
    {
        private string uRIField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SigningCertificate", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class CertIDListType
    {
        private CertIDType[] certField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cert")]
        public CertIDType[] Cert
        {
            get
            {
                return this.certField;
            }
            set
            {
                this.certField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    [System.Xml.Serialization.XmlRootAttribute("SPUserNotice", Namespace = "http://uri.etsi.org/01903/v1.3.2#", IsNullable = false)]
    public partial class SPUserNoticeType
    {
        private NoticeReferenceType noticeRefField;
        private string explicitTextField;
        /// <remarks/>
        public NoticeReferenceType NoticeRef
        {
            get
            {
                return this.noticeRefField;
            }
            set
            {
                this.noticeRefField = value;
            }
        }
        /// <remarks/>
        public string ExplicitText
        {
            get
            {
                return this.explicitTextField;
            }
            set
            {
                this.explicitTextField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
    public partial class NoticeReferenceType
    {
        private string organizationField;
        private string[] noticeNumbersField;
        /// <remarks/>
        public string Organization
        {
            get
            {
                return this.organizationField;
            }
            set
            {
                this.organizationField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("int", DataType = "integer", IsNullable = false)]
        public string[] NoticeNumbers
        {
            get
            {
                return this.noticeNumbersField;
            }
            set
            {
                this.noticeNumbersField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("CipherData", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class CipherDataType
    {
        private object itemField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CipherReference", typeof(CipherReferenceType))]
        [System.Xml.Serialization.XmlElementAttribute("CipherValue", typeof(byte[]), DataType = "base64Binary")]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("CipherReference", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class CipherReferenceType
    {
        private TransformsType1 itemField;
        private string uRIField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Transforms")]
        public TransformsType1 Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "TransformsType", Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    public partial class TransformsType1
    {
        private TransformType[] transformField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public TransformType[] Transform
        {
            get
            {
                return this.transformField;
            }
            set
            {
                this.transformField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("EncryptedData", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class EncryptedDataType : EncryptedType
    {
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EncryptedKeyType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EncryptedDataType))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    public abstract partial class EncryptedType
    {
        private EncryptionMethodType encryptionMethodField;
        private KeyInfoType keyInfoField;
        private CipherDataType cipherDataField;
        private EncryptionPropertiesType encryptionPropertiesField;
        private string idField;
        private string typeField;
        private string mimeTypeField;
        private string encodingField;
        /// <remarks/>
        public EncryptionMethodType EncryptionMethod
        {
            get
            {
                return this.encryptionMethodField;
            }
            set
            {
                this.encryptionMethodField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public KeyInfoType KeyInfo
        {
            get
            {
                return this.keyInfoField;
            }
            set
            {
                this.keyInfoField = value;
            }
        }
        /// <remarks/>
        public CipherDataType CipherData
        {
            get
            {
                return this.cipherDataField;
            }
            set
            {
                this.cipherDataField = value;
            }
        }
        /// <remarks/>
        public EncryptionPropertiesType EncryptionProperties
        {
            get
            {
                return this.encryptionPropertiesField;
            }
            set
            {
                this.encryptionPropertiesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MimeType
        {
            get
            {
                return this.mimeTypeField;
            }
            set
            {
                this.mimeTypeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Encoding
        {
            get
            {
                return this.encodingField;
            }
            set
            {
                this.encodingField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    public partial class EncryptionMethodType
    {
        private int? keySizeField;
        private byte[] oAEPparamsField;
        private System.Xml.XmlNode[] anyField;
        private string algorithmField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = false)]
        public int? KeySize
        {
            get
            {
                return this.keySizeField;
            }
            set
            {
                this.keySizeField = value;
            }
        }
        /// <remarks/>
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] OAEPparams
        {
            get
            {
                return this.oAEPparamsField;
            }
            set
            {
                this.oAEPparamsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("EncryptionProperties", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class EncryptionPropertiesType
    {
        private EncryptionPropertyType[] encryptionPropertyField;
        private string idField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EncryptionProperty")]
        public EncryptionPropertyType[] EncryptionProperty
        {
            get
            {
                return this.encryptionPropertyField;
            }
            set
            {
                this.encryptionPropertyField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("EncryptionProperty", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class EncryptionPropertyType
    {
        private System.Xml.XmlElement[] itemsField;
        private string[] textField;
        private string targetField;
        private string idField;
        private System.Xml.XmlAttribute[] anyAttrField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "ID")]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr
        {
            get
            {
                return this.anyAttrField;
            }
            set
            {
                this.anyAttrField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("EncryptedKey", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class EncryptedKeyType : EncryptedType
    {
        private ReferenceList referenceListField;
        private string carriedKeyNameField;
        private string recipientField;
        /// <remarks/>
        public ReferenceList ReferenceList
        {
            get
            {
                return this.referenceListField;
            }
            set
            {
                this.referenceListField = value;
            }
        }
        /// <remarks/>
        public string CarriedKeyName
        {
            get
            {
                return this.carriedKeyNameField;
            }
            set
            {
                this.carriedKeyNameField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Recipient
        {
            get
            {
                return this.recipientField;
            }
            set
            {
                this.recipientField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class ReferenceList
    {
        private ReferenceType1[] itemsField;
        private ItemsChoiceType5[] itemsElementNameField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DataReference", typeof(ReferenceType1))]
        [System.Xml.Serialization.XmlElementAttribute("KeyReference", typeof(ReferenceType1))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public ReferenceType1[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType5[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "ReferenceType", Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    public partial class ReferenceType1
    {
        private System.Xml.XmlElement[] anyField;
        private string uRIField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string URI
        {
            get
            {
                return this.uRIField;
            }
            set
            {
                this.uRIField = value;
            }
        }
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#", IncludeInSchema = false)]
    public enum ItemsChoiceType5
    {
        /// <remarks/>
        DataReference,
        /// <remarks/>
        KeyReference,
    }
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.w3.org/2001/04/xmlenc#")]
    [System.Xml.Serialization.XmlRootAttribute("AgreementMethod", Namespace = "http://www.w3.org/2001/04/xmlenc#", IsNullable = false)]
    public partial class AgreementMethodType
    {
        private byte[] kANonceField;
        private System.Xml.XmlNode[] anyField;
        private KeyInfoType originatorKeyInfoField;
        private KeyInfoType recipientKeyInfoField;
        private string algorithmField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("KA-Nonce", DataType = "base64Binary")]
        public byte[] KANonce
        {
            get
            {
                return this.kANonceField;
            }
            set
            {
                this.kANonceField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlNode[] Any
        {
            get
            {
                return this.anyField;
            }
            set
            {
                this.anyField = value;
            }
        }
        /// <remarks/>
        public KeyInfoType OriginatorKeyInfo
        {
            get
            {
                return this.originatorKeyInfoField;
            }
            set
            {
                this.originatorKeyInfoField = value;
            }
        }
        /// <remarks/>
        public KeyInfoType RecipientKeyInfo
        {
            get
            {
                return this.recipientKeyInfoField;
            }
            set
            {
                this.recipientKeyInfoField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "anyURI")]
        public string Algorithm
        {
            get
            {
                return this.algorithmField;
            }
            set
            {
                this.algorithmField = value;
            }
        }
    }


    [System.Xml.Serialization.XmlIncludeAttribute(typeof(AISCaseURI))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000044")]
    public partial class RegisteredDocumentURI
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("DocumentInInternalRegisterURI", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("DocumentURI", typeof(DocumentURI))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000073")]
    public partial class AISCaseURI : RegisteredDocumentURI
    {
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3010")]
    public partial class RemovingIrregularitiesInstructionsIrregularities
    {
        private string irregularityTypeField;
        private string additionalInformationSpecifyingIrregularityField;
        public string IrregularityType
        {
            get
            {
                return this.irregularityTypeField;
            }
            set
            {
                this.irregularityTypeField = value;
            }
        }
        public string AdditionalInformationSpecifyingIrregularity
        {
            get
            {
                return this.additionalInformationSpecifyingIrregularityField;
            }
            set
            {
                this.additionalInformationSpecifyingIrregularityField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3010")]
    public partial class RemovingIrregularitiesInstructionsOfficial
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenNames", typeof(ForeignCitizenNames))]
        [System.Xml.Serialization.XmlElementAttribute("PersonNames", typeof(PersonNames))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000135")]
    public partial class CitizenshipRegistrationBasicData
    {
        private PersonBasicData personBasicDataField;
        private string genderCodeField;
        private string genderNameField;
        private System.DateTime? birthDateField;
        private object itemField;
        private List<Citizenship> citizenshipsField;
        public PersonBasicData PersonBasicData
        {
            get
            {
                return this.personBasicDataField;
            }
            set
            {
                this.personBasicDataField = value;
            }
        }
        public string GenderCode
        {
            get
            {
                return this.genderCodeField;
            }
            set
            {
                this.genderCodeField = value;
            }
        }
        public string GenderName
        {
            get
            {
                return this.genderNameField;
            }
            set
            {
                this.genderNameField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? BirthDate
        {
            get
            {
                return this.birthDateField;
            }
            set
            {
                this.birthDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BirthDateSpecified
        {
            get
            {
                return BirthDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirth", typeof(PlaceOfBirth))]
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirthAbroad", typeof(PlaceOfBirthAbroad))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Citizenship> Citizenships
        {
            get
            {
                return this.citizenshipsField;
            }
            set
            {
                this.citizenshipsField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000095")]
    public partial class PlaceOfBirth
    {
        private string districtGRAOCodeField;
        private string districtGRAONameField;
        private string municipalityGRAOCodeField;
        private string municipalityGRAONameField;
        private string settlementGRAOCodeField;
        private string settlementGRAONameField;
        public string DistrictGRAOCode
        {
            get
            {
                return this.districtGRAOCodeField;
            }
            set
            {
                this.districtGRAOCodeField = value;
            }
        }
        public string DistrictGRAOName
        {
            get
            {
                return this.districtGRAONameField;
            }
            set
            {
                this.districtGRAONameField = value;
            }
        }
        public string MunicipalityGRAOCode
        {
            get
            {
                return this.municipalityGRAOCodeField;
            }
            set
            {
                this.municipalityGRAOCodeField = value;
            }
        }
        public string MunicipalityGRAOName
        {
            get
            {
                return this.municipalityGRAONameField;
            }
            set
            {
                this.municipalityGRAONameField = value;
            }
        }
        public string SettlementGRAOCode
        {
            get
            {
                return this.settlementGRAOCodeField;
            }
            set
            {
                this.settlementGRAOCodeField = value;
            }
        }
        public string SettlementGRAOName
        {
            get
            {
                return this.settlementGRAONameField;
            }
            set
            {
                this.settlementGRAONameField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000096")]
    public partial class PlaceOfBirthAbroad
    {
        private string countryGRAOCodeField;
        private string countryNameField;
        private string settlementAbroadNameField;
        public string CountryGRAOCode
        {
            get
            {
                return this.countryGRAOCodeField;
            }
            set
            {
                this.countryGRAOCodeField = value;
            }
        }
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }
        public string SettlementAbroadName
        {
            get
            {
                return this.settlementAbroadNameField;
            }
            set
            {
                this.settlementAbroadNameField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000133")]
    public partial class Citizenship
    {
        private string countryGRAOCodeField;
        private string countryNameField;
        public string CountryGRAOCode
        {
            get
            {
                return this.countryGRAOCodeField;
            }
            set
            {
                this.countryGRAOCodeField = value;
            }
        }
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }
    }   
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000142")]
    public partial class Residence
    {
        private string districtCodeField;
        private string districtNameField;
        private string municipalityCodeField;
        private string municipalityNameField;
        private string settlementCodeField;
        private string settlementNameField;
        public string DistrictCode
        {
            get
            {
                return this.districtCodeField;
            }
            set
            {
                this.districtCodeField = value;
            }
        }
        public string DistrictName
        {
            get
            {
                return this.districtNameField;
            }
            set
            {
                this.districtNameField = value;
            }
        }
        public string MunicipalityCode
        {
            get
            {
                return this.municipalityCodeField;
            }
            set
            {
                this.municipalityCodeField = value;
            }
        }
        public string MunicipalityName
        {
            get
            {
                return this.municipalityNameField;
            }
            set
            {
                this.municipalityNameField = value;
            }
        }
        public string SettlementCode
        {
            get
            {
                return this.settlementCodeField;
            }
            set
            {
                this.settlementCodeField = value;
            }
        }
        public string SettlementName
        {
            get
            {
                return this.settlementNameField;
            }
            set
            {
                this.settlementNameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000150")]
    public partial class IndividualAdministrativeActRefusalOfficial
    {
        private string electronicDocumentAuthorQualityField;
        private object itemField;
        private XMLDigitalSignature xMLDigitalSignatureField;
        private string signatureUniqueIDField;
        public string ElectronicDocumentAuthorQuality
        {
            get
            {
                return this.electronicDocumentAuthorQualityField;
            }
            set
            {
                this.electronicDocumentAuthorQualityField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenNames", typeof(ForeignCitizenNames))]
        [System.Xml.Serialization.XmlElementAttribute("PersonNames", typeof(PersonNames))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        public XMLDigitalSignature XMLDigitalSignature
        {
            get
            {
                return this.xMLDigitalSignatureField;
            }
            set
            {
                this.xMLDigitalSignatureField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignatureUniqueID
        {
            get
            {
                return this.signatureUniqueIDField;
            }
            set
            {
                this.signatureUniqueIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000154")]
    public partial class CorrespondenceConsiderationRefusalOfficial
    {
        private string electronicDocumentAuthorQualityField;
        private object itemField;
        private XMLDigitalSignature xMLDigitalSignatureField;
        private string signatureUniqueIDField;
        public string ElectronicDocumentAuthorQuality
        {
            get
            {
                return this.electronicDocumentAuthorQualityField;
            }
            set
            {
                this.electronicDocumentAuthorQualityField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenNames", typeof(ForeignCitizenNames))]
        [System.Xml.Serialization.XmlElementAttribute("PersonNames", typeof(PersonNames))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        public XMLDigitalSignature XMLDigitalSignature
        {
            get
            {
                return this.xMLDigitalSignatureField;
            }
            set
            {
                this.xMLDigitalSignatureField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignatureUniqueID
        {
            get
            {
                return this.signatureUniqueIDField;
            }
            set
            {
                this.signatureUniqueIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3202")]
    public partial class RefusalWithoutConsideringTerminationProceedingsOfficial
    {
        private string electronicDocumentAuthorQualityField;
        private object itemField;
        private XMLDigitalSignature xMLDigitalSignatureField;
        private string signatureUniqueIDField;
        public string ElectronicDocumentAuthorQuality
        {
            get
            {
                return this.electronicDocumentAuthorQualityField;
            }
            set
            {
                this.electronicDocumentAuthorQualityField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenNames", typeof(ForeignCitizenNames))]
        [System.Xml.Serialization.XmlElementAttribute("PersonNames", typeof(PersonNames))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        public XMLDigitalSignature XMLDigitalSignature
        {
            get
            {
                return this.xMLDigitalSignatureField;
            }
            set
            {
                this.xMLDigitalSignatureField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SignatureUniqueID
        {
            get
            {
                return this.signatureUniqueIDField;
            }
            set
            {
                this.signatureUniqueIDField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000110")]
    public partial class ParentData
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("CitizenshipRegistrationBasicData", typeof(CitizenshipRegistrationBasicData))]
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenData", typeof(ForeignCitizenData))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000109")]
    public partial class ForeignCitizenData
    {
        private ForeignCitizenNames foreignCitizenNamesField;
        private string genderNameField;
        private string genderCodeField;
        private string birthDateField;
        private object itemField;
        private List<Citizenship> citizenshipsField;
        public ForeignCitizenNames ForeignCitizenNames
        {
            get
            {
                return this.foreignCitizenNamesField;
            }
            set
            {
                this.foreignCitizenNamesField = value;
            }
        }
        public string GenderName
        {
            get
            {
                return this.genderNameField;
            }
            set
            {
                this.genderNameField = value;
            }
        }
        public string GenderCode
        {
            get
            {
                return this.genderCodeField;
            }
            set
            {
                this.genderCodeField = value;
            }
        }
        public string BirthDate
        {
            get
            {
                return this.birthDateField;
            }
            set
            {
                this.birthDateField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirth", typeof(PlaceOfBirth))]
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirthAbroad", typeof(PlaceOfBirthAbroad))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Citizenship> Citizenships
        {
            get
            {
                return this.citizenshipsField;
            }
            set
            {
                this.citizenshipsField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000156")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000156", IsNullable = false)]
    public partial class GenderData
    {
        private GenderDataGender[] gendersField;
        private System.DateTime versionDateField;
        [System.Xml.Serialization.XmlArrayItemAttribute("Gender", IsNullable = false)]
        public GenderDataGender[] Genders
        {
            get
            {
                return this.gendersField;
            }
            set
            {
                this.gendersField = value;
            }
        }
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime VersionDate
        {
            get
            {
                return this.versionDateField;
            }
            set
            {
                this.versionDateField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000156")]
    public partial class GenderDataGender
    {
        private string codeField;
        private string nameField;
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000099")]
    public partial class IdentityDocumentBasicData
    {
        private string identityNumberField;
        private System.DateTime identitityIssueDateField;
        private string identityIssuerField;
        private IdentityDocumentType identityDocumentTypeField;

        /// <remarks/>
        public string IdentityNumber
        {
            get
            {
                return this.identityNumberField;
            }
            set
            {
                this.identityNumberField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime IdentitityIssueDate
        {
            get
            {
                return this.identitityIssueDateField;
            }
            set
            {
                this.identitityIssueDateField = value;
            }
        }
        /// <remarks/>
        /// <remarks/>
        public string IdentityIssuer
        {
            get
            {
                return this.identityIssuerField;
            }
            set
            {
                this.identityIssuerField = value;
            }
        }
        /// <remarks/>
        public IdentityDocumentType IdentityDocumentType
        {
            get
            {
                return this.identityDocumentTypeField;
            }
            set
            {
                this.identityDocumentTypeField = value;
            }
        }
        /// <remarks/>
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/0007-000016")]
    public enum IdentityDocumentType
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000087")]
        PersonalCard,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000088")]
        Passport,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000089")]
        DiplomaticPassport,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000090")]
        OfficialPassport,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000091")]
        SeaManPassport,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000092")]
        MilitaryIDCard,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000093")]
        DrivingLicense,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000094")]
        TemporaryPassport,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000095")]
        OfficeOpenSheetBorderCrossing,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000096")]
        TemporaryPassportForLeavingTheRepublicOfBulgaria,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000097")]
        RefugeeCard,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000098")]
        MapForeignerGrantedAsylum,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000099")]
        MapOfForeignerWithHumanitarianStatus,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000100")]
        TemporaryCardOfForeigner,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000101")]
        CertificateForTravelingAbroadOfRefugee,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000102")]
        CertificateForTravelingAbroadOfAForeignerGrantedAsylum,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000103")]
        CertificateForTravelingAbroadOfAForeignerWithHumanitarianStatus,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000104")]
        CertificateForTravelingAbroadForAPersonWithoutCitizenship,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000105")]
        TemporaryCertificateForLeavingTheRepublicOfBulgaria,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000106")]
        ResidencePermitForAContinuouslyStayingForeignerInBulgaria,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000107")]
        ResidencePermitForPermanentResidenceInBulgariaForeigner,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000108")]
        ResidencePermitForResidentFamilyMemberOfEUCitizenWhoHasNotExercised,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000109")]
        ResidencePermitForContinuouslyStayingForeignerMarkedBeneficiaryUnderArticle3,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000110")]
        ResidencePermitForPermanentResidentAlienMarkedBeneficiaryUnderArticle3,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000111")]
        ResidencePermitContinuousResidentFamilyMembers,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000112")]
        ResidencePermitForResidentFamilyMembers,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000113")]
        CertificateForLongStay,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000114")]
        CertificateOfResidence,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000115")]
        DiplomaticCard,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000116")]
        ConsularCard,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000117")]
        MapOfTheAdministrativeAndTechnicalStaff,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000118")]
        MapOfStaff,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000119")]
        CertificateForReturnToTheRepublicOfBulgariaToAForeigner,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000120")]
        ResidencePermitContinuousResidentFamilyMemberOfAnEUCitizen,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000121")]
        ResidencePermit,
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000122")]
        ResidenceCertificateForEUCitizens,
    }
  
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment//R-3136")]
    public partial class Declaration
    {
        private bool isDeclarationFilledField;
        private string declarationNameField;
        private string declarationCodeField;
        private string furtherDescriptionFromDeclarerField;
        public bool IsDeclarationFilled
        {
            get
            {
                return this.isDeclarationFilledField;
            }
            set
            {
                this.isDeclarationFilledField = value;
            }
        }
        public string DeclarationName
        {
            get
            {
                return this.declarationNameField;
            }
            set
            {
                this.declarationNameField = value;
            }
        }
        public string DeclarationCode
        {
            get
            {
                return this.declarationCodeField;
            }
            set
            {
                this.declarationCodeField = value;
            }
        }
        public string FurtherDescriptionFromDeclarer
        {
            get
            {
                return this.furtherDescriptionFromDeclarerField;
            }
            set
            {
                this.furtherDescriptionFromDeclarerField = value;
            }
        }

    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3037")]
    public partial class PoliceDepartment
    {
        private bool? policeDepartmentTypeField;
        private string policeDepartmentCodeField;
        private string policeDepartmentNameField;
        public bool? PoliceDepartmentType
        {
            get
            {
                return this.policeDepartmentTypeField;
            }
            set
            {
                this.policeDepartmentTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PoliceDepartmentTypeSpecified
        {
            get
            {
                return PoliceDepartmentType.HasValue;
            }
        }
        public string PoliceDepartmentCode
        {
            get
            {
                return this.policeDepartmentCodeField;
            }
            set
            {
                this.policeDepartmentCodeField = value;
            }
        }
        public string PoliceDepartmentName
        {
            get
            {
                return this.policeDepartmentNameField;
            }
            set
            {
                this.policeDepartmentNameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3015")]
    public partial class PersonalInformation
    {
        private PersonAddress personAddressField;
        private string mobilePhoneField;
        private string workPhoneField;
        public PersonAddress PersonAddress
        {
            get
            {
                return this.personAddressField;
            }
            set
            {
                this.personAddressField = value;
            }
        }
        public string MobilePhone
        {
            get
            {
                return this.mobilePhoneField;
            }
            set
            {
                this.mobilePhoneField = value;
            }
        }
        public string WorkPhone
        {
            get
            {
                return this.workPhoneField;
            }
            set
            {
                this.workPhoneField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3017")]
    public partial class IdentityDocumentForeignCitizenBasicData
    {
        private string identityNumberField;
        private System.DateTime? identitityIssueDateField;
        private System.DateTime? identitityExpireDateField;
        private string identityIssuerField;
        private IdentityDocumentType? identityDocumentTypeField;
        public string IdentityNumber
        {
            get
            {
                return this.identityNumberField;
            }
            set
            {
                this.identityNumberField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? IdentitityIssueDate
        {
            get
            {
                return this.identitityIssueDateField;
            }
            set
            {
                this.identitityIssueDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdentitityIssueDateSpecified
        {
            get
            {
                return IdentitityIssueDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? IdentitityExpireDate
        {
            get
            {
                return this.identitityExpireDateField;
            }
            set
            {
                this.identitityExpireDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdentitityExpireDateSpecified
        {
            get
            {
                return IdentitityExpireDate.HasValue;
            }
        }
        public string IdentityIssuer
        {
            get
            {
                return this.identityIssuerField;
            }
            set
            {
                this.identityIssuerField = value;
            }
        }
        public IdentityDocumentType? IdentityDocumentType
        {
            get
            {
                return this.identityDocumentTypeField;
            }
            set
            {
                this.identityDocumentTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdentityDocumentTypeSpecified
        {
            get
            {
                return IdentityDocumentType.HasValue;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3040")]
    public partial class DocumentMustServeTo
    {
        private string itemField;
        private ItemChoiceType1 itemElementNameField;
        [System.Xml.Serialization.XmlElementAttribute("AbroadDocumentMustServeTo", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("InRepublicOfBulgariaDocumentMustServeTo", typeof(string))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemChoiceType1 ItemElementName
        {
            get
            {
                return this.itemElementNameField;
            }
            set
            {
                this.itemElementNameField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3040", IncludeInSchema = false)]
    public enum ItemChoiceType1
    {
        InRepublicOfBulgariaDocumentMustServeTo,
        AbroadDocumentMustServeTo,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3100")]
    public partial class OutstandingConditionsForStartOfServiceMessageGrounds
    {
        private string outstandingConditionsForStartOfServiceMessageGroundField;
        public string OutstandingConditionsForStartOfServiceMessageGround
        {
            get
            {
                return this.outstandingConditionsForStartOfServiceMessageGroundField;
            }
            set
            {
                this.outstandingConditionsForStartOfServiceMessageGroundField = value;
            }
        }
    }   
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3101")]
    public partial class TerminationOfServiceMessageGrounds
    {
        private string terminationOfServiceMessageGroundField;
        public string TerminationOfServiceMessageGround
        {
            get
            {
                return this.terminationOfServiceMessageGroundField;
            }
            set
            {
                this.terminationOfServiceMessageGroundField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3203")]
    public partial class EntityAddress : EkatteAddress
    {
       
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3043")]
    public partial class IdentificationPhotoAndSignature
    {
        private Byte[] identificationSignatureField;
        private Byte[] identificationPhotoField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public Byte[] IdentificationSignature
        {
            get
            {
                return this.identificationSignatureField;
            }
            set
            {
                this.identificationSignatureField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public Byte[] IdentificationPhoto
        {
            get
            {
                return this.identificationPhotoField;
            }
            set
            {
                this.identificationPhotoField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3003")]
    public partial class PersonNamesLatin
    {
        private string firstField;
        private string middleField;
        private string lastField;
        public string First
        {
            get
            {
                return this.firstField;
            }
            set
            {
                this.firstField = value;
            }
        }
        public string Middle
        {
            get
            {
                return this.middleField;
            }
            set
            {
                this.middleField = value;
            }
        }
        public string Last
        {
            get
            {
                return this.lastField;
            }
            set
            {
                this.lastField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3005")]
    public partial class PersonData
    {
        private PersonIdentificationData personIdentificationField;
        private object itemField;
        private BIDEyesColor? eyesColorField;
        private BIDMaritalStatus? maritalStatusField;
        private int? heightField;
        public PersonIdentificationData PersonIdentification
        {
            get
            {
                return this.personIdentificationField;
            }
            set
            {
                this.personIdentificationField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirth", typeof(PlaceOfBirth))]
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirthAbroad", typeof(PlaceOfBirthAbroad))]
        [System.Xml.Serialization.XmlElementAttribute("PlaceOfBirthOtherData", typeof(string))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
        public BIDEyesColor? EyesColor
        {
            get
            {
                return this.eyesColorField;
            }
            set
            {
                this.eyesColorField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EyesColorSpecified
        {
            get
            {
                return EyesColor.HasValue;
            }
        }
        public BIDMaritalStatus? MaritalStatus
        {
            get
            {
                return this.maritalStatusField;
            }
            set
            {
                this.maritalStatusField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MaritalStatusSpecified
        {
            get
            {
                return MaritalStatus.HasValue;
            }
        }
        public int? Height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HeightSpecified
        {
            get
            {
                return Height.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3004")]
    public partial class PersonIdentificationData
    {
        private PersonNames namesField;
        private PersonNamesLatin namesLatinField;
        private PersonIdentifier identifierField;
        private System.DateTime birthDateField;
        private GenderData genderField;
        public PersonNames Names
        {
            get
            {
                return this.namesField;
            }
            set
            {
                this.namesField = value;
            }
        }
        public PersonNamesLatin NamesLatin
        {
            get
            {
                return this.namesLatinField;
            }
            set
            {
                this.namesLatinField = value;
            }
        }
        public PersonIdentifier Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime BirthDate
        {
            get
            {
                return this.birthDateField;
            }
            set
            {
                this.birthDateField = value;
            }
        }
        public GenderData Gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3023")]
    public partial class TravelDocument
    {
        private string identityNumberField;
        private System.DateTime? identitityIssueDateField;
        private System.DateTime? identitityExpireDateField;
        private IssuerCountry identityIssuerField;
        private string identityDocumentSeriesField;
        public string IdentityNumber
        {
            get
            {
                return this.identityNumberField;
            }
            set
            {
                this.identityNumberField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? IdentitityIssueDate
        {
            get
            {
                return this.identitityIssueDateField;
            }
            set
            {
                this.identitityIssueDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdentitityIssueDateSpecified
        {
            get
            {
                return IdentitityIssueDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? IdentitityExpireDate
        {
            get
            {
                return this.identitityExpireDateField;
            }
            set
            {
                this.identitityExpireDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdentitityExpireDateSpecified
        {
            get
            {
                return IdentitityExpireDate.HasValue;
            }
        }
        public IssuerCountry IdentityIssuer
        {
            get
            {
                return this.identityIssuerField;
            }
            set
            {
                this.identityIssuerField = value;
            }
        }
        public string IdentityDocumentSeries
        {
            get
            {
                return this.identityDocumentSeriesField;
            }
            set
            {
                this.identityDocumentSeriesField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3049")]
    public partial class IssuerCountry
    {
        private string countryGRAOCodeField;
        private string countryNameField;
        public string CountryGRAOCode
        {
            get
            {
                return this.countryGRAOCodeField;
            }
            set
            {
                this.countryGRAOCodeField = value;
            }
        }
        public string CountryName
        {
            get
            {
                return this.countryNameField;
            }
            set
            {
                this.countryNameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3059")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3059", IsNullable = false)]
    public partial class ApplicationForWithdrawService : IApplicationForm
    {
        private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
        private ServiceTermType? serviceTermTypeField;
        private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private ApplicationForWithdrawServiceData applicationForWithdrawServiceDataField;
        private ElectronicAdministrativeServiceFooter electronicAdministrativeServiceFooterField;
        private List<Declaration> declarationsField;
        private List<AttachedDocument> attachedDocumentsField;
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
        public ElectronicServiceApplicant ElectronicServiceApplicant
        {
            get
            {
                return this.electronicServiceApplicantField;
            }
            set
            {
                this.electronicServiceApplicantField = value;
            }
        }
        public ApplicationForWithdrawServiceData ApplicationForWithdrawServiceData
        {
            get
            {
                return this.applicationForWithdrawServiceDataField;
            }
            set
            {
                this.applicationForWithdrawServiceDataField = value;
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
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3060")]
    public partial class ApplicationForWithdrawServiceData
    {
        private string caseFileURIField;
        private PoliceDepartment issuingPoliceDepartmentField;
        private string issuingDocumentField;
        private string refusalReasonsField;
        public string CaseFileURI
        {
            get
            {
                return this.caseFileURIField;
            }
            set
            {
                this.caseFileURIField = value;
            }
        }
        public PoliceDepartment IssuingPoliceDepartment
        {
            get
            {
                return this.issuingPoliceDepartmentField;
            }
            set
            {
                this.issuingPoliceDepartmentField = value;
            }
        }
        public string IssuingDocument
        {
            get
            {
                return this.issuingDocumentField;
            }
            set
            {
                this.issuingDocumentField = value;
            }
        }
        public string RefusalReasons
        {
            get
            {
                return this.refusalReasonsField;
            }
            set
            {
                this.refusalReasonsField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3119")]
    public partial class OutstandingConditionsForWithdrawServiceMessageGrounds
    {
        private string outstandingConditionsForWithdrawServiceMessageGroundField;
        public string OutstandingConditionsForWithdrawServiceMessageGround
        {
            get
            {
                return this.outstandingConditionsForWithdrawServiceMessageGroundField;
            }
            set
            {
                this.outstandingConditionsForWithdrawServiceMessageGroundField = value;
            }
        }
    }
}
