using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EAU.Security
{
    /// <summary>
    /// Интерфейс, чрез който се достъпват данни за текущия потребител.
    /// </summary>
    public interface IEAUUserAccessor
    {
        /// <summary>
        /// Връща данни за текушия потребител.
        /// </summary>
        EAUPrincipal User { get; }
        /// <summary>
        /// Връща текущата потребителска сесия.
        /// </summary>
        Guid? UserSessionID { get; }

        /// <summary>
        /// Връща IP адреса на потребителя, от който се прави заявката.
        /// </summary>
        IPAddress RemoteIpAddress { get; }
    }
}
