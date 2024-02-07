using CNSys;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EAU.Users
{
    /// <summary>
    /// Интерфейс за работа с данни на потребител
    /// </summary>
    public interface IUserInteractionService
    {
        /// <summary>
        /// Взима сертификат от логин сесията на текущия потребител, ако е наличен.
        /// </summary>
        /// <returns></returns>
        Task<OperationResult<X509Certificate2>> GetCurrentUserSessionCertificateAsync();
    }
}
