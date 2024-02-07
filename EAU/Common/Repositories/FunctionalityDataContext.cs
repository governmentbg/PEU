using CNSys.Data;
using Dapper;
using EAU.Common.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Common.Repositories
{
    /// <summary>
    /// Клас капсулиращ работата по извикване на процедурите в базата данни свързани с функционалности на системата.
    /// </summary>
    internal class FunctionalityDataContext : BaseDataContext
    {
        public FunctionalityDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<Functionality> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
            int? p_functionality_id,
            int? p_start_index,
	        int? p_page_size,
            bool? p_calculate_count,
            CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_functionality_id", p_functionality_id);
            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<Functionality>("[dbo].[p_n_s_functionalities_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }
    }
}
