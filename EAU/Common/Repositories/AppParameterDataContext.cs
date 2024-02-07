using CNSys.Data;
using Dapper;
using EAU.Common.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Common.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани с конфигурационни параметри на системата.
    /// </summary>
    internal class AppParameterDataContext : BaseDataContext
    {
        public AppParameterDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task AppParameterUpdateAsync(
            string p_code,
            DateTime? p_value_date_time,
            DateTime? p_value_interval,
            string p_value_string,
            int? p_value_int,
            TimeSpan? p_value_hour,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_code", p_code);
            parameters.Add("p_value_date_time", p_value_date_time);
            parameters.Add("p_value_interval", p_value_interval);
            parameters.Add("p_value_string", p_value_string);
            parameters.Add("p_value_int", p_value_int);
            parameters.Add("p_value_hour", p_value_hour);

            return _dbContext.SPExecuteAsync("[dbo].[p_app_parameters_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<AppParameter> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            int? p_functionality_id,
            string p_code,
            bool? p_code_is_exact,
            string p_description,
	        bool? p_is_system,
            int? p_start_index,
	        int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_functionality_id", p_functionality_id);
            parameters.Add("p_code", p_code);
            parameters.Add("p_code_is_exact", p_code_is_exact);
            parameters.Add("p_description", p_description);
            parameters.Add("p_is_system", p_is_system);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<AppParameter>("[dbo].[p_app_parameters_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }
    }
}
