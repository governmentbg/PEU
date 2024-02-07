using CNSys.Data;
using Dapper;
using EAU.Nomenclatures.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с видове документи
    /// </summary>
    internal class ServiceTermDataContext : BaseDataContext
    {
        public ServiceTermDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<ServiceTerm> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
          int? p_service_id,
          int? p_start_index,
          int? p_page_size,
          bool? p_calculate_count,
          CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<ServiceTerm>("[nom].[p_n_d_service_terms_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        public async Task<int?> CreateAsync(
            int? p_service_id,
            short? p_service_term_type,
            decimal? p_price,
            int? p_execution_period,
            string p_description,
            short? p_period_type,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            int? p_service_term_id = null;

            var parameters = new DynamicParameters();
            parameters.Add("p_service_term_id", p_service_term_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_service_term_type", p_service_term_type);
            parameters.Add("p_price", p_price);
            parameters.Add("p_execution_period", p_execution_period);
            parameters.Add("p_description", p_description);
            parameters.Add("p_period_type", p_period_type);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_terms_create]", parameters, cancellationToken);

            p_service_term_id = parameters.Get<int?>("p_service_term_id");

            return p_service_term_id;
        }

        public async Task UpdateAsync(
            int? p_service_term_id,
            int? p_service_id,
            short? p_service_term_type,
            decimal? p_price,
            int? p_execution_period,
            string p_description,
            short? p_period_type,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_service_term_id", p_service_term_id);
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_service_term_type", p_service_term_type);
            parameters.Add("p_price", p_price);
            parameters.Add("p_execution_period", p_execution_period);
            parameters.Add("p_description", p_description);
            parameters.Add("p_period_type", p_period_type);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_terms_update]", parameters, cancellationToken);
        }

        public async Task DeleteAsync(
            int? p_service_term_id,            
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_service_term_id", p_service_term_id);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_terms_delete]", parameters, cancellationToken);
        }
    }
}
