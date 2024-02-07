using EAU.Utilities;

namespace EAU.Web.IdentityServer
{
    public class EAUIdsrvStartupBootstrapper
    {
        public static void Run()
        {
            EAUStartupBootstrapper.Run();

            //EPZEUWebCoreStartupBootstrapper.Run();

            EAUIdsrvStartupRegistrator.Current.Register();
        }
    }

    public class EAUIdsrvStartupRegistrator : StartupRegistrator<EAUIdsrvStartupRegistrator>
    {
        protected override void RegisterInternal()
        {

        }
    }
}
