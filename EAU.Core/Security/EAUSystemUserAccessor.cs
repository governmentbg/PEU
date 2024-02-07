using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EAU.Security
{
    /// <summary>
    /// Имплементация на IEAUUserAccessor за системен потребител.
    /// </summary>
    public class EAUSystemUserAccessor : IEAUUserAccessor
    {
        public EAUPrincipal User
        {
            get => SystemUser;
            set => throw new NotImplementedException();
        }

        public static EAUPrincipal SystemUser { get; } = new EAUPrincipal(Principal.Anonymous, EAUPrincipal.SystemLocalUserID.ToString());

        public Guid? UserSessionID => throw new NotImplementedException();

        public IPAddress RemoteIpAddress => throw new NotImplementedException();
    }   
}
