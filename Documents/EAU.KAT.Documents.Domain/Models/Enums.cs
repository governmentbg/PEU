using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.KAT.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1100")]
    public enum DocumentFor
    {
        /// <summary>
        /// Собственост на ПС по регистрационен номер
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2001")]
        OwnershipOfVehicleWithRegistrationNumberAndMake,
        /// <summary>
        /// Бивша собственост на ПС
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2002")]
        OwnershipOfPreviousVehicle,
        /// <summary>
        /// Собственост на всички  ПС
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2003")]
        OwnershipOfAllVehicles,
        /// <summary>
        /// Няма данни за собственост на ПС с регистрационен номер
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2004")]
        NoDataForOwnershipOfSpecificVehicle,
        /// <summary>
        /// Няма данни да притежава ПС
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2005")]
        NoDataForOwnershipOfVehicles,
        /// <summary>
        /// Няма данни за бивша собственост на ПС с регистрационен номер
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2006")]
        NoDataForPreviousOwnershipOfSpecificVehicle,

    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1111")]
    public enum OwnershipCertificateReason
    {
        [System.Xml.Serialization.XmlEnumAttribute("2001")]
        Others,
        [System.Xml.Serialization.XmlEnumAttribute("2002")]
        InsuranceAuthorities,
        [System.Xml.Serialization.XmlEnumAttribute("2003")]
        ConculServices,
        [System.Xml.Serialization.XmlEnumAttribute("2004")]
        MinistryOfTransport,
        [System.Xml.Serialization.XmlEnumAttribute("2005")]
        Customs,
        [System.Xml.Serialization.XmlEnumAttribute("2006")]
        NotaryAuthorities,
        [System.Xml.Serialization.XmlEnumAttribute("2007")]
        JudicialAuthorities,
        [System.Xml.Serialization.XmlEnumAttribute("2008")]
        FinancialAutorities,
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1112")]
    public enum ANDCertificateReason
    {
        [System.Xml.Serialization.XmlEnumAttribute("2001")]
        OfficialNotice,
        [System.Xml.Serialization.XmlEnumAttribute("2002")]
        StartingWork,
        [System.Xml.Serialization.XmlEnumAttribute("2003")]
        InsuranceAuthority,
        [System.Xml.Serialization.XmlEnumAttribute("2004")]
        MedicalAuthorities,
        [System.Xml.Serialization.XmlEnumAttribute("2005")]
        JudicalAuthorities,
        [System.Xml.Serialization.XmlEnumAttribute("2006")]
        ConsularDepartament,
        [System.Xml.Serialization.XmlEnumAttribute("2007")]
        PrivateInformation,
        [System.Xml.Serialization.XmlEnumAttribute("2008")]
        Retraining,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1301")]
    public enum RegistrationCertificateTypeNomenclature
    {
        //СР на МПС
        [System.Xml.Serialization.XmlEnumAttribute("1000")]
        RegistrationDocument,
        //Регистрационен талон
        [System.Xml.Serialization.XmlEnumAttribute("1001")]
        RegistrationCertificate,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2211")]
    public enum CouponDuplicateIssuensReason
    {
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Loss = 1,
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Theft = 2,
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Damage = 3,
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Destruction = 4,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2233")]
    public enum VehicleOwnerAdditionalCircumstances
    {
        /// <summary>
        /// Лицето е починало
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        DeadPerson,

        /// <summary>
        /// Лицето е лишено от свобода
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Prisoner,

        /// <summary>
        /// Лицето се представлява от родител, законен представител
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        ChildRepresentedByLegalRepresentative,

        /// <summary>
        /// Лицето се представлява от настойник
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        ChildGuardian,

        /// <summary>
        /// Лицето се представлява от попечител
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        ChildRepresentedByTrustee,

        /// <summary>
        /// Лицето е с изтекъл срок на пребиваване/отнет статут на пребиваване
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        PersonWithRevokedResidenceStatus,

        /// <summary>
        /// Продажбата се извършва чрез синдик
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        SoldBySyndic,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2231")]
    public enum VehicleOwnershipChangeType
    {
        /// <summary>
        /// Промяна на собственост на едно пътно превозно средство
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        ChangeOwnership = 1,

        /// <summary>
        /// Замяна на ППС между двама собственици
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Barter = 2,
    }

    /// <summary>
    /// Табели в зависимост от броя цифри и букви
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-2235")]
    public enum PlatesContentTypes
    {
        /// <summary>
        /// произведени с регистрационен номер с 4 цифри
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        FourDigits,

        /// <summary>
        /// с регистрационен номер с комбинация от 6 букви и/или цифри
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        SixLettersOrDigits,
    }
}
