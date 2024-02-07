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
    internal class ServiceGroupDataContext : BaseDataContext
    {
        public ServiceGroupDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<ServiceGroup> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            string p_ids,
            int? p_language_id,
            bool? p_force_translated,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_ids", p_ids);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_force_translated", p_force_translated);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<ServiceGroup>("[nom].[p_n_d_service_groups_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        public async Task<int?> CreateAsync(
                     string p_name,
                     int? p_order_number,
                     string p_icon_name,
                     bool? p_is_active,
                     CancellationToken cancellationToken)
        {
            int? p_group_id = null;

            var parameters = new DynamicParameters();
            parameters.Add("p_group_id", p_group_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_name", p_name);            
            parameters.Add("p_order_number", p_order_number);
            parameters.Add("p_icon_name", p_icon_name);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_groups_create]", parameters, cancellationToken);

            p_group_id = parameters.Get<int?>("p_group_id");

            return p_group_id;
        }

        public async Task UpdateAsync(
            int? p_group_id,
            string p_name,
            int? p_order_number,
            string p_icon_name,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_group_id", p_group_id);
            parameters.Add("p_name", p_name);
            parameters.Add("p_order_number", p_order_number);
            parameters.Add("p_icon_name", p_icon_name);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_groups_update]", parameters, cancellationToken);
        }

        public async Task DeleteAsync(
            int? p_group_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_group_id", p_group_id);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_groups_delete]", parameters, cancellationToken);
        }

        public async Task ServiceGroupTranslationCreateAsync(
            int? p_group_id,
            int? p_language_id,
            string p_name,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_group_id", p_group_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_name", p_name);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_groups_i18n_create]", parameters, cancellationToken);
        }

        public async Task ServiceGroupTranslationUpdateAsync(
            int? p_group_id,
            int? p_language_id,
            string p_name,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_group_id", p_group_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_name", p_name);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_service_groups_i18n_update]", parameters, cancellationToken);
        }
    }
}
