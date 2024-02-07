using Dapper;
using EAU.Utilities;
using System;
using System.Data;
using System.Text.Json;

namespace EAU.Signing.Models
{
    /// <summary>
    /// Класът капсулира данни за подписващ, учатващ в процеса по подписване.
    /// </summary>
    public class Signer
    {
        /// <summary>
        /// Идентификатор на подписващ.
        /// </summary>
        [DapperColumn("signer_id")]
        public long? SignerID { get; set; }

        /// <summary>
        /// Име на подписващ.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// ЕГН/ЛНЧ на подписващ.
        /// </summary>
        [DapperColumn("ident")]
        public string Ident { get; set; }

        /// <summary>
        /// Ред за полагане на подпис.
        /// </summary>
        [DapperColumn("order")]
        public short? Order { get; set; }

        /// <summary>
        /// Статус на полагане на подпис.
        /// </summary>
        [DapperColumn("status")]
        public SignerSigningStatuses? Status { get; set; }

        /// <summary>
        /// Канал за подписване.
        /// </summary>
        [DapperColumn("signing_channel")]
        public SigningChannels? SigningChannel { get; set; }

        /// <summary>
        /// Идентификатор на заявката за подписване.
        /// </summary>
        [DapperColumn("process_id")]
        public Guid? ProcessID { get; set; }

        /// <summary>
        /// Допълнителни данни при отдалечено подписване. Данните трябва да бъдат в Json формат.
        /// </summary>
        [DapperColumn("additional_sign_data")]
        public RemoteSignRequestAdditionalData SigningAdditionalData { get; set; }

        /// <summary>
        /// Идентификатор на процеса по подписване в системата на доставчика на услугата по отдалечено подписване.
        /// </summary>
        [DapperColumn("transaction_id")]
        public string TransactionID { get; set; }

        /// <summary>
        /// Причина за отказ.
        /// </summary>
        [DapperColumn("reject_reson_label")]
        public string RejectReson { get; set; }
    }

    /// <summary>
    /// Статуси на полагане на подпис.: 0 = В изчакване.; 1 = Започнато подписване.; 2 = Подписан.;
    /// </summary>
    public enum SignerSigningStatuses
    {
        /// <summary>
        /// В изчакване.
        /// </summary>
        Waiting = 0,

        /// <summary>
        /// Започнато подписване.
        /// </summary>
        StartSigning = 1,

        /// <summary>
        /// Подписан.
        /// </summary>
        Signed = 2
    }

    /// <summary>
    /// Канали за подписване.: 0 = Btrust локално.; 1 = Btrust отдалечено.; 2 = Evrotrust отдалечено.;
    /// </summary>
    public enum SigningChannels
    {
        /// <summary>
        /// Btrust локално.
        /// </summary>
        BtrustLocal = 0,

        /// <summary>
        /// Btrust отдалечено.
        /// </summary>
        BtrustRemote = 1,

        /// <summary>
        /// Evrotrust отдалечено.
        /// </summary>
        EvrotrustRemote = 2
    }

    public class RemoteSignRequestAdditionalDataDapperMapHandler : SqlMapper.TypeHandler<RemoteSignRequestAdditionalData>
    {
        public override void SetValue(IDbDataParameter parameter, RemoteSignRequestAdditionalData value)
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

        public override RemoteSignRequestAdditionalData Parse(object value)
        {
            if (value == null)
            {
                return null;
            }

            string json = value.ToString();
            RemoteSignRequestAdditionalData result = EAUJsonSerializer.Deserialize<RemoteSignRequestAdditionalData>(json);

            return result;
        }
    }
}
