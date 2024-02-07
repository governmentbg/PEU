namespace EAU.Web.IdentityServer.Common
{
    internal static class ClaimsHelper
    {
        public static string BuildSubClaimValueForCIN(int cin) => $"cin:{cin}";
        public static string BuildSubClaimValueForCertThumbprint(string vlaue) => $"certtb:{vlaue}";
        public static string BuildSubClaimValueForPersonalId(string value) => $"pid:{value}";
    }
}
