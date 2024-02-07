using Dapper;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text.Json;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Класът капсулира данни за процес по подписване.
    /// </summary>
    public class SigningProcess
    {
        /// <summary>
        /// Идентификатор на процес за подписване.
        /// </summary>
        [DapperColumn("process_id")]
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Съдържание на документа за подписване.
        /// </summary>
        [DapperColumn("content")]
        public Stream Content { get; set; }

        /// <summary>
        /// Формат на подписване.
        /// </summary>
        [DapperColumn("format")]
        public SigningFormats? Format { get; set; }
      
        /// <summary>
        /// Статус на процес за подписване.
        /// </summary>
        [DapperColumn("status")]
        public SigningRequestStatuses? Status { get; set; }

        /// <summary>
        /// Име на файла за подписване. 
        /// </summary>
        [DapperColumn("file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Mime тип на файла за подписване. 
        /// </summary>
        [DapperColumn("content_type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Ниво на подписване.
        /// </summary>
        [DapperColumn("level")]
        public SigningLevels? Level { get; set; }

        /// <summary>
        /// Тип на пакетиране на подписа.
        /// </summary>
        [DapperColumn("type")]
        public SigningPackingTypes? Type { get; set; }

        /// <summary>
        /// Хеш алгоритъм на подписа.
        /// </summary>
        [DapperColumn("digest_method")]
        public DigestMethods? DigestMethod { get; set; }

        /// <summary>
        /// Адрес за известяване при прекратен процес по подписване.
        /// </summary>
        [DapperColumn("rejected_callback_url")]
        public string RejectedCallbackUrl { get; set; }

        /// <summary>
        /// Адрес за известяване при успешно завършен процес по подписване.
        /// </summary>
        [DapperColumn("completed_callback_url")]
        public string CompletedCallbackUrl { get; set; }

        /// <summary>
        /// Идентификатор на предварително дефиниран клиент за обратно известяване.
        /// </summary>
        [DapperColumn("callback_client_config_id")]
        public int? CallbackClientId { get; set; }

        /// <summary>
        /// Име на предварително дефиниран клиент за обратно известяване.
        /// </summary>
        [DapperColumn("callback_http_client_name")]
        public string CallbackHttpClientName { get; set; }

        /// <summary>
        /// Допълнителни данни за процеса по подписване. Данните трябва да бъдат в Json формат.
        /// </summary>
        [DapperColumn("additional_data")]
        public SignProcessAdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Списък с подписващи към процеса.
        /// </summary>
        public List<Signer> Signers { get; set; }
    }

    public class SessionData
    {
        public Guid? UserSessionID { get; set; }
        public Guid? LoginSessionID { get; set; }
        public string IpAddress { get; set; }
        public int? UserCIN { get; set; }
    }

    public class SignProcessAdditionalData 
    {
        public SessionData SessionData { get; set; }
        public string SignatureXpath { get; set; }
        public Dictionary<string, string> SignatureXPathNamespaces { get; set; }
    }

    public class SignProcessAdditionalDataDapperMapHandler : SqlMapper.TypeHandler<SignProcessAdditionalData>
    {
        public override void SetValue(IDbDataParameter parameter, SignProcessAdditionalData value)
        {
            parameter.DbType = DbType.String;

            if (value == null)
            {
                parameter.Value = (object)DBNull.Value;
            }
            else
            {
                string json = EAUJsonSerializer.Serialize(value);
                parameter.Value = json;
            }
        }

        public override SignProcessAdditionalData Parse(object value)
        {
            if (value == null)
            {
                return null;
            }

            string json = value.ToString();
            SignProcessAdditionalData result = EAUJsonSerializer.Deserialize<SignProcessAdditionalData>(json);

            return result;
        }
    }
}
