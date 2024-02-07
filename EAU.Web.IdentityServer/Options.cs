namespace EAU.Web.IdentityServer
{
    /// <summary>
    /// Параметри на конфигурация за потребителски профили.
    /// </summary>
    public class AccountOptions
    {
        public static bool ShowLogoutPrompt = false;

        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        public static readonly string NRASchemeName = "nra";
    }

    /// <summary>
    /// Параметри на конфигурация за автентификация.
    /// </summary>
    public class AuthenticationOptions
    {
        public static readonly string UserSessionCookieName = "idsrv.user.session";
        public static readonly string EAUIsLoggedCookieName = "EAU_ISLOGGED";

        public bool EnableWindowsAuth { get; set; }

        public bool EnableUsrNamePwdAuth { get; set; }

        public bool EnableKEPAuth { get; set; }

        public bool EnableNRAAuth { get; set; }

        public bool EnableEAuth { get; set; }
    }

    public class IdentityServerTLSOptions
    {
        public string GL_MTLS_IDSRV_URL { get; set; }
        public string GL_IDSRV_CERT_COOKIE_DOMAIN { get; set; }
    }

    public class EAuthIntegrationOptions
    {       
        public string RequestedServiceOID { get; set; }
        public string RequestedProviderOID { get; set; }
        public string RequestedLevelOfAssurance { get; set; }
    }

    /// <summary>
    /// Опции за UserManagement
    /// </summary>
    public class UserManagementOptions
    {
        public ProfileRegistrationWhenMissingOptions ProfileRegistrationWhenMissing { get; set; }
    }

    /// <summary>
    /// Опции за клиенти на които да е позволена автоматична регистрация на потребителски профил
    /// </summary>
    public class ProfileRegistrationWhenMissingOptions
    {
        public ProfileRegistrationWhenMissingEnabledClientsOptions[] EnabledClients { get; set; }
    }

    /// <summary>
    /// Опции за автоматична регистрация на потребителски профил по клиент и вид автентификация
    /// </summary>
    public class ProfileRegistrationWhenMissingEnabledClientsOptions
    {
        public string ClientId { get; set; }
        public string AuthenticationMode { get; set; }
    }
}
