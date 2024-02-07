using System;

namespace EAU.Utilities
{
    public abstract class StartupRegistrator<T>
    {
        private static Lazy<T> _lazyInitializer = new Lazy<T>(() => { return Activator.CreateInstance<T>(); });

        protected StartupRegistrator()
        {
            RegisterInternal();
        }

        public static T Current { get { return _lazyInitializer.Value; } }

        public void Register()
        {

        }

        protected abstract void RegisterInternal();
    }
}
