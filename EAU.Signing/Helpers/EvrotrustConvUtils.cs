using System;
using System.Text;

namespace EAU.Signing.Helpers
{
    internal class EvrotrustConvUtils
    {
        public static string StringToHex(string inputString)
        {
            StringBuilder ret = new StringBuilder("");

            foreach (char c in inputString)
            {
                int tmp = c;

                ret.Append(String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString())));
            }

            try
            {
                return ret.ToString();
            }
            finally
            {
                ret = null;
            }
        }

        public static string BytesToHex(byte[] barray, bool toLowerCase = true)
        {
            byte addByte = 0x37;
            if (toLowerCase) addByte = 0x57;
            char[] c = new char[barray.Length * 2];
            byte b;

            for (int i = 0; i < barray.Length; ++i)
            {
                b = ((byte)(barray[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + addByte : b + 0x30);
                b = ((byte)(barray[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + addByte : b + 0x30);
            }

            return new string(c);
        }

        public static byte[] PackH(string hex)
        {
            if ((hex.Length % 2) == 1) hex += '0';
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            try
            {
                return bytes;
            }
            finally
            {
                bytes = null;
            }
        }
    }
}
