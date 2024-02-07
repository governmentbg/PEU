using System.Security.Cryptography;
using System.Text;

namespace EAU.Payments.PaymentRequests.Epay.Security
{
    public class SecretKey
    {
        public static string GetCheckSum(string data, string password)
        {
            Encoding encoding = Encoding.ASCII;
            using (HMACSHA1 hmacsha = new HMACSHA1(encoding.GetBytes(password)))
            {
                byte[] bytes = encoding.GetBytes(data);

                return ByteToHex(hmacsha.ComputeHash(bytes));
            }
        }

        public static bool IsCheckSumValid(string checksum, string encoded, string password)
        {
            string encodedChecksum = GetCheckSum(encoded, password);

            return checksum.ToUpper() == encodedChecksum.ToUpper();
        }

        private static string ByteToHex(byte[] buff)
        {
            StringBuilder ret = new StringBuilder(buff.Length * 2);

            for (int i = 0; i < buff.Length; i++)
            {
                ret.Append(buff[i].ToString("X2"));
            }
            return ret.ToString();
        }
    }
}
