using CNSys.Data;
using Dapper;
using EAU.Payments.Obligations.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.Obligations.Repositories
{
    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани със задължения.
    /// </summary>
    public class ObligationDataContext : BaseDataContext
    {
        public ObligationDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<long?> ObligationCreateAsync(
                        byte? p_status,
                        decimal? p_amount,
                        decimal? p_discount_amount,
                        string p_bank_name,
                        string p_bic,
                        string p_iban,
                        string p_payment_reason,
                        string p_pep_cin,
                        DateTime? p_expiration_date,
                        int? p_applicant_id,
                        string p_obliged_person_name,
                        string p_obliged_person_ident,
                        byte? p_obliged_person_ident_type,
                        DateTime? p_obligation_date,
                        string p_obligation_identifier,
                        byte? p_type,
                        long? p_service_instance_id,
                        int? p_service_id,
                        AdditionalData p_additional_data,
                        byte? p_and_source_id,
                        CancellationToken cancellationToken)
        {
            long? p_obligation_id = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_obligation_id", p_obligation_id, DbType.Int64, ParameterDirection.Output);
            parameters.Add("p_status", p_status);
            parameters.Add("p_amount", p_amount);
            parameters.Add("p_discount_amount", p_discount_amount);
            parameters.Add("p_bank_name", p_bank_name);
            parameters.Add("p_bic", p_bic);
            parameters.Add("p_iban", p_iban);
            parameters.Add("p_payment_reason", p_payment_reason);
            parameters.Add("p_pep_cin", p_pep_cin);
            parameters.Add("p_expiration_date", p_expiration_date);
            parameters.Add("p_applicant_id", p_applicant_id);
            parameters.Add("p_obliged_person_name", p_obliged_person_name);
            parameters.Add("p_obliged_person_ident", p_obliged_person_ident);
            parameters.Add("p_obliged_person_ident_type", p_obliged_person_ident_type);
            parameters.Add("p_obligation_date", p_obligation_date);
            parameters.Add("p_obligation_identifier", p_obligation_identifier);
            parameters.Add("p_type", p_type);
            parameters.Add("p_service_instance_id", p_service_instance_id);
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_additional_data", p_additional_data);
            parameters.Add("p_and_source_id", p_and_source_id);

            await _dbContext.SPExecuteAsync("[pmt].[p_obligations_create]", parameters, cancellationToken);

            p_obligation_id = parameters.Get<long?>("p_obligation_id");

            return p_obligation_id;
        }

        public Task ObligationUpdateAsync(
            long? p_obligation_id,
            byte? p_status,
            decimal? p_amount,
            decimal? p_discount_amount,
            string p_bank_name,
            string p_bic,
            string p_iban,
            string p_payment_reason,
            string p_pep_cin,
            DateTime? p_expiration_date,
            int? p_applicant_id,
            string p_obliged_person_name,
            string p_obliged_person_ident,
            byte? p_Obliged_person_ident_type,
            DateTime? p_obligation_date,
            string p_obligation_identifier,
            byte? p_type,
            long? p_service_instance_id,
            int? p_service_id,
            AdditionalData p_additional_data,
            byte? p_and_source_id,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_obligation_id", p_obligation_id);
            parameters.Add("p_status", p_status);
            parameters.Add("p_amount", p_amount);
            parameters.Add("p_discount_amount", p_discount_amount);
            parameters.Add("p_bank_name", p_bank_name);
            parameters.Add("p_bic", p_bic);
            parameters.Add("p_iban", p_iban);
            parameters.Add("p_payment_reason", p_payment_reason);
            parameters.Add("p_pep_cin", p_pep_cin);
            parameters.Add("p_expiration_date", p_expiration_date);
            parameters.Add("p_applicant_id", p_applicant_id);
            parameters.Add("p_obliged_person_name", p_obliged_person_name);
            parameters.Add("p_obliged_person_ident", p_obliged_person_ident);
            parameters.Add("p_Obliged_person_ident_type", p_Obliged_person_ident_type);
            parameters.Add("p_obligation_date", p_obligation_date);
            parameters.Add("p_obligation_identifier", p_obligation_identifier);
            parameters.Add("p_type", p_type);
            parameters.Add("p_service_instance_id", p_service_instance_id);
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_additional_data", p_additional_data);
            parameters.Add("p_and_source_id", p_and_source_id);

            return _dbContext.SPExecuteAsync("[pmt].[p_obligations_update]", parameters, cancellationToken);
        }

        public Task<CnsysGridReader> ObligationSearchAsync(
                                    List<ObligationIdentifiersSearchCriteria> p_obligations_search_ids,
                                    bool? p_is_active,
                                    byte? p_type,
                                    string p_obligation_ids,
                                    string p_statuses,
                                    int? p_applicant_id,
                                    bool? p_is_applicant_anonimous,
                                    long? p_service_instance_id,
                                    bool? p_with_lock,
                                    bool? p_load_payment_requests,
                                    int? p_start_index,
                                    int? p_page_size,
                                    bool? p_calculate_count,
                                    CancellationToken cancellationToken)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("obligation_date", typeof(DateTime));
            dt.Columns.Add("obligation_identifier", typeof(string));

            if (p_obligations_search_ids != null && p_obligations_search_ids.Count > 0)
            {
                p_obligations_search_ids.ForEach(g =>
                {
                    dt.Rows.Add(g.ObligationDate.Value, g.ObligationIdentifier);
                });
            }

            var parameters = new DynamicParameters();

            parameters.Add("p_obligations_search_ids", dt.AsTableValuedParameter("[pmt].[tt_obligations_search_ids]"));
            parameters.Add("p_is_active", p_is_active);
            parameters.Add("p_type", p_type);
            parameters.Add("p_obligation_ids", p_obligation_ids);
            parameters.Add("p_statuses", p_statuses);
            parameters.Add("p_applicant_id", p_applicant_id);
            parameters.Add("p_is_applicant_anonimous", p_is_applicant_anonimous);
            parameters.Add("p_service_instance_id", p_service_instance_id);
            parameters.Add("p_with_lock", p_with_lock);
            parameters.Add("p_load_payment_requests", p_load_payment_requests);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("@p_count", dbType: DbType.Int32, direction: System.Data.ParameterDirection.Output);

            return _dbContext.SPQueryMultipleAsync("[pmt].[p_obligations_search]", parameters, cancellationToken);
        }
    }
}