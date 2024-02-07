using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Nomenclatures.Models
{
    /// <summary>
    /// Номенклатура на административните услуги.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Идентификатор на услуга
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Флаг указващ дали дадения запис е преведен за избрания език.
        /// </summary>
        [DapperColumn("is_translated")]
        public bool? IsTranslated { get; set; }

        /// <summary>
        /// Идентификатор на група
        /// </summary>
        [DapperColumn("group_id")]
        public int? GroupID { get; set; }

        /// <summary>
        /// Идентификатор на документа инициращ услугата
        /// </summary>
        [DapperColumn("doc_type_id")]
        public int? DocumentTypeID { get; set; }

        /// <summary>
        /// Наименование  на услугата
        /// </summary>
        [DapperColumn("name")]
        public  string Name { get; set; }

        /// <summary>
        /// УРИ на административна услуга
        /// </summary>
        [DapperColumn("sunau_service_uri")]
        public string SunauServiceUri { get; set; }


        /// <summary>
        /// Начин на стартиране на услугата
        /// </summary>
        [DapperColumn("initiation_type_id")]
        public WaysToStartService? InitiationTypeID { get; set; }

        /// <summary>
        /// Наименование на документа получен в резултат изпълнението на услугата. (Ползва се когато с едно заявление може да се заявят множе се стартират различни услуги.)
        /// Може да отпадне
        /// </summary>
        [DapperColumn("result_document_name")]
        public string ResultDocumentName { get; set; }

        /// <summary>
        /// html съдържание за описание на административната услуга.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// Пояснителен текст към услуга.
        /// </summary>
        [DapperColumn("explanatory_text_service")]
        public string ExplanatoryTextService { get; set; }

        /// <summary>
        /// Пояснителен текст при изпълнена услуга.
        /// </summary>
        [DapperColumn("explanatory_text_fulfilled_service")]
        public string ExplanatoryTextFulfilledService { get; set; }

        /// <summary>
        /// Пояснителен текст при отказана/прекратена услуга.
        /// </summary>
        [DapperColumn("explanatory_text_refused_or_terminated_service")]
        public string ExplanatoryTextRefusedOrTerminatedService { get; set; }

        /// <summary>
        /// Пореден номер на услугата за група.
        /// </summary>
        [DapperColumn("order_number")]
        public int? OrderNumber { get; set; }

        /// <summary>
        /// Наименование на структурна еденица  от администрацията или длъжностно лице до което се подава заявлението – използва се при визуализация и печат.
        /// Може да отпадне
        /// </summary>        
        [DapperColumn("adm_structure_unit_name")]
        public string AdmStructureUnitName { get; set; }

        /// <summary>
        /// Описание на прилаганите документи
        /// </summary>
        [DapperColumn("attached_documents_description")]
        public string AttachedDocumentsDescription { get; set; }

        /// <summary>
        /// Доплънителна конфигурация
        /// </summary>
        [DapperColumn("additional_configuration")]
        public AdditionalData AdditionalConfiguration { get; set; }

        /// <summary>
        /// Доплънителна конфигурация 
        /// </summary>       
        public string AdditionalDataAsString { get; set; }

        /// <summary>
        /// Адрес на услугата
        /// </summary>
        [DapperColumn("service_url")]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Флаг указващ дали услугата е активна
        /// </summary>
        [DapperColumn("is_active")]
        public bool? IsActive { get; set; }

        /// <summary>
        /// Код на език.
        /// </summary>
        [DapperColumn("language_code")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Дата на създаване/последна промяна.
        /// </summary>
        [DapperColumn("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Номенклатура на видовете документи, които поддържа портала.
        /// </summary>
        public List<DocumentType> AttachedDocumentTypes { get; set; } = new List<DocumentType>();

        /// <summary>
        /// Номенклатура на декларативни обстоятелства и политики
        /// </summary>
        public List<Declaration> Declarations { get; set; } = new List<Declaration>();

        /// <summary>
        /// Номенклатура начини на предаване на административните услуги в административните структури.
        /// </summary>
        public List<ServiceTerm> SeviceTerms { get; set; } = new List<ServiceTerm>();

        /// <summary>
        /// Номенклатура на начините на предаване.
        /// </summary>
        public List<DeliveryChannel> DeliveryChannels { get; set; } = new List<DeliveryChannel>();
    }

}
