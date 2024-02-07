using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Common
{
    /// <summary>
    /// TODO: Не е пренесен напълно! Да се допълва.
    /// </summary>
    public class GlobalOptions
    {
        /// <summary>
        /// Базов адрес за публичен достъп до ПЕАУ.
        /// </summary>
        public string GL_EAU_PUBLIC_APP { get; set; }

        /// <summary>
        /// Базов адрес на сървър за автентикация.
        /// </summary>
        public string GL_IDSRV_URL { get; set; }

        /// <summary>
        /// Големина на буфер за копиране на данни. Използва при прехвърляне на данни, като това е размера на буфера в байтове.
        /// </summary>
        public int GL_COPY_BUFFER_SIZE { get; set; }

        /// <summary>
        /// Брой опити за извикване на услуга.
        /// </summary>
        public int GL_API_TRY_COUNT { get; set; }

        /// <summary>
        /// Период от време за изчакване между два опита за извикване на услуга.
        /// </summary>
        public TimeSpan GL_API_RETRY_INTERVAL { get; set; }

        /// <summary>
        /// Период от за опресняване на данни. Използва при опресняване на номеклатури, системни параметри.
        /// </summary>
        public TimeSpan GL_POLLING_INTERVAL { get; set; }

        /// <summary>
        /// Допустими типове файлове с документи, които могат да се прикачат.
        /// </summary>
        public string GL_DOCUMENT_ALLOWED_FORMATS { get; set; }

        /// <summary>
        /// Домейн адрес за сетване в cookie.
        /// </summary>
        public string GL_COMMON_COOKIE_DOMAIN { get; set; }

        /// <summary>
        /// Допустим размер на прикачен файл с документ в KB.
        /// </summary>
        public string GL_DOCUMENT_MAX_FILE_SIZE { get; set; }

        /// <summary>
        /// Допустим размер на заявлението в KB.
        /// </summary>
        public int GL_APPLICATION_MAX_SIZE { get; set; }

        /// <summary>
        /// Период от време за изчакване на потвърждение от потребител при стартиран процес за регистрация на профил или смяна на парола.
        /// </summary>
        public TimeSpan GL_USR_PROCESS_CONFIRM_PERIOD { get; set; }

        /// <summary>
        /// Базов адрес за вътрешен достъп до услуги на ПЕАУ.
        /// </summary>
        public string GL_EAU_PRIVATE_API { get; set; }

        /// <summary>
        /// Име на административна структура, предоставяща услугите.
        /// </summary>
        public string GL_ADM_STRUCTURE_NAME { get; set; }

        /// <summary>
        /// Кратко име на административна структура, предоставяща услугите.
        /// </summary>
        public string GL_ADM_STRUCTURE_NAME_SHORT { get; set; }

        /// <summary>
        /// ЕИК на административна структура, предоставяща услугите.
        /// </summary>
        public string GL_ADM_STRUCTURE_UIC { get; set; }

        /// <summary>
        /// Идентификатор на ПЕП за плащане на задължения към АНД.
        /// </summary>
        public string GL_AND_PEP_CIN { get; set; }

        /// <summary>
        /// Допустими видове серии за фиш.
        /// </summary>
        public string POSSIBLE_KAT_OBLIGATIONS_FISH_SERIES { get; set; }

        /// <summary>
        /// Идентификатор на СУНАУ на услуга към АНД.
        /// </summary>
        public string AND_SERVICE_SUNAU_ID { get; set; }

        /// <summary>
        /// Адрес на WAIS_INTEGRATION_MOI_API.
        /// </summary>
        public string GL_WAIS_INTEGRATION_MOI_API { get; set; }

        /// <summary>
        /// Адрес на WAIS_INTEGRATION_NOTARY_API.
        /// </summary>
        public string GL_WAIS_INTEGRATION_NOTARY_API { get; set; }

        /// <summary>
        /// Адрес на GL_WAIS_INTEGRATION_REGIX_API.
        /// </summary>
        public string GL_WAIS_INTEGRATION_REGIX_API { get; set; }

        /// <summary>
        /// Плащане по док. {0} за усл. {1}
        /// </summary>
        public string GL_SERVICE_INSTANCE_PAYMENT_REASON { get; set; }

        /// <summary>
        /// Разрешен интервал за неактивност от страна на потребителя.
        /// </summary>
        public TimeSpan GL_EAU_USR_SESSION_INACTIVITY_INTERVAL { get; set; }
    }
}
