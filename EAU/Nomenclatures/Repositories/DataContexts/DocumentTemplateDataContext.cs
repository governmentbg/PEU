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
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с шаблон за документ
    /// </summary>
    internal class DocumentTemplateDataContext : BaseDataContext
    {
        public DocumentTemplateDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<DocumentTemplate> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            int? p_start_index,
            int? p_page_size,
            int? p_document_type_id,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_document_type_id", p_document_type_id);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = (await _dbContext.SPQueryAsync<DocumentTemplate>("[nom].[p_n_d_document_templates_search]", parameters, cancellationToken));

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        public async Task<int?> CreateAsync(
            int? p_document_type_id,
            string p_content,
            bool? p_is_submitted_according_to_template,
            CancellationToken cancellationToken)
        {
            int? p_doc_template_id = null;

            var parameters = new DynamicParameters();
            parameters.Add("p_doc_template_id", p_doc_template_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_document_type_id", p_document_type_id);
            parameters.Add("p_content", p_content);
            parameters.Add("p_is_submitted_according_to_template", p_is_submitted_according_to_template);            

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_document_templates_create]", parameters, cancellationToken);

            p_doc_template_id = parameters.Get<int?>("p_doc_template_id");

            return p_doc_template_id;
        }

        public async Task UpdateAsync(
            int? p_doc_template_id,
            int? p_document_type_id,
            string p_content,
            bool? p_is_submitted_according_to_template,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_doc_template_id", p_doc_template_id);
            parameters.Add("p_document_type_id", p_document_type_id);
            parameters.Add("p_content", p_content);
            parameters.Add("p_is_submitted_according_to_template", p_is_submitted_according_to_template);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_document_templates_update]", parameters, cancellationToken);
        }

        public async Task DeleteAsync(
            int? p_doc_template_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_doc_template_id", p_doc_template_id);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_document_templates_delete]", parameters, cancellationToken);
        }
    }
}
