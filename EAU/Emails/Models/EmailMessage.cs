using Dapper;
using EAU.Utilities;
using System;
using System.Data;
using System.Text.Json;

namespace EAU.Emails.Models
{
    /// <summary>
    /// Имейл съобщение.
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Идентификатор на съобщение.
        /// </summary>
        [DapperColumn("email_id")]
        public int? EmailID { get; set; }

        /// <summary>
        /// Приоритет.
        /// </summary>
        [DapperColumn("priority")]
        public EmailPriority? Priority { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [DapperColumn("status")]
        public EmailStatues? Status { get; set; }

        /// <summary>
        /// Брой направени опити за изпращане - разлика спрямо фиксиран максимален брой възможни опити.
        /// </summary>
        [DapperColumn("try_count")]
        public int? TryCount { get; set; }

        /// <summary>
        /// Дата и час на изпращане.
        /// </summary>
        [DapperColumn("send_date")]
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Тема.
        /// </summary>
        [DapperColumn("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Тяло на съобщението.
        /// </summary>
        [DapperColumn("body")]
        public string Body { get; set; }

        /// <summary>
        /// Флаг, указващ дали съдържанието е HTML.
        /// </summary>
        [DapperColumn("is_body_html")]
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Име на изпращаща услуга.
        /// </summary>
        [DapperColumn("sending_provider_name")]
        public string SendingProviderName { get; set; }

        /// <summary>
        /// Получатели на имейла.
        /// </summary>
        [DapperColumn("recipients")]
        public string RecipientsJSONSerialized { get; set; }

        public EmailRecipient[] Recipients { get; set; }
    }

    public class EmailRecipientDapperMapHandler : SqlMapper.TypeHandler<EmailRecipient[]>
    {
        public override void SetValue(IDbDataParameter parameter, EmailRecipient[] value)
        {
            string json = EAUJsonSerializer.Serialize<EmailRecipient[]>(value);
            parameter.Value = json;
        }

        public override EmailRecipient[] Parse(object value)
        {
            string json = value.ToString();
            EmailRecipient[] result = EAUJsonSerializer.Deserialize<EmailRecipient[]>(json);

            return result;
        }
    }
}