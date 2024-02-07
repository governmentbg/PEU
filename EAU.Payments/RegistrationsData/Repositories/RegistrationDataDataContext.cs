using CNSys.Data;
using Dapper;
using EAU.Payments.RegistrationsData.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Payments.RegistrationsData.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани с регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    internal class RegistrationDataDataContext : BaseDataContext
    {
        public RegistrationDataDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int?> RegistrationDataCreateAsync(
            int? p_type,
            string p_description,
            string p_cin,
            string p_email,
            string p_secret_word,
            TimeSpan? p_validity_period,
            string p_portal_url,
            string p_notification_url,
            string p_service_url,
            string p_iban,
            CancellationToken cancellationToken)
        {
            int? p_registration_data_id = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_registration_data_id", p_registration_data_id, DbType.Int32, ParameterDirection.Output);
            parameters.Add("p_type", p_type);
            parameters.Add("p_description", p_description);
            parameters.Add("p_cin", p_cin);
            parameters.Add("p_email", p_email);
            parameters.Add("p_secret_word", p_secret_word);
            parameters.Add("p_validity_period", p_validity_period);
            parameters.Add("p_portal_url", p_portal_url);
            parameters.Add("p_notification_url", p_notification_url);
            parameters.Add("p_service_url", p_service_url);
            parameters.Add("p_iban", p_iban);

            await _dbContext.SPExecuteAsync("[pmt].[p_n_d_registration_data_create]", parameters, cancellationToken);

            p_registration_data_id = parameters.Get<int?>("p_registration_data_id");

            return p_registration_data_id;
        }


        public Task RegistrationDataUpdateAsync(
            int? p_registration_data_id,
            int? p_type,
            string p_description,
            string p_cin,
            string p_email,
            string p_secret_word,
            TimeSpan? p_validity_period,
            string p_portal_url,
            string p_notification_url,
            string p_service_url,
            string p_iban,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_registration_data_id", p_registration_data_id);
            parameters.Add("p_type", p_type);
            parameters.Add("p_description", p_description);
            parameters.Add("p_cin", p_cin);
            parameters.Add("p_email", p_email);
            parameters.Add("p_secret_word", p_secret_word);
            parameters.Add("p_validity_period", p_validity_period);
            parameters.Add("p_portal_url", p_portal_url);
            parameters.Add("p_notification_url", p_notification_url);
            parameters.Add("p_service_url", p_service_url);
            parameters.Add("p_iban", p_iban);

            return _dbContext.SPExecuteAsync("[pmt].[p_n_d_registration_data_update]", parameters, cancellationToken);
        }
        public Task RegistrationDataDeleteAsync(int? p_registration_data_id, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@p_registration_data_id", p_registration_data_id);

            return _dbContext.SPExecuteAsync("[pmt].[p_n_d_registration_data_delete]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<RegistrationData> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            string p_registration_data_ids,
            int? p_type,
            string p_cin,
            string p_iban,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_registration_data_ids", p_registration_data_ids);
            parameters.Add("p_type", p_type);
            parameters.Add("p_cin", p_cin);
            parameters.Add("p_iban", p_iban);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);
            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<RegistrationData>("[pmt].[p_n_d_registration_data_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }
    }
}
