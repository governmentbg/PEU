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
    internal class ServiceDataContext : BaseDataContext
    {
        public ServiceDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<Service> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
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

            var res = await _dbContext.SPQueryAsync<Service>("[nom].[p_n_d_services_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }

        public async Task<int?> CreateAsync(
            int? p_group_id,
            string p_name,
            int? p_doc_type_id,
            string p_sunau_service_uri,
            short? p_initiation_type_id,
            string p_result_document_name,
            string p_description,
            string p_explanatory_text_service,
            string p_explanatory_text_fulfilled_service,
            string p_explanatory_text_refused_or_terminated_service,
            int? p_order_number,
            string p_adm_structure_unit_name,
            string p_attached_documents_description,
            AdditionalData p_additional_configuration,
            string p_service_url,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            int? p_service_id = null;

            var parameters = new DynamicParameters();
            parameters.Add("p_service_id", p_service_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_group_id", p_group_id);
            parameters.Add("p_name", p_name);
            parameters.Add("p_doc_type_id", p_doc_type_id);
            parameters.Add("p_sunau_service_uri", p_sunau_service_uri);
            parameters.Add("p_initiation_type_id", p_initiation_type_id);
            parameters.Add("p_result_document_name", p_result_document_name);
            parameters.Add("p_description", p_description);
            parameters.Add("p_explanatory_text_service", p_explanatory_text_service);
            parameters.Add("p_explanatory_text_fulfilled_service", p_explanatory_text_fulfilled_service);
            parameters.Add("p_explanatory_text_refused_or_terminated_service", p_explanatory_text_refused_or_terminated_service);
            parameters.Add("p_order_number", p_order_number);
            parameters.Add("p_adm_structure_unit_name", p_adm_structure_unit_name);
            parameters.Add("p_attached_documents_description", p_attached_documents_description);
            parameters.Add("p_additional_configuration", p_additional_configuration);
            parameters.Add("p_service_url", p_service_url);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_services_create]", parameters, cancellationToken);

            p_service_id = parameters.Get<int?>("p_service_id");

            return p_service_id;
        }

        public async Task UpdateAsync(
            int? p_service_id,
            int? p_group_id,
            string p_name,
            int? p_doc_type_id,
            string p_sunau_service_uri,
            short? p_initiation_type_id,
            string p_result_document_name,
            string p_description,
            string p_explanatory_text_service,
            string p_explanatory_text_fulfilled_service,
            string p_explanatory_text_refused_or_terminated_service,
            int? p_order_number,
            string p_adm_structure_unit_name,
            string p_attached_documents_description,
            AdditionalData p_additional_configuration,
            string p_service_url,
            bool? p_is_active,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_group_id", p_group_id);
            parameters.Add("p_name", p_name);
            parameters.Add("p_doc_type_id", p_doc_type_id);
            parameters.Add("p_sunau_service_uri", p_sunau_service_uri);            ;
            parameters.Add("p_initiation_type_id", p_initiation_type_id);
            parameters.Add("p_result_document_name", p_result_document_name);
            parameters.Add("p_description", p_description);
            parameters.Add("p_explanatory_text_service", p_explanatory_text_service);
            parameters.Add("p_explanatory_text_fulfilled_service", p_explanatory_text_fulfilled_service);
            parameters.Add("p_explanatory_text_refused_or_terminated_service", p_explanatory_text_refused_or_terminated_service);
            parameters.Add("p_order_number", p_order_number);
            parameters.Add("p_adm_structure_unit_name", p_adm_structure_unit_name);
            parameters.Add("p_attached_documents_description", p_attached_documents_description);
            parameters.Add("p_additional_configuration", p_additional_configuration);
            parameters.Add("p_service_url", p_service_url);
            parameters.Add("p_is_active", p_is_active);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_services_update]", parameters, cancellationToken);
        }

        public async Task DeleteAsync(
            int? p_service_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_service_id", p_service_id);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_services_delete]", parameters, cancellationToken);
        }

        public async Task ServiceTranslationCreateAsync(
            int? p_service_id,
            int? p_language_id,
            string p_name,
            string p_description,
            string p_attached_documents_description,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_name", p_name);
            parameters.Add("p_description", p_description);
            parameters.Add("p_attached_documents_description", p_attached_documents_description);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_services_i18n_create]", parameters, cancellationToken);
        }

        public async Task ServiceTranslationUpdateAsync(
            int? p_service_id,
            int? p_language_id,
            string p_name,
            string p_description,
            string p_attached_documents_description,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_language_id", p_language_id);
            parameters.Add("p_name", p_name);
            parameters.Add("p_description", p_description);
            parameters.Add("p_attached_documents_description", p_attached_documents_description);

            await _dbContext.SPExecuteAsync("[nom].[p_n_d_services_i18n_update]", parameters, cancellationToken);
        }
    }
}
