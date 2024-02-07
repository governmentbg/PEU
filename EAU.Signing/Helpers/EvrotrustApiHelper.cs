using System;

namespace EAU.Signing.Helpers
{
    internal class EvrotrustApiHelper
    {
        public static string GetAuthorizationHeader(string api_key_value, string jason_value)
        {
            // 1. sha256 an api_key
            string api_key_sha256 = EvrotrustCryptoUtils.GetSHA256String(api_key_value).ToLower();

            // 2.  jason_value to hex
            string jason_hex = EvrotrustConvUtils.StringToHex(jason_value);

            try
            {
                // 3. AUTHORIZATION = HMAC(SHA256, DATA_TO_HEX, VENDOR_API_KEY_SHA256)
                return EvrotrustCryptoUtils.GetHMACSHA256(jason_hex, api_key_sha256);
            }
            finally
            {
                api_key_sha256 = null;
                jason_hex = null;
            }
        }

        public static long UnixTimeNow()
        {
            return (long)(DateTime.UtcNow.AddYears(2) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        public static long UnixTime(DateTime inpDate)
        {
            return (long)(inpDate.AddYears(2) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        public static bool StatusCodeValidation(int StatusCode, out string msgText)
        {
            switch (StatusCode)
            {
                case 200:
                    msgText = "OK";
                    return true;
                case 204:
                    msgText = "User found";
                    return true;
                case 400:
                    msgText = "Invalid data supplied!";
                    return false;
                case 401:
                    msgText = "Unauthorized!";
                    return false;
                case 438:
                    msgText = "User not found!";
                    return false;
                case 454:
                    msgText = "Incorrect coverage!";
                    return false;
                default:
                    msgText = "Unknown!";
                    return false;
            }
        }
    }
}
