using CNSys.Data;
using Dapper;
using EAU.Signing.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Signing.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани с процесите по подписване.
    /// </summary>
    internal class SigningDataContext : BaseDataContext
    {
        public SigningDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Signers

        /// <summary>
        /// Създава запис за подписващ в базата.
        /// </summary>
        /// <param name="p_process_id">Връзка към процеса за подписване.</param>
        /// <param name="p_name">Име на подписващия.</param>
        /// <param name="p_ident">ЕГМ/ЛНЧ на подписващия.</param>
        /// <param name="p_order">Поредност на полагане на подписа.</param>
        /// <param name="cancellationToken"></param>
        public async Task<long?> SignerCreateAsync(Guid? p_process_id
                                 ,string p_name
                                 ,string p_ident
                                 ,int? p_order
                                 ,CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_signer_id", dbType: DbType.Int64, direction: ParameterDirection.Output);
            parameters.Add("@p_process_id", p_process_id);
            parameters.Add("@p_name", p_name);
            parameters.Add("@p_ident", p_ident);
            parameters.Add("@p_order", p_order);          
            
            await _dbContext.SPExecuteAsync("[sign].[p_signers_create]", parameters, cancellationToken);

            long? id = parameters.Get<long?>("@p_signer_id");

            return id;
        }

        /// <summary>
        /// Редактира запис зза подписващ в базата.
        /// </summary>
        /// <param name="p_signer_id">Идентификатор на подписващия.</param>
        /// <param name="p_status">Статус на подписващия.</param>
        /// <param name="p_signing_channel">Канал на подписване.</param>
        /// <param name="p_additional_sign_data">Допълнителни данни за отдалечено подписване.</param>
        /// <param name="p_transaction_id"></param>
        /// <param name="p_reject_reson_label"></param>
        /// <param name="cancellationToken"></param>
        public async Task SignerUpdateAsync(long? p_signer_id,
            short? p_status,
            short? p_signing_channel,
            RemoteSignRequestAdditionalData p_additional_sign_data,
            string p_transaction_id,
            string p_reject_reson_label,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_signer_id", p_signer_id);
            parameters.Add("@p_status", p_status);
            parameters.Add("@p_signing_channel", p_signing_channel);
            parameters.Add("@p_additional_sign_data", p_additional_sign_data);
            parameters.Add("@p_transaction_id", p_transaction_id);
            parameters.Add("@p_reject_reson_label", p_reject_reson_label);

            await _dbContext.SPExecuteAsync("[sign].[p_signers_update]", parameters, cancellationToken);
        }

        /// <summary>
        ///  Изтрива запис за подписващ в базата.
        /// </summary>
        /// <param name="p_signer_id">Идентификатор на подписващия.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SignerDeleteAsync(long? p_signer_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_signer_id", p_signer_id);

            await _dbContext.SPExecuteAsync("[sign].[p_signers_delete]", parameters, cancellationToken);
        }

        /// <summary>
        /// Търси подписващи по подадени критерии.
        /// </summary>
        /// <param name="p_signer_id">Идентификатор на подписващия.</param>
        /// <param name="p_process_ids">Списък с идентификатори на процеси за подписване.</param>
        /// <param name="p_signing_channel"></param>
        /// <param name="p_transaction_id"></param>
        /// <param name="p_start_index"></param>
        /// <param name="p_page_size"></param>
        /// <param name="p_calculate_count"></param>
        /// <param name="p_max_nor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<Signer> signers, int? count)> SignersSearchAsync(long? p_signer_id,
                                             List<Guid> p_process_ids,
                                             short? p_signing_channel,
                                             string p_transaction_id,
                                             int? p_start_index,
                                             int? p_page_size,
                                             bool? p_calculate_count,
                                             CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Guid));

            if (p_process_ids != null && p_process_ids.Count > 0) 
            {
                p_process_ids.ForEach(g => dt.Rows.Add(g));
            }

            parameters.Add("@p_signer_id", p_signer_id);
            parameters.Add("@p_process_ids", dt.AsTableValuedParameter("[dbo].[tt_guids]"));
            parameters.Add("@p_signing_channel", p_signing_channel);
            parameters.Add("@p_transaction_id", p_transaction_id);
            parameters.Add("@p_start_index", p_start_index);
            parameters.Add("@p_page_size", p_page_size);
            parameters.Add("@p_calculate_count", p_calculate_count);
            parameters.Add("@p_count", dbType: DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var signers = await _dbContext.SPQueryAsync<Signer>("[sign].[p_signers_search]", parameters, cancellationToken);

            var p_count = parameters.Get<int?>("@p_count");

            return (signers, p_count);
        }

        #endregion

        #region SigningProcesses

        /// <summary>
        /// Създава запис за процес по подписване в базата.
        /// </summary>
        /// <param name="p_process_id">Идентификатор на процес за подписване.</param>
        /// <param name="p_format">Формат.</param>
        /// <param name="p_file_name">Име на файла за подписване.</param>
        /// <param name="p_content_type">Mime тип на файла за подписване. </param>
        /// <param name="p_level">Ниво на подписа.</param>
        /// <param name="p_type">Тип на пакетиране на подписа.</param>
        /// <param name="p_digest_method">Хеш алгоритъм на подписа.</param>
        /// <param name="p_rejected_callback_url"></param>
        /// <param name="p_completed_callback_url"></param>
        /// <param name="p_additional_data"></param>
        /// <param name="cancellationToken"></param>
        public async Task SigningProcessCreateAsync(Guid? p_process_id,
                                         short? p_format,
                                         string p_file_name,
                                         string p_content_type,
                                         short? p_level,
                                         short? p_type,
                                         short? p_digest_method,
                                         string p_rejected_callback_url,
                                         string p_completed_callback_url,
                                         SignProcessAdditionalData p_additional_data,
                                         CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_process_id", p_process_id);
            parameters.Add("@p_format", p_format);
            parameters.Add("@p_file_name", p_file_name);
            parameters.Add("@p_content_type", p_content_type);
            parameters.Add("@p_level", p_level);
            parameters.Add("@p_type", p_type);
            parameters.Add("@p_digest_method", p_digest_method);
            parameters.Add("@p_rejected_callback_url", p_rejected_callback_url);
            parameters.Add("@p_completed_callback_url", p_completed_callback_url);
            parameters.Add("@p_additional_data", p_additional_data);

            await _dbContext.SPExecuteAsync("[sign].[p_signing_process_create]", parameters, cancellationToken);
        }

        /// <summary>
        /// Редактира запис за процес по подписване в базата.
        /// </summary>
        /// <param name="p_process_id">Идентификатор на процес за подписване.</param>
        /// <param name="p_status">Статус на процес по подписване.</param>
        /// <param name="p_additional_data"></param>
        /// <param name="cancellationToken"></param>
        public Task SigningProcessUpdateAsync(Guid? p_process_id, short? p_status, SignProcessAdditionalData p_additional_data, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_process_id", p_process_id);
            parameters.Add("@p_status", p_status);
            parameters.Add("@p_additional_data", p_additional_data);

            return _dbContext.SPExecuteAsync("[sign].[p_signing_process_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<SigningProcess> processes, int? count)> SigningProcessSearchAsync(
            Guid? p_process_id,
            short? p_status,
            bool? p_with_tx_lock,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,         
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_process_id", p_process_id);
            parameters.Add("@p_status", p_status);
            parameters.Add("@p_with_tx_lock", p_with_tx_lock);
            parameters.Add("@p_start_index", p_start_index);
            parameters.Add("@p_page_size", p_page_size);
            parameters.Add("@p_calculate_count", p_calculate_count);
            parameters.Add("@p_count", dbType: DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var processes = await _dbContext.SPQueryAsync<SigningProcess>("[sign].[p_signing_process_search]", parameters, cancellationToken);

            var p_count = parameters.Get<int?>("@p_count");

            return (processes, p_count);
        }

        /// <summary>
        /// Изтрива запис за процес по подписване в базата.
        /// </summary>
        /// <param name="p_process_id">Идентификатор на процес за подписване.</param>
        /// <param name="cancellationToken"></param>
        public Task SigningProcessDeleteAsync(Guid? p_process_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_process_id", p_process_id);

            return _dbContext.SPExecuteAsync("[sign].[p_signing_process_delete]", parameters, cancellationToken);
        }

        public Task SigningProcessesDeleteAllAsync(Guid[] p_process_ids, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Guid));

            if (p_process_ids != null && p_process_ids.Length > 0)
            {
                for (var i = 0; i < p_process_ids.Length; i++) 
                {
                    dt.Rows.Add(p_process_ids[i]);
                }
            }

            parameters.Add("@p_process_ids", dt.AsTableValuedParameter("[dbo].[tt_guids]"));

            return _dbContext.SPExecuteAsync("[sign].[p_signing_process_delete_all]", parameters, cancellationToken);
        }

        /// <summary>
        /// Създава или редактира запис за съдържанието на процес по подписване в базата.
        /// </summary>
        /// <param name="p_process_id">Идентификатор на процес за подписване.</param>
        /// <param name="p_content">Съдържание на документа за подписване.</param>
        /// <param name="p_offset">Изместване.</param>
        /// <param name="p_length">Брой байтове.</param>
        /// <param name="cancellationToken"></param>
        public Task SigningProcessContentUploadAsync(Guid? p_process_id,
                                                    byte[] p_content,
                                                    int? p_length,
                                                    CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_process_id", p_process_id);
            parameters.Add(name: "@p_content", value: p_content, dbType: DbType.Binary, size: p_length.Value);

            return _dbContext.SPExecuteAsync("[sign].[p_signing_process_content_upload]", parameters, cancellationToken);
        }

        public static Stream CreateSigningProcessContenStream(Guid processID, IDbContextProvider dbContextProvider)
        {
            return new SqlDeferredInitializedStream((context) =>
            {
                using (SigningDataContext dataContext = new SigningDataContext(context))
                {
                    return dataContext.SigningProcessContenReadAsync(processID, CancellationToken.None).GetAwaiter().GetResult();
                }
            }, dbContextProvider);
        }

        /// <summary>
        /// Прочита съдържанието на процес по подписване в базата.
        /// </summary>
        /// <param name="p_process_id">Идентификатор на процес за подписване.</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<IDataReader> SigningProcessContenReadAsync(Guid p_process_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@p_process_id", p_process_id);
            IDataReader reader = await _dbContext.SPExecuteReaderAsync("[sign].[p_signing_process_content_read]", parameters, cancellationToken);

            return reader;
        }

        #endregion
    }
}
