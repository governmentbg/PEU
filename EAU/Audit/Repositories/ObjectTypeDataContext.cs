using CNSys.Data;
using Dapper;
using EAU.Audit.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Audit.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани с видове обекти.
    /// </summary>
    internal class ObjectTypeDataContext : BaseDataContext
    {
        public ObjectTypeDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<ObjectType> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            int? p_object_type_id,
            int? p_start_index,
	        int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_object_type_id", p_object_type_id);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<ObjectType>("[audit].[p_n_s_object_types_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }
    }
}
