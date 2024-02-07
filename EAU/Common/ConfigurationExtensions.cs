using CNSys.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static ConnectionStringSettings GetEAUDBConnectionString(this IConfiguration configuration)
        {
            string connectionString = configuration.GetEAUSection().GetValue<string>("GL_DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                return configuration.GetSection("ConnectionStrings").Get<Dictionary<string, ConnectionStringSettings>>().Single(cs => cs.Key == "default").Value;
            }
            else
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.ApplicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

                return new ConnectionStringSettings("default", builder.ToString(), "Microsoft.Data.SqlClient");
            }
        }

        public static ConnectionStringSettings GetDBCacheDependencyConnectionString(this IConfiguration configuration)
        {
            string connectionString = configuration.GetEAUSection().GetValue<string>("GL_DB_CONNECTION_STRING");

            if (string.IsNullOrEmpty(connectionString))
            {
                return configuration.GetSection("ConnectionStrings").Get<Dictionary<string, ConnectionStringSettings>>().Single(cs => cs.Key == "SqlDependency").Value;
            }
            else
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.ApplicationName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

                return new ConnectionStringSettings("SqlDependency", builder.ToString(), "Microsoft.Data.SqlClient");
            }
        }
    }
}
