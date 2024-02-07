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
    //TODO_DBRead

    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с 
    /// </summary>
    internal class LabelDataContext : BaseDataContext
    {
        public LabelDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task LabelUpdateAsync(
            int? p_label_id,
            string p_code,
            string p_value,
            string p_description,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_label_id", p_label_id);
            parameters.Add("p_code", p_code);
            parameters.Add("p_value", p_value);
            parameters.Add("p_description", p_description);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_labels_update]", parameters, cancellationToken);
        }

        public async Task LabelTranslationCreateAsync(
            int? p_label_id,
            int? p_language_id,
            string p_value,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_label_id", p_label_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_value", p_value);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_labels_i18n_create]", parameters, cancellationToken);
        }

        public async Task LabelTranslationUpdateAsync(
            int? p_label_id,
            int? p_language_id,
            string p_value,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_label_id", p_label_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_value", p_value);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_labels_i18n_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<Label> reader, int? p_count, DateTime? p_last_updated_on)> SearchAsync(
            string p_label_ids,
            int? p_language_id,
            string p_code,
            string p_value,
            bool? p_load_description,
            bool? p_load_only_untranslated,
            bool? p_force_translated,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_label_ids", p_label_ids);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_code", p_code);
            parameters.Add("p_value", p_value);
            parameters.Add("p_load_description", p_load_description);
            parameters.Add("p_load_only_untranslated", p_load_only_untranslated);
            parameters.Add("p_force_translated", p_force_translated);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<Label>("[nom].[p_n_d_labels_search]", parameters, cancellationToken);
            
            var p_count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, p_count, p_last_updated_on);
        }
    }
}
