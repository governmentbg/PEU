using Microsoft.AspNetCore.DataProtection;

namespace EAU.Web.DataProtection
{
    /// <summary>
    /// Интерфейс за работа със защита на данни.
    /// </summary>
    public interface IDataProtectorService
    {
        /// <summary>
        /// Операция за защитаване.
        /// </summary>
        /// <param name="plainText">Обикновен текст.</param>
        /// <returns></returns>
        string Protect(string plainText);

        /// <summary>
        /// Операция за дешифриране.
        /// </summary>
        /// <param name="cipherText">Шифър</param>
        /// <returns></returns>
        string Unprotect(string cipherText);
    }

    /// <summary>
    /// Защита на чувствителни данни.
    /// </summary>
    public class GenericDataProtectorService : IDataProtectorService
    {
        private const string Purpose = "GenericDataProtection";
        private readonly IDataProtector _dataProtector;

        public GenericDataProtectorService(IDataProtectionProvider provider) => _dataProtector = provider.CreateProtector(Purpose);

        public string Protect(string plainText) => string.IsNullOrEmpty(plainText) ? "" : _dataProtector.Protect(plainText);

        public string Unprotect(string cipherText) => string.IsNullOrEmpty(cipherText) ? "" : _dataProtector.Unprotect(cipherText);        
    }
}
