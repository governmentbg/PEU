using System.Collections.Generic;

namespace EAU.Web.IdentityServer
{
    public static class HeaderNamesConstants
    {
        public static readonly string ForwardedSiteBasePath = "X-Forwarded-SiteBasePath";
    }

    public static class CustomGrantTypes
    {
        public const string Delegation = "delegation";
        public const string WeakDelegation = "weak_delegation";
    }

    /// <summary>
    /// Констатни за ClaimTypes.
    /// </summary>
    public static class ClaimTypesConstants
    {
        /// <summary>
        /// amr claim type for certificate
        /// </summary>
        public static readonly string AmrCertificate = "cert";
        public static readonly string AmrEauth = "eauth";
    }

    public static class EndpointConstants
    {
        public static readonly string AuthorizeCallbackEndpointAddress = "/connect/authorize/callback";
    }

    public static class EAuthConstants
    {
        public const string AssertionConsumerServiceUrl = "/SAML/AssertionConsumerService";
        public const string SingleLogoutServiceUrl = "/SAML/SingleLogoutService";
        public const string ArtifactResolutionServiceUrl = "/SAML/ArtifactResolutionService";
        public const string LoginCompletionUrl = "/Saml/SsoCallback";
    }

    public static class ReturnReasons
    {
        public static string EAuthNotAttachedToUser = "EAuthNotAttachedToUser";
        public static string DefaultErrorText = "Грешка при вход в системата";

        public static Dictionary<string, string> Items = new Dictionary<string, string>
        {
            { EAuthNotAttachedToUser, "GL_00027_E" }
        };
    }
}
