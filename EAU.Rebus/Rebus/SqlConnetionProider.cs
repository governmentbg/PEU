using CNSys.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Rebus.SqlServer;
using System.Threading.Tasks;

namespace EAU.Rebus
{
    public class SqlConnetionProider : IDbConnectionProvider
    {
        private readonly IDbContextAccessor _dbContextAccessor;
        private readonly IConfiguration _configuration;

        public SqlConnetionProider(IDbContextAccessor dbContextAccessor, IConfiguration configuration)
        {
            _dbContextAccessor = dbContextAccessor;
            _configuration = configuration;
        }

        public async Task<IDbConnection> GetConnection()
        {
            var currentDbContext = _dbContextAccessor.GetCurrentContext();

            SqlConnection connection = (SqlConnection)currentDbContext?.Connection;
            SqlTransaction transaction = (SqlTransaction)currentDbContext?.Transaction;

            if (currentDbContext == null)
            {
                var connectionStringSettings = _configuration.GetEAUDBConnectionString();

                connection = new SqlConnection(connectionStringSettings.ConnectionString);
                await connection.OpenAsync();

                transaction = (SqlTransaction)await connection.BeginTransactionAsync();
            }

            return new RebusConnectionWrapper(connection, transaction, currentDbContext != null);
        }
    }
}
