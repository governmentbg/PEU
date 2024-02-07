using Microsoft.Data.SqlClient;
using Rebus.SqlServer;

namespace EAU.Rebus
{
    public class RebusConnectionWrapper : DbConnectionWrapper
    {
        public SqlConnection Connection { get; private set; }
        public SqlTransaction Transaction { get; private set; }

        public RebusConnectionWrapper(SqlConnection connection, SqlTransaction currentTransaction, bool managedExternally) :
            base(connection, currentTransaction, managedExternally)
        {
            Connection = connection;
            Transaction = currentTransaction;
        }
    }
}
