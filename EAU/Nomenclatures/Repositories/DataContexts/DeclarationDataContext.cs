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
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с декларативни обстоятелства и политики
    /// </summary>
    internal class DeclarationDataContext : BaseDataContext
    {
        public DeclarationDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<Declaration> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            string p_ids,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_ids", p_ids);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<Declaration>("[nom].[p_n_d_declarations_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        public async Task<int?> CreateAsync(
           string p_description,
           string p_content,
           bool? p_is_required,
           bool? p_is_additional_description_required,
           string p_code,
           CancellationToken cancellationToken)
        {
            int? p_delcaration_id = null;

            var parameters = new DynamicParameters();
            parameters.Add("p_delcaration_id", p_delcaration_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_description", p_description);
            parameters.Add("p_content", p_content);
            parameters.Add("p_is_required", p_is_required);
            parameters.Add("p_is_additional_description_required", p_is_additional_description_required);
            parameters.Add("p_code", p_code);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_declarations_create]", parameters, cancellationToken);

            p_delcaration_id = parameters.Get<int?>("p_delcaration_id");

            return p_delcaration_id;
        }

        public async Task UpdateAsync(
            int? p_delcaration_id,
            string p_description,
            string p_content,
            bool? p_is_required,
            bool? p_is_additional_description_required,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_delcaration_id", p_delcaration_id);
            parameters.Add("p_description", p_description);
            parameters.Add("p_content", p_content);
            parameters.Add("p_is_required", p_is_required);
            parameters.Add("p_is_additional_description_required", p_is_additional_description_required);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_declarations_update]", parameters, cancellationToken);
        }

        public async Task DeleteAsync(
            int? p_delcaration_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_delcaration_id", p_delcaration_id);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_declarations_delete]", parameters, cancellationToken);
        }
    }
}
