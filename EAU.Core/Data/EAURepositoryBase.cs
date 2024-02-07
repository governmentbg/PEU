using CNSys.Data;

namespace EAU.Data
{
    /// <summary>
    /// TODO Velin: добавен е този междинен базов клас само заради override CreateDataContext.
    /// Ще се помисли дали да бъде оставен, или добави нещо от рода на IDataContextCreator, който да се ползва от Cnsys.Data.
    /// </summary>
    /// <typeparam name="EntityType"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="SCriteria"></typeparam>
    /// <typeparam name="DataContext"></typeparam>
    public class EAURepositoryBase<EntityType, Key, SCriteria, DataContext> : RepositoryBase<EntityType, Key, SCriteria, DataContext> where DataContext : System.IDisposable
    {
        public EAURepositoryBase(IDbContextProvider dbContextProvider)
            : base(dbContextProvider)
        {
        }

        protected override DataContext CreateDataContext(IDbContext context)
            => (DataContext)System.Activator.CreateInstance(typeof(DataContext), new object[] { context });
    }
}
