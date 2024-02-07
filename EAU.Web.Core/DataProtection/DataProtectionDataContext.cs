using CNSys.Data;
using Dapper;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Protection
{
    public class DataProtectionDataContext : BaseDataContext
    {
        public DataProtectionDataContext(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<DataProtectionKey>> DataProtectionKeysSearchAsync(CancellationToken cancelationToken)
            => _dbContext.SPQueryAsync<DataProtectionKey>("[aspnetcore].[p_data_protection_keys_search]", new DynamicParameters(), cancelationToken);

        public async Task DataProtectionKeysCreateAsync(string p_id, string p_keyxml, DateTime p_creationDate, DateTime p_activationDate, DateTime p_expirationDate, CancellationToken cancelationToken)
        {
            var parameters = new DynamicParameters();

            parameters.Add("p_id", p_id);
            parameters.Add("p_keyxml", p_keyxml);
            parameters.Add("p_creation_date", p_creationDate);
            parameters.Add("p_activation_date", p_activationDate);
            parameters.Add("p_expiration_date", p_expirationDate);

            await _dbContext.SPExecuteAsync("[aspnetcore].[p_data_protection_keys_create]", parameters, cancelationToken);
        }
    }
}
