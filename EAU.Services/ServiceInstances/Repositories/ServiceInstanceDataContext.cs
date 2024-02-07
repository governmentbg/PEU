using CNSys.Data;
using Dapper;
using EAU.Services.ServiceInstances.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EAU.Services.ServiceInstances.Repositories
{
    public class ServiceInstanceDataContext : BaseDataContext
    {
        public ServiceInstanceDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        #region Service Instance CRUD

        public async Task<long?> ServiceInstanceCreateAsync(
                      short? p_status,
                      long? p_applicant_id,
                      DateTime? p_service_instance_date,
                      int? p_service_id,
                      string p_case_file_uri,
                      AdditionalData p_additional_data,
                      DateTime? p_status_date,
                      CancellationToken cancellationToken)
        {
            long? p_srv_instance_id = null;

            var parameters = new Dapper.DynamicParameters();
            parameters.Add("p_srv_instance_id", p_srv_instance_id, DbType.Int64, ParameterDirection.Output);
            parameters.Add("p_status", p_status);
            parameters.Add("p_applicant_id", p_applicant_id);
            parameters.Add("p_service_instance_date", p_service_instance_date);
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_case_file_uri", p_case_file_uri);
            parameters.Add("p_additional_data", p_additional_data);
            parameters.Add("p_status_date", p_status_date);

            await _dbContext.SPExecuteAsync("[dbo].[p_srv_instances_create]", parameters, cancellationToken);

            p_srv_instance_id = parameters.Get<long?>("p_srv_instance_id");

            return p_srv_instance_id;
        }

        public async Task ServiceInstanceUpdateAsync(
            long? p_srv_instance_id,
            short? p_status,
            AdditionalData p_additional_data,
            DateTime? p_status_date,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_srv_instance_id", p_srv_instance_id);
            parameters.Add("p_status", p_status);
            parameters.Add("p_additional_data", p_additional_data);
            parameters.Add("p_status_date", p_status_date);
            await _dbContext.SPExecuteAsync("[dbo].[p_srv_instances_update]", parameters, cancellationToken);
        }

        public async Task<(IEnumerable<ServiceInstance> reader, int? count)> ServiceInstanceSearchAsync(string p_srv_instance_ids,
                       short? p_status,
                       long? p_applicant_id,
                       DateTime? p_service_instance_date_from,
                       DateTime? p_service_instance_date_to,
                       int? p_service_id,
                       string p_case_file_uri,
                       bool? p_with_lock,
                       int? p_start_index,
                       int? p_page_size,
                       bool? p_calculate_count,
                       CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_srv_instance_ids", p_srv_instance_ids);
            parameters.Add("p_status", p_status);
            parameters.Add("p_applicant_id", p_applicant_id);
            parameters.Add("p_service_instance_date_from", p_service_instance_date_from);
            parameters.Add("p_service_instance_date_to", p_service_instance_date_to);
            parameters.Add("p_service_id", p_service_id);
            parameters.Add("p_case_file_uri", p_case_file_uri);
            parameters.Add("p_with_lock", p_with_lock);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<ServiceInstance>("[dbo].[p_srv_instances_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");

            return (res, count);
        }

        #endregion
    }
}
