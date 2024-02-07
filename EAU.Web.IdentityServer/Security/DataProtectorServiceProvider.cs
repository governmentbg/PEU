using Microsoft.AspNetCore.DataProtection;

namespace EAU.Web.IdentityServer.Security
{
    public interface IDataProtectorServiceProvider
    {
        IDataProtector GetDataProtector();
    }

    /// <summary>
    /// Защита на чувствителни данни.
    /// </summary>
    internal class DataProtectorServiceProvider : IDataProtectorServiceProvider
    {
        private const string Purpose = "EAU.Web.IdentityServer.DataProtectorService";
        private readonly IDataProtector _dataProtector;

        public DataProtectorServiceProvider(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector(Purpose);
        }

        public IDataProtector GetDataProtector() => _dataProtector;
    }
}
