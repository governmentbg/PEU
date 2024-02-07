using EAU.Utilities;
using System;

namespace EAU.Payments.RegistrationsData.Models
{
    /// <summary>
    /// Регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    public class RegistrationData
    {
        /// <summary>
        /// Идентификаор на регистрационните данни.
        /// </summary>
        [DapperColumn("registration_data_id")]
        public int? RegistrationDataID { get; set; }

        /// <summary>
        /// Тип на регистрационните данни.
        /// </summary>
        [DapperColumn("type")]
        public RegistrationDataTypes? Type { get; set; }

        /// <summary>
        /// Описание на вид на плащане.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// IBAN.
        /// </summary>
        [DapperColumn("iban")]
        public string IBAN { get; set; }

        /// <summary>
        /// КИН на ПЕАУ - клиентски номер в личните данни на търговеца.
        /// </summary>
        [DapperColumn("cin")]
        public string Cin { get; set; }

        /// <summary>
        /// E-mail на ПЕАУ по регистрация.
        /// </summary>
        [DapperColumn("email")]
        public string Email { get; set; }

        /// <summary>
        /// Буквено цифрова секретна дума генерирана от ePay.
        /// </summary>
        [DapperColumn("secret_word")]
        public string SecretWord { get; set; }

        /// <summary>
        /// Период на валидност на плащане - необходим за определяне на крайната дата и час на валидността на плащането през оператора.
        /// </summary>
        [DapperColumn("validity_period")]
        public TimeSpan? ValidityPeriod { get; set; }

        /// <summary>
        ///Адрес за достъп.
        /// </summary>
        [DapperColumn("portal_url")]
        public string PortalUrl { get; set; }

        /// <summary>
        /// Електронен адрес за уведомяване - електронен адрес, на който се изпраща съобщение за променен статус на заявка за плащане.
        /// </summary>
        [DapperColumn("notification_url")]
        public string NotificationUrl { get; set; }

        /// <summary>
        /// URL за достъп до услугите на ПЕП на ДАЕУ.
        /// </summary>
        [DapperColumn("service_url")]
        public string ServiceUrl { get; set; }
    }
}
