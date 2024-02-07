using System;

namespace EAU.Web.IdentityServer.Common
{
    /// <summary>
    /// Exception за грешки в процесите в рамките на IdentityServer.
    /// </summary>
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }
    }
}