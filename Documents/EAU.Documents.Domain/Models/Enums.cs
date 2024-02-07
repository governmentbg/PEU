namespace EAU.Documents.Domain.Models
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/0007-000003")]
    public enum ElectronicServiceProviderType
    {
        /// <summary>
        /// Административен орган.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000031")]
        Administration = 1,

        /// <summary>
        /// Лице, осъществяващо публични функции.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000032")]
        PhysicalPerson = 2,

        /// <summary>
        /// Организация, предоставящи обществени услуги.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000033")]
        Company = 3,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1004")]
    public enum BIDPersonalIdentificationDocumentReceivePlace
    {
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        ODMVR,
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        RPU,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1002")]
    public enum BIDMaritalStatus
    {
        /// <summary>
        /// ВДОВЕЦ/ВДОВИЦА
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("355")]
        Widowed,

        /// <summary>
        /// НЕЖЕНЕН/НЕОМЪЖЕНА
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("356")]
        Single,

        /// <summary>
        /// ЖЕНЕН/ОМЪЖЕНА
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("357")]
        Maried,

        /// <summary>
        /// РАЗВЕДЕН(А)
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("358")]
        Divorsed,

        /// <summary>
        /// ФАКТ.РАЗДЕЛЕН(А)
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("359")]
        Separated,

        /// <summary>
        /// НЕПОКАЗАНО
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4832")]
        Unspecified,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/0007-000001")]
    public enum DocumentElectronicTransportType
    {

        /// <summary>
        /// Пренос на електронен документ чрез уеб базирано приложение / Transfer of an electronic document by means of a web based application
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000001")]
        Item0006000001,

        /// <summary>
        /// Пренос на електронен документ чрез електронна поща / Transfer of an electronic document by means of e-mail
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000002")]
        Item0006000002,

        /// <summary>
        /// Пренос на електронен документ чрез физически носител / Transfer of an electronic document by means of a physical carrier
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000003")]
        Item0006000003,

        /// <summary>
        /// Пренос на електронен документ чрез единната среда за обмен на електронни документи / Transfer of an electronic document by means of the uniform environment for exchange of electronic documents
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000004")]
        Item0006000004,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/0007-000002")]
    public enum ElectronicDocumentDiscrepancyType
    {

        /// <summary>
        /// Подаваното заявление не е в нормативно установения формат
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000005")]
        Item0006000005,

        /// <summary>
        /// Размерът на заявлението заедно с приложенията надвишава определения от административния орган размер за електронните административни услуги, предоставяни от съответната администрация
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000006")]
        Item0006000006,

        /// <summary>
        /// Приложените към заявлението документи не са в нормативно установения формат
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000007")]
        Item0006000007,

        /// <summary>
        /// Подаденото заявление и приложенията към него съдържат вируси или друг нежелан софтуер
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000008")]
        Item0006000008,

        /// <summary>
        /// Подаденото заявление не съдържа уникален идентификатор на заявителя и на получателя на електронната административна услуга при законово изискване за идентификация
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000009")]
        Item0006000009,

        /// <summary>
        /// Заявителят не е посочил електронен пощенски адрес
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000010")]
        Item0006000010,

        /// <summary>
        /// Не е налице техническа възможност за достъп до съдържанието на подадения на физически носител електронен документ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000011")]
        Item0006000011,
    }

    /// <summary>
    /// Цвят на очите
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1001")]
    public enum BIDEyesColor
    {
        /// <summary>
        /// КАФЯВИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1288")]
        Brown,

        /// <summary>
        /// ПЪСТРИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2472")]
        Colorful,

        /// <summary>
        /// СИНИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2698")]
        Blue,

        /// <summary>
        /// СИВИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2704")]
        Gray,

        /// <summary>
        /// ЗЕЛЕНИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3227")]
        Green,

        /// <summary>
        /// ЧЕРНИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3292")]
        Black,

        /// <summary>
        /// НЯМА
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0000")]
        None,

        /// <summary>
        /// ЧЕРВЕНИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("21773")]
        Red,

        /// <summary>
        /// РАЗЛИЧНИ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("22073")]
        Heterochromia
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-3057")]
    public enum PersonIdentificationForeignStatut
    {
        /// <summary>
        /// Гражданин на ЕС
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("EUCitizen")]
        EUCitizen,

        /// <summary>
        /// Чужденец - Постоянно
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("ForeignerPermanently")]
        ForeignerPermanently,

        /// <summary>
        /// Чужденец - Продължително
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("ForeignerTemporarily")]
        ForeignerTemporarily,

        /// <summary>
        /// Чужденец - Постоянно без разрешено пребиваване в РБ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("ForeignerPermanentlyWithoutResidencePermit")]
        ForeignerPermanentlyWithoutResidencePermit,

        /// <summary>
        /// Чужденец - Продължително без разрешено пребиваване в РБ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("ForeignerTemporarilyWithoutResidencePermit")]
        ForeignerTemporarilyWithoutResidencePermit,
    }
}