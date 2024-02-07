using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.COD.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1110")]
    public enum PersonAssignorCitizenshipType
    {
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Bulgarian = 1,
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Foreign = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1106")]
    public enum EmployeeCitizenshipType
    {
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Bulgarian = 1,
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Foreign = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1108")]
    public enum PersonAssignorIdentifierType
    {
        /// <summary>
        /// ЕГН
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        EGN = 1,
        /// <summary>
        /// ЛН
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        LN = 2,
        /// <summary>
        /// ДДММГГ
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Date = 3,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1104")]
    public enum PointOfPrivateSecurityServicesLaw
    {
        /// <summary>
        /// Лична охрана на физически лица
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-301")]
        PersonalSecurityServicesForPersons,
        /// <summary>
        /// Охрана на имуществото на физически или юридически лица
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-302")]
        PropertySecurityServices,
        /// <summary>
        /// сигнално-охранителна дейност
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-307")]
        AlarmAndSecurityActivity,
        /// <summary>
        /// Самоохрана на имущество на търговци или юридически лица
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-305")]
        EntityPropertySelfProtection,
        /// <summary>
        /// охрана на обекти - недвижими имоти
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-308")]
        RealEstatSecurity,
        /// <summary>
        /// Охрана на мероприятия
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-303")]
        EventsSecurityServices,
        /// <summary>
        /// Охрана на ценни пратки и товари
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-304")]
        ValuablesAndCargoesSecurityServices,
        /// <summary>
        /// охрана на селскостопанско имущество
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-309")]
        AgriculturalAndPropertyProtection,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1105")]
    public enum ScopeOfCertification
    {
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        WholeCountry = 1,
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        SelectedDistricts = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2207")]
    public enum NotificationOfEmploymentContractType
    {
        /// <summary>
        /// За сключване на нов трудов договор
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2001")]
        Concluding,

        /// <summary>
        /// За прекратяване действието на трудов договор
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2002")]
        Terminating,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2205")]
    public enum ContractType
    {
        /// <summary>
        /// Безсрочен
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2001")]
        Unlimited,

        /// <summary>
        /// Срочен
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2002")]
        ForPeriod,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2209")]
    public enum EmployeeIdentifierType
    {
        /// <summary>
        /// ЕГН
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        EGN = 1,
        /// <summary>
        /// ЛН
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        LN = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2219")]
    public enum SecurityType
    {
        /// <summary>
        /// Въоръжена
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Armed = 1,

        /// <summary>
        /// Невъоръжена
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Unarmed = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2225")]
    public enum AccessRegimeType
    {
        /// <summary>
        /// Извършване на пропускателен режим
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Performing = 2,

        /// <summary>
        /// Не извършване на пропускателен режим
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        NotPerforming = 3,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2227")]
    public enum ControlType
    {
        /// <summary>
        /// Видеонаблюдение
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        VideoControl = 4,

        /// <summary>
        /// Мониторен контрол
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Monitoring = 5,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2221")]
    public enum ClothintType
    {
        /// <summary>
        /// Ежедневно облекло
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Casual = 1,

        /// <summary>
        /// Униформено облекло
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Uniform = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2217")]
    public enum GuardedPersonType
    {
        /// <summary>
        /// Охраняваното лице 
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        GuardedPerson = 1,

        /// <summary>
        /// Представител
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Representative = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2223")]
    public enum VehicleOwnershipType
    {
        /// <summary>
        /// Собствено 
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Own = 1,

        /// <summary>
        /// Наето
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Rented = 2,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2215")]
    public enum GuardedType
    {
        /// <summary>
        /// Мъж
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Man = 1,

        /// <summary>
        /// Жена
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Woman = 2,

        /// <summary>
        /// Момче
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Boy = 3,

        /// <summary>
        /// Момиче
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Girl = 4,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2213")]
    public enum NotificationType
    {
        /// <summary>
        /// За сключване на нов договор за охрана по т. 1, 2, 3, 5, 7, 8 и 9 от чл. 5, ал. 1 от ЗЧОД
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        NewSecurityContr235789 = 1,

        /// <summary>
        /// За издадена нова заповед за самоохрана по т. 4 от чл. 5, ал. 1 от ЗЧОД
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        NewSecurityContr4 = 2,

        /// <summary>
        /// За прекратяване действието на договор за охрана по т. 1, 2, 3, 5, 7, 8 и 9 от чл. 5, ал. 1 от ЗЧОД
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        TerminationSecurityContr235789 = 3,

        /// <summary>
        /// За прекратяване действието на заповед за самоохрана по т. 4 от чл. 5, ал. 1 от ЗЧОД
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        TerminationSecurityContr4 = 4,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2229")]
    public enum AssignorPersonEntityType
    {
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Person = 1,
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Entity = 2,
    }
}
