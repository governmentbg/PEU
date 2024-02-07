namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// Доставчик на електронни административни услуги
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000002")]
    public partial class ElectronicServiceProviderBasicData
    {

        private EntityBasicData entityBasicDataField;

        private ElectronicServiceProviderType electronicServiceProviderTypeField;

        /// <remarks/>
        public EntityBasicData EntityBasicData
        {
            get
            {
                return this.entityBasicDataField;
            }
            set
            {
                this.entityBasicDataField = value;
            }
        }

        /// <remarks/>
        public ElectronicServiceProviderType ElectronicServiceProviderType
        {
            get
            {
                return this.electronicServiceProviderTypeField;
            }
            set
            {
                this.electronicServiceProviderTypeField = value;
            }
        }
    }
}
