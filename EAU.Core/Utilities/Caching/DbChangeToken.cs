using CNSys.Caching;

namespace EAU.Utilities.Caching
{
    internal class DbChangeToken : GenericChangeToken
    {
        private readonly IDbCacheInvalidationDispatcher _npgNotificationDispatcher;
        private readonly string _tableName;

        public DbChangeToken(string tableName, IDbCacheInvalidationDispatcher npgNotificationDispatcher)
        {
            _tableName = tableName;
            _npgNotificationDispatcher = npgNotificationDispatcher;
        }

        protected override void TokenCreated()
        {
            _npgNotificationDispatcher.RegisterTable(_tableName, Callback);
        }

        protected override void TokenReleased()
        {
            _npgNotificationDispatcher.UnregisterTable(_tableName, Callback);
        }

        private void Callback(object sender)
        {
            OnChanged();
        }
    }
}
