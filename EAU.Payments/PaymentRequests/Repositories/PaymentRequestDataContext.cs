using CNSys.Data;
using Dapper;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;


namespace EAU.Payments.PaymentRequests.Repositories
{
    /// <summary>
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани със заявки за плащания.
    /// </summary>
    public class PaymentRequestDataContext : BaseDataContext
    {
        public PaymentRequestDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<long?> PaymentRequestCreateAsync(
                        int? p_registration_data_id,
                        byte? p_registration_data_type,
                        byte? p_status,
                        long? p_obligation_id,
                        string p_obliged_person_name,
                        string p_obliged_person_ident,
                        byte? p_obliged_person_ident_type,
                        string p_payer_ident,
                        byte? p_payer_ident_type,
                        DateTime? p_send_date,
                        DateTime? p_pay_date,
                        string p_external_portal_payment_number,
                        decimal? p_amount,
                        AdditionalData p_additional_data,
                        CancellationToken cancellationToken)
        {
            long? p_payment_request_id = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_payment_request_id", p_payment_request_id, DbType.Int64, ParameterDirection.Output);
            parameters.Add("p_registration_data_id", p_registration_data_id);
            parameters.Add("p_registration_data_type", p_registration_data_type);
            parameters.Add("p_status", p_status);
            parameters.Add("p_obligation_id", p_obligation_id);
            parameters.Add("p_obliged_person_name", p_obliged_person_name);
            parameters.Add("p_obliged_person_ident", p_obliged_person_ident);
            parameters.Add("p_obliged_person_ident_type", p_obliged_person_ident_type);
            parameters.Add("p_payer_ident", p_payer_ident);
            parameters.Add("p_payer_ident_type", p_payer_ident_type);
            parameters.Add("p_send_date", p_send_date);
            parameters.Add("p_pay_date", p_pay_date);
            parameters.Add("p_external_portal_payment_number", p_external_portal_payment_number);
            parameters.Add("p_amount", p_amount);
            parameters.Add("p_additional_data", p_additional_data);

            await _dbContext.SPExecuteAsync("[pmt].[p_payment_requests_create]", parameters, cancellationToken);

            p_payment_request_id = parameters.Get<long?>("p_payment_request_id");


            return p_payment_request_id;
        }

        public Task PaymentRequestUpdateAsync(
            long? p_payment_request_id,
            int? p_registration_data_id,
            byte? p_registration_data_type,
            byte? p_status,
            long? p_obligation_id,
            string p_obliged_person_name,
            string p_obliged_person_ident,
            byte? p_obliged_person_ident_type,
            string p_payer_ident,
            byte? p_payer_ident_type,
            DateTime? p_send_date,
            DateTime? p_pay_date,
            string p_external_portal_payment_number,
            decimal? p_amount,
            AdditionalData p_additional_data,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_payment_request_id", p_payment_request_id);
            parameters.Add("p_registration_data_id", p_registration_data_id);
            parameters.Add("p_registration_data_type", p_registration_data_type);
            parameters.Add("p_status", p_status);
            parameters.Add("p_obligation_id", p_obligation_id);
            parameters.Add("p_obliged_person_name", p_obliged_person_name);
            parameters.Add("p_obliged_person_ident", p_obliged_person_ident);
            parameters.Add("p_obliged_person_ident_type", p_obliged_person_ident_type);
            parameters.Add("p_payer_ident", p_payer_ident);
            parameters.Add("p_payer_ident_type", p_payer_ident_type);
            parameters.Add("p_send_date", p_send_date);
            parameters.Add("p_pay_date", p_pay_date);
            parameters.Add("p_external_portal_payment_number", p_external_portal_payment_number);
            parameters.Add("p_amount", p_amount);
            parameters.Add("p_additional_data", p_additional_data);

            return _dbContext.SPExecuteAsync("[pmt].[p_payment_requests_update]", parameters, cancellationToken);
        }

        public Task PaymentRequestDeleteAsync(long? p_payment_request_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_payment_request_id", p_payment_request_id);

            return _dbContext.SPExecuteAsync("[pmt].[p_payment_requests_delete]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<Models.PaymentRequest> reader, int? count)> PaymentRequestSearchAsync(
                       string p_payment_request_ids,
                       string p_obligation_ids,
                       int? p_registration_data_id,
                       byte? p_registration_data_type,
                       string p_external_portal_number,
                       bool? p_with_lock,
                       int? p_start_index,
                       int? p_page_size,
                       bool? p_calculate_count,
                       CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_payment_request_ids", p_payment_request_ids);
            parameters.Add("p_obligation_ids", p_obligation_ids);
            parameters.Add("p_registration_data_id", p_registration_data_id);
            parameters.Add("p_registration_data_type", p_registration_data_type);
            parameters.Add("p_external_portal_number", p_external_portal_number);
            parameters.Add("p_with_lock", p_with_lock);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<Models.PaymentRequest>("[pmt].[p_payment_requests_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }
    }

}
