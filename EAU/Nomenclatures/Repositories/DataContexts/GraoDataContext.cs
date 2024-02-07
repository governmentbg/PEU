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
    /// Клас, капсулиращ работата по извикването на процедурите от базата данни, свързани с GRAO
    /// </summary>
    internal class GraoDataContext : BaseDataContext
    {
        public GraoDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IEnumerable<Grao> reader, int? count, DateTime? p_last_updated_on)> SearchAsync(
          int? p_start_index,
          int? p_page_size,
          bool? p_calculate_count,
          CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_start_index", p_start_index);
            parameters.Add("p_page_size", p_page_size);
            parameters.Add("p_calculate_count", p_calculate_count);

            parameters.Add("p_count", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("p_last_updated_on", dbType: DbType.DateTime, direction: ParameterDirection.Output);

            var res = await _dbContext.SPQueryAsync<Grao>("[nom].[p_n_s_grao_search]", parameters, cancellationToken);

            int? count = parameters.Get<int?>("p_count");
            var p_last_updated_on = parameters.Get<DateTime?>("p_last_updated_on");

            return (res, count, p_last_updated_on);
        }
    }
}
