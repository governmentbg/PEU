namespace EAU.BDS.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1006")]
    public enum AddressForIssuing
    {
        /// <summary>
        /// Постоянен адрес
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        PermanentAddress,

        /// <summary>
        /// Настоящ адрес
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        CurrentAddress,
    }

    /// <summary>
    /// Виза тип
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1005")]
    public enum VisaTypes
    {
        /// <summary>
        /// С виза/летищен трансфер
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        VisaAirportTransfer,

        /// <summary>
        /// Краткосрочна
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        ShortTerm,

        /// <summary>
        /// Транзитна
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Transit,

        /// <summary>
        /// Дългосрочна
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        LongTerm,

        /// <summary>
        /// Без Виза
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        None,
    }
    /// <summary>
    /// Виза данни за включване към удостоверението.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1007")]
    public enum DataContainsInCertificateNomenclature
    {
        /// <summary>
        /// Постоянен адрес (за лична карта).
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        PermanentAddress,

        /// <summary>
        /// Имена на кирилица.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        CyrillicNames,

        /// <summary>
        /// Имена на латиница.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        LatinNames,

        /// <summary>
        /// Дата на издаване.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        IssuingDate,

        /// <summary>
        /// Дата на валидност.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        ExpiryDate,

        /// <summary>
        /// Актуален статус на документ и дата на обявяване.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        CurrentDocStatusAndPublicationDate,
    }
    /// <summary>
    /// Вид личен документ на български граждани.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1008")]
    public enum BulgarianIdentityDocumentTypes
    {
        /// <summary>
        /// Лична карта.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        IDCard,

        /// <summary>
        /// Паспорт.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Passport,

        /// <summary>
        /// Свидетелство за управление на МПС.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        DrivingLicense,
    }
    /// <summary>
    /// Вид личен документ на български граждани.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1009")]
    public enum IssuingBgPersonalDocumentReasonNomenclature
    {
        /// <summary>
        /// Загуба.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Loss,

        /// <summary>
        /// Кражба.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Stealing,

        /// <summary>
        /// Повреждане.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Damage,

        /// <summary>
        /// Унищожаване.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Destruction,
    }
}