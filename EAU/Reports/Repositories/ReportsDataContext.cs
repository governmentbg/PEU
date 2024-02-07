using CNSys.Data;
using Dapper;
using EAU.Reports.NotaryInterestsForPersonOrVehicle.Models;
using EAU.Reports.PaymentsObligations.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Reports.Repositories
{
    public class ReportsDataContext : BaseDataContext
    {
        public ReportsDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<PaymentsObligationsData> reader, int? count)> PaymentsObligationsSearchAsync(string p_obliged_person_ident,
                       DateTime? p_date_from,
                       DateTime? p_date_to,
                       int? p_start_index,
                       int? p_page_size,
                       bool? p_calculate_count,
                       CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_obliged_person_ident", p_obliged_person_ident);
            parameters.Add("p_date_from", p_date_from);
            parameters.Add("p_date_to", p_date_to);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<PaymentsObligationsData>("[pmt].[p_payments_obligations_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }

        public async Task<(IEnumerable<DocumentAccessedDataGroupedRow> reader, int? count)> DocumentAccessDataReportSearchAsync(
            byte? p_data_type,
            string p_data_value,
            DateTime? p_date_from,
            DateTime? p_date_to,
            int? p_document_type_id,
            string p_data_types_in_result,
            int? p_start_index,
            int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_data_type", p_data_type);
            parameters.Add("p_data_value", p_data_value);
            parameters.Add("p_date_from", p_date_from);
            parameters.Add("p_date_to", p_date_to);
            parameters.Add("p_document_type_id", p_document_type_id);
            parameters.Add("p_data_types_in_result", p_data_types_in_result);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<DocumentAccessedDataGroupedRow>("[audit].[p_document_accessed_data_report_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }
    }
}