using CNSys.Data;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Utilities
{
    /// <summary>
    /// Extension методи върху IDbContext за работа с базата данни.
    /// </summary>
    public static class DbContextExtensions
    {
        public static Task<int> SPExecuteAsync(this IDbContext dbContext, string commandText, DynamicParameters parameters, CancellationToken cancellationToken)
        {
            return dbContext.Connection.ExecuteAsync(new CommandDefinition(
                        commandText: commandText,
                        commandType: System.Data.CommandType.StoredProcedure,
                        parameters: parameters,
                        transaction: dbContext.Transaction,
                        cancellationToken: cancellationToken));

        }

        public static Task<System.Data.IDataReader> SPExecuteReaderAsync(this IDbContext dbContext, string commandText, DynamicParameters parameters, CancellationToken cancellationToken)
        {
            return dbContext.Connection.ExecuteReaderAsync(new CommandDefinition(
                        commandText: commandText,
                        commandType: System.Data.CommandType.StoredProcedure,
                        parameters: parameters,
                        transaction: dbContext.Transaction,
                        cancellationToken: cancellationToken));

        }

        public static Task<IEnumerable<T>> SPQueryAsync<T>(this IDbContext dbContext, string commandText, DynamicParameters parameters, CancellationToken cancellationToken)
        {
            return dbContext.Connection.QueryAsync<T>(new CommandDefinition(
                        commandText: commandText,
                        commandType: System.Data.CommandType.StoredProcedure,
                        parameters: parameters,
                        transaction: dbContext.Transaction,
                        cancellationToken: cancellationToken));

        }

        public static async Task<CnsysGridReader> SPQueryMultipleAsync(this IDbContext dbContext, string commandText, DynamicParameters parameters, CancellationToken cancellationToken)
        {
            var reader = await dbContext.Connection.QueryMultipleAsync(new CommandDefinition(
                        commandText: commandText,
                        commandType: System.Data.CommandType.StoredProcedure,
                        parameters: parameters,
                        transaction: dbContext.Transaction,
                        cancellationToken: cancellationToken));
            return new CnsysGridReader(reader, parameters);
        }
    }
}
