using System;

namespace EAU.Security
{
    public interface IEAUUserImpersonation
    {
        EAUUserImpersonationControl Impersonate(EAUPrincipal user);
    }

    public struct EAUUserImpersonationControl : IDisposable
    {
        private Action _onDispose;

        public EAUUserImpersonationControl(Action onDispose)
        {
            _onDispose = onDispose;
        }

        public void Dispose()
        {
            _onDispose?.Invoke();
        }
    }
}
