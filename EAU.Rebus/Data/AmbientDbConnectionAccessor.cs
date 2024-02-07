using CNSys.Data;
using EAU.Rebus;
using Rebus.SqlServer;
using Rebus.SqlServer.Transport;
using Rebus.Transport;
using System.Data.Common;
using System.Threading.Tasks;

namespace EAU.Data
{
    public class AmbientDbConnectionAccessor : IAmbientDbConnectionAccessor
    {
        private class Connection : IAmbientDbConnection
        {
            private readonly ITransactionContext _transactionContext;
            private readonly RebusConnectionWrapper _rebusConnectionWrapper;

            public Connection(ITransactionContext transactionContext, RebusConnectionWrapper rebusConnectionWrapper)
            {
                _transactionContext = transactionContext;
                _rebusConnectionWrapper = rebusConnectionWrapper;
            }

            public DbTransaction Transaction => _rebusConnectionWrapper.Transaction;

            DbConnection IAmbientDbConnection.Connection => _rebusConnectionWrapper.Connection;

            public void Abort()
            {
                _transactionContext.Abort();
            }
        }

        public IAmbientDbConnection CurrentConnection
        {
            get
            {
                var ctx = AmbientTransactionContext.Current;

                if (ctx != null)
                {
                    if (ctx.Items.TryGetValue(SqlServerTransport.CurrentConnectionKey, out object connectionTask))
                    {
                        return new Connection(ctx, (RebusConnectionWrapper)((Task<IDbConnection>)connectionTask).Result);
                    }
                }
                return null;
            }
        }
    }
}
