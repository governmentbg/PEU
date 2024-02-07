using CNSys.Data;
using Dapper;
using EAU.Nomenclatures.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Nomenclatures.Repositories
{
    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с езици за превод
    /// </summary>
    internal class LanguageDataContext : BaseDataContext
    {
        public LanguageDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int?> CreateAsync(
            string p_code,
            string p_name,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            int? p_language_id = null;

            var parameters = new DynamicParameters();
            parameters.Add("p_code", p_code);
            parameters.Add("p_name", p_name);
            parameters.Add("p_is_active", p_is_active);
            parameters.Add("p_language_id", p_language_id, DbType.Int32, ParameterDirection.Output);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_languages_create]", parameters, cancellationToken);

            p_language_id = parameters.Get<int?>("p_language_id");
            return p_language_id;
        }

        public async Task UpdateAsync(
            int? p_language_id,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_languages_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<Language> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            int? p_language_id,
            string p_code,
            string p_name,
            bool? p_is_active,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_code", p_code);
            parameters.Add("p_name", p_name);
            parameters.Add("p_is_active", p_is_active);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var result = await _dbContext.SPQueryAsync<Language>("[nom].[p_n_d_languages_search]", parameters, cancellationToken);

            var p_count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (result, p_count, p_last_updated_on);
        }
    }
}
