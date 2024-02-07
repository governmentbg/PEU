namespace Microsoft.AspNetCore.Authentication
{
    public static class AuthenticateResultExtensions
    {
        public static bool IsRegisteringAuthentication(this AuthenticateResult authenticateResult)
        {
            return authenticateResult.Properties.Items.ContainsKey("addauthentication")
                    && bool.TryParse(authenticateResult.Properties.Items["addauthentication"], out bool addauthentication)
                    && addauthentication;
        }
    }
}
