using CNSys.Data;
using System;

namespace EAU.Utilities
{
    public class BaseDataContext : IDisposable
    {
        protected IDbContext _dbContext;

        public BaseDataContext(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
        }
    }
}
