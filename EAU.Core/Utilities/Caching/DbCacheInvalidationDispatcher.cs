using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EAU.Utilities.Caching
{
    /// <summary>
    /// Интерфейс за работа с Db cache invalidation
    /// </summary>
    public interface IDbCacheInvalidationDispatcher
    {
        /// <summary>
        /// Регистрира handler за таблица.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="handler"></param>
        void RegisterTable(string tableName, Action<object> handler);

        /// <summary>
        /// Дерегистрира handler за таблица.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="handler"></param>
        void UnregisterTable(string tableName, Action<object> handler);
    }

    public static class IDbCacheInvalidationDispatcherExtensions
    {
        /// <summary>
        /// Връща IChangeToken по подадени таблици, по които да се следи за промяна.
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="dependencyTableNames"></param>
        /// <returns></returns>
        public static IChangeToken GetChangeToken(this IDbCacheInvalidationDispatcher dispatcher, string[] dependencyTableNames)
        {
            return new CompositeChangeToken(dependencyTableNames.Select((tableName) => { return new DbChangeToken(tableName, dispatcher); }).ToList());
        }
    }

    public class DbCacheInvalidationDispatcherOptions
    {
        public string ConnectionString { get; set; }
    }

    public class DbCacheInvalidationDispatcher : IDbCacheInvalidationDispatcher, IDisposable
    {
        private readonly object _locker = new object();
        private readonly DbCacheInvalidationDispatcherOptions _options;
        private readonly ILogger _logger;

        private readonly Dictionary<ValueTuple<string, Action<object>>, ValueTuple<SqlDependency, OnChangeEventHandler>> _registeredHandlers = new Dictionary<(string, Action<object>), (SqlDependency, OnChangeEventHandler)>();
        private readonly Dictionary<string, SqlDependency> _currentActiveDependencies = new Dictionary<string, SqlDependency>();

        private bool _disposed = false;
        
        #region Constructors

        public DbCacheInvalidationDispatcher(ILogger<DbCacheInvalidationDispatcher> logger, IOptions<DbCacheInvalidationDispatcherOptions> options)
        {
            _options = options.Value;
            _logger = logger;

            _logger.LogInformation($"{GetType().Name} created!");

            SqlDependency.Start(_options.ConnectionString);
        }

        #endregion

        #region Public Interface

        public void RegisterTable(string tableName, Action<object> handler)
        {            
            lock (_locker)
            {
                var tupleKey = new ValueTuple<string, Action<object>>(tableName, handler);

                if (_registeredHandlers.ContainsKey(tupleKey))
                    throw new ArgumentException("Alrdeady registered handler!", nameof(handler));

                var sqlDepencency = GetOrCreateSqlDependencyForTable(tableName);

                var innerHandler = new OnChangeEventHandler((o, a) =>
                {
                    handler(o);
                    SlqDepencencyEventFired((SqlDependency)o, a);
                });

                sqlDepencency.OnChange += innerHandler;

                _registeredHandlers.Add(tupleKey, new ValueTuple<SqlDependency, OnChangeEventHandler>(sqlDepencency, innerHandler));

                OnRegisterdTable(tableName, handler, sqlDepencency);
            }
        }

        public void UnregisterTable(string tableName, Action<object> handler)
        {
            lock (_locker)
            {
                var tupleKey = new ValueTuple<string, Action<object>>(tableName, handler);

                if (!_registeredHandlers.TryGetValue(tupleKey, out ValueTuple<SqlDependency, OnChangeEventHandler> data))
                    throw new ArgumentException("The handler is not registered", nameof(handler));

                _registeredHandlers.Remove(tupleKey);
                /*Unregister from the SqlDependency*/
                data.Item1.OnChange -= data.Item2;

                OnUnregisteredTable(tableName, handler, data.Item1);
            }
        }

        public void Dispose()
        {
            lock (_locker)
            {
                if (!_disposed)
                {
                    SqlDependency.Stop(_options.ConnectionString);
                    _disposed = true;

                    _logger.LogInformation($"{GetType().Name} disposed and SqlDependency stopped.");
                }
            }            
        }

        #endregion

        #region Helpers
        
        private SqlDependency CreateSqlDependencyForTable(string tableName)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("[dbo].[p_sys_cache_querynotify]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("p_Tablename", tableName));

                    var dependency = new SqlDependency(command, null, 0);
                    dependency.AddCommandDependency(command);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read()) { }
                    }

                    return dependency;
                }
            }
        }

        private SqlDependency GetOrCreateSqlDependencyForTable(string tableName)
        {
            SqlDependency sqlDependency;

            if (!_currentActiveDependencies.TryGetValue(tableName, out sqlDependency))
                sqlDependency = null;
            else if (sqlDependency.HasChanges) /*Ако има промяна по обекта, не може да го преизползваме, трябва да създаден нов*/
            {
                _currentActiveDependencies.Remove(tableName);
                sqlDependency = null;
            }

            if(sqlDependency == null)
            {
                sqlDependency = CreateSqlDependencyForTable(tableName);
                _currentActiveDependencies.Add(tableName, sqlDependency);
            }


            return sqlDependency;
        }

        private void OnRegisterdTable(string tableName, Action<object> handler, SqlDependency sqlDependency)
        {
            _logger.LogInformation("Register Table={0} with handlerHashCode={1} for dependencyID={2}", tableName, handler.GetHashCode(), sqlDependency.Id);
        }

        private void OnUnregisteredTable(string tableName, Action<object> handler, SqlDependency sqlDependency)
        {
            _logger.LogInformation("UnRegister Table={0} with handlerHashCode={1} for dependencyID={2}", tableName, handler.GetHashCode(), sqlDependency.Id);
        }

        private void SlqDepencencyEventFired(SqlDependency sqlDependency, SqlNotificationEventArgs sqlNotificationEventArgs)
        {
            _logger.LogInformation("SqlDependency fired event on handler with ID={0}, Info={1}, Source={2}, Type={3}, ", sqlDependency.Id, sqlNotificationEventArgs.Info, sqlNotificationEventArgs.Source, sqlNotificationEventArgs.Type);
        }

        #endregion
    }
}
