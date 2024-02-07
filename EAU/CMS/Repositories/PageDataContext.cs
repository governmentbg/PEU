using CNSys.Data;
using Dapper;
using EAU.CMS.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.CMS.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани със страници със съдържание.
    /// </summary>
    internal class PageDataContext : BaseDataContext
    {
        public PageDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Page

        public Task PageUpdateAsync(
            string p_code,
            string p_title,
            string p_content,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_code", p_code);
            parameters.Add("p_title", p_title);
            parameters.Add("p_content", p_content);

            return _dbContext.SPExecuteAsync("[cms].[p_pages_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<Page> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            string p_page_ids,
            int? p_language_id,
            bool? p_force_translated,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_page_ids", p_page_ids);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_force_translated", p_force_translated);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<Page>("[cms].[p_pages_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        #endregion

        #region Page Translation

        public async Task PageTranslationCreateAsync(
            int? p_page_id,
            int? p_language_id,
            string p_title,
            string p_content,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_page_id", p_page_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_title", p_title);
            parameters.Add("p_content", p_content);

            await _dbContext.SPExecuteAsync("[cms].[p_pages_i18n_create]", parameters, cancellationToken);
        }

        public async Task PageTranslationUpdateAsync(
            int? p_page_id,
            int? p_language_id,
            string p_title,
            string p_content,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_page_id", p_page_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_title", p_title);
            parameters.Add("p_content", p_content);

            await _dbContext.SPExecuteAsync("[cms].[p_pages_i18n_update]", parameters, cancellationToken);
        }

        #endregion
    }
}
