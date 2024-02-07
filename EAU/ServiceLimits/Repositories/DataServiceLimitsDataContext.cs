using CNSys.Data;
using Dapper;
using EAU.ServiceLimits.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.ServiceLimits.Repositories
{
    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с лимити.
    /// </summary>
    internal class DataServiceLimitsDataContext : BaseDataContext
    {
        public DataServiceLimitsDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        #region DataServiceLimit

        public Task DataServiceLimitUpdateAsync(
            string p_service_code,
            DateTime p_requests_interval,
            int p_requests_number,
            int p_status,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_service_code", p_service_code);
            parameters.Add("p_requests_interval", p_requests_interval);
            parameters.Add("p_requests_number", p_requests_number);
            parameters.Add("p_status", p_status);

            return _dbContext.SPExecuteAsync("[dbo].[p_data_service_limits_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<DataServiceLimit> reader, int? count, DateTime? p_last_updated_on)> DataServiceLimitsSearchAsync(
            string p_service_limit_ids,
            string p_service_code,
            string p_service_name,
            int? p_status,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_service_limit_ids", p_service_limit_ids);
            parameters.Add("p_service_code", p_service_code);
            parameters.Add("p_service_name", p_service_name);
            parameters.Add("p_status", p_status);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<DataServiceLimit>("[dbo].[p_data_service_limits_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        #endregion

        #region DataServiceUserLimit

        public async Task<int?> DataServiceUserLimitCreateAsync(
                        int? p_service_limit_id,
                        int? p_user_id,
                        DateTime p_requests_interval,
                        int p_requests_number,
                        int p_status,
                        CancellationToken cancellationToken)
        {
            int? p_user_limit_id = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_user_limit_id", p_user_limit_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_service_limit_id", p_service_limit_id);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_requests_interval", p_requests_interval);
            parameters.Add("p_requests_number", p_requests_number);
            parameters.Add("p_status", p_status);
            

            await _dbContext.SPExecuteAsync("[dbo].[p_data_service_user_limits_create]", parameters, cancellationToken);

            p_user_limit_id = parameters.Get<int?>("p_user_limit_id");

            return p_user_limit_id;
        }

        public Task DataServiceUserLimitUpdateAsync(
            int? p_user_limit_id,
            int? p_service_limit_id,
            int? p_user_id,
            DateTime p_requests_interval,
            int p_requests_number,
            int p_status,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_limit_id", p_user_limit_id);
            parameters.Add("p_service_limit_id", p_service_limit_id);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_requests_interval", p_requests_interval);
            parameters.Add("p_requests_number", p_requests_number);
            parameters.Add("p_status", p_status);

            return _dbContext.SPExecuteAsync("[dbo].[p_data_service_user_limits_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<DataServiceUserLimit> reader, int? count, DateTime? p_last_updated_on)> DataServiceUserLimitsSearchAsync(
           string p_user_limit_ids,
           int? p_service_limit_id,
           int? p_user_id,
           int? p_status,
           int? p_start_index,
           int? p_page_size,
           bool? p_calculate_count,
           CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_user_limit_ids", p_user_limit_ids);
            parameters.Add("p_service_limit_id", p_service_limit_id);
            parameters.Add("p_user_id", p_user_id);
            parameters.Add("p_status", p_status);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<DataServiceUserLimit>("[dbo].[p_data_service_user_limits_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        #endregion
    }
}
