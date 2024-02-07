using CNSys.Data;
using Dapper;
using EAU.Audit.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Audit.Repositories
{
    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с одит
    /// </summary>
    internal class LogActionDataContext : BaseDataContext
    {
        public LogActionDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<long?> LogActionCreateAsync(
                      DateTime? p_log_action_date,
                      short? p_object_type_id,
                      short? p_action_type_id,
                      short? p_functionality_id,
                      string p_key,
                      Guid? p_login_session_id,
                      int? p_user_id,
                      string p_user_email,
                      byte[] p_ip_address,
                      AdditionalData p_additional_data,
                      CancellationToken cancellationToken)
        {
            long? p_log_action_id = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_log_action_id", p_log_action_id, DbType.Int64, ParameterDirection.Output);
            parameters.Add("p_log_action_date", p_log_action_date);
            parameters.Add("p_object_type_id", p_object_type_id);
            parameters.Add("p_action_type_id", p_action_type_id);
            parameters.Add("p_functionality_id", p_functionality_id);
            parameters.Add("p_login_session_id", p_login_session_id);
            parameters.Add("p_key", p_key);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_user_email", p_user_email);
            parameters.Add("p_ip_address", p_ip_address, DbType.Binary);
            parameters.Add("p_additional_data", p_additional_data);

            await _dbContext.SPExecuteAsync("[audit].[p_log_actions_create]", parameters, cancellationToken);

            p_log_action_id = parameters.Get<long?>("p_log_action_id");

            return p_log_action_id;
        }

        public async Task<(IEnumerable<LogAction> reader, int? count)> LogActionSearchAsync(
                       short? p_search_mode,
                       int[] p_log_action_ids,
                       DateTime? p_log_action_date_from,
                       DateTime? p_log_action_date_to,
                       short? p_object_type_id,
                       short? p_action_type_id,
                       short? p_functionality_id,
                       string p_key,
                       int? p_user_id,
                       byte[] p_ip_address,
                       int? p_start_index,
                       int? p_page_size,
                       bool? p_calculate_count,
                       CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_search_mode", p_search_mode);
            parameters.Add("p_log_action_ids", p_log_action_ids);
            parameters.Add("p_log_action_date_from", p_log_action_date_from);
            parameters.Add("p_log_action_date_to", p_log_action_date_to);
            parameters.Add("p_object_type_id", p_object_type_id);
            parameters.Add("p_action_type_id", p_action_type_id);
            parameters.Add("p_functionality_id", p_functionality_id);
            parameters.Add("p_key", p_key);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_ip_address", p_ip_address, DbType.Binary);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<LogAction>("[audit].[p_log_actions_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }

        public async Task<long> DocumentAccessedDataCreateAsync(
            string p_document_uri,
            int? p_document_type_id,
            string p_applicant_names,
            string p_applicant_identifier,
            byte[] p_ip_address,
            CancellationToken cancellationToken)
        {
            long? p_id_out = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_document_uri", p_document_uri);
            parameters.Add("p_document_type_id", p_document_type_id);
            parameters.Add("p_applicant_names", p_applicant_names);
            parameters.Add("p_applicant_identifier", p_applicant_identifier);
            parameters.Add("p_ip_address", p_ip_address, DbType.Binary);
            parameters.Add("p_id_out", p_id_out, DbType.Int64, ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[audit].[p_document_accessed_data_create]", parameters, cancellationToken);

            p_id_out = parameters.Get<long?>("p_id_out");

            return p_id_out.Value;
        }

        public Task DocumentAccessedDataValueCreateAsync(
            long p_document_accessed_data_id,
            byte? p_data_type,
            string p_data_value,
            CancellationToken cancellationToken)
        {
            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_document_accessed_data_id", p_document_accessed_data_id);
            parameters.Add("p_data_type", p_data_type);
            parameters.Add("p_data_value", p_data_value);

            return _dbContext.SPExecuteAsync("[audit].[p_document_accessed_data_value_create]", parameters, cancellationToken);
        }
    }
}
