using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace EAU.Signing.Helpers
{
    internal class EvrotrustCryptoUtils
    {
        //Work only for .net framework
        //public static string RijndaelDecrypt(string text, byte[] key, byte[] iv)
        //{
        //    using (RijndaelManaged rj = new RijndaelManaged())
        //    {
        //        rj.KeySize = 256;
        //        rj.BlockSize = 256;
        //        rj.Padding = PaddingMode.Zeros;
        //        rj.Mode = CipherMode.CBC;

        //        rj.Key = key;
        //        rj.IV = iv;

        //        using (ICryptoTransform AESDecrypt = rj.CreateDecryptor(rj.Key, rj.IV))
        //        {
        //            byte[] buffer = Convert.FromBase64String(text);

        //            try
        //            {
        //                byte[] result = AESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length);
        //                string base64 = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
        //                return base64.TrimEnd('\0');
        //            }
        //            finally
        //            {
        //                buffer = null;
        //            }
        //        }
        //    }
        //}

        public static string RijndaelDecrypt(string cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold the decrypted text.
            string plaintext = null;

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            Org.BouncyCastle.Crypto.Engines.RijndaelEngine engine = new Org.BouncyCastle.Crypto.Engines.RijndaelEngine(256);

            var blockCipher = new Org.BouncyCastle.Crypto.Modes.CbcBlockCipher(engine);
            var cipher = new Org.BouncyCastle.Crypto.Paddings.PaddedBufferedBlockCipher(blockCipher, new Org.BouncyCastle.Crypto.Paddings.ZeroBytePadding());
            ICipherParameters keyParam = new KeyParameter(Key);
            ICipherParameters keyParamWithIV = new ParametersWithIV(keyParam, IV, 0, 32);

            cipher.Init(false, keyParamWithIV);
            byte[] decryptedBytes = new byte[cipher.GetOutputSize(cipherTextBytes.Length)];
            int length = cipher.ProcessBytes(cipherTextBytes, decryptedBytes, 0);
            cipher.DoFinal(decryptedBytes, length);

            plaintext = System.Text.Encoding.UTF8.GetString(decryptedBytes);
            
            return plaintext.TrimEnd('\0');
        }

        /// <summary>
        /// RSA decryption PKCS1 with PEM encodet private key
        /// </summary>
        /// <param name="DataToDecrypt">byte[]</param>
        /// <param name="privKeyXml">string</param>
        /// <param name="PrivKeySize">int</param>
        /// <returns>byte[]</returns>
        /// /// <remarks>Ivan Blagoev</remarks>
        public static byte[] Pkcs1Decrypt(byte[] DataToDecrypt, string privKeyXml, int PrivKeySize)
        {
            //PemReader pr = new PemReader(new StringReader(privKeyXml));
            //AsymmetricCipherKeyPair KeyPair = (AsymmetricCipherKeyPair)pr.ReadObject();
            //RSAParameters rsaParams = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)KeyPair.Private);

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(PrivKeySize))
            {
                //rsa.FromXmlString(privKeyXml);
                RSAKeys.FromPrivateKeyXmlString(rsa, privKeyXml);

                return rsa.Decrypt(DataToDecrypt, false);
            }
        }

        public static string GetSHA256String(string inputString)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(inputString)))
            {
                using (SHA256 Sha256 = SHA256.Create())
                {
                    return BitConverter.ToString(Sha256.ComputeHash(stream)).Replace("-", String.Empty); ;
                }
            }
        }

        public static string GetSha256ToB64(string Utf8String)
        {
            byte[] inp = Encoding.UTF8.GetBytes(Utf8String);

            using (SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider())
            {
                return Convert.ToBase64String(sha256.ComputeHash(inp));
            }
        }

        public static string GetHMACSHA256(string jason_value, string api_key_value)
        {

            byte[] api_key_byte = EvrotrustConvUtils.PackH(api_key_value);
            byte[] byte_hex = EvrotrustConvUtils.PackH(jason_value);

            using (HMACSHA256 hmacsha256 = new HMACSHA256(api_key_byte))
            {
                byte[] hashmessage_arr = hmacsha256.ComputeHash(byte_hex);

                try
                {
                    return EvrotrustConvUtils.BytesToHex(hashmessage_arr, true);
                }
                finally
                {
                    api_key_byte = null;
                    byte_hex = null;
                    hashmessage_arr = null;
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/23739932/2860309
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="length"></param>
        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        /// <summary>
        /// https://stackoverflow.com/a/23739932/2860309
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
        /// <param name="forceUnsigned"></param>
        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }

        public class RSAKeys
        {
            #region Evrotrust code

            public static void GenerateRSAToPem(int KeyLenght, out string PrivKeyXml, out string PubKeyPem)
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(KeyLenght))
                {
                    AsymmetricCipherKeyPair rsaKeyPair = GetRsaKeyPair(rsa);

                    RsaKeyParameters publicKey = (RsaKeyParameters)rsaKeyPair.Public;
                    RsaKeyParameters privateKey = (RsaKeyParameters)rsaKeyPair.Private;

                    using (StringWriter publicWriter = new StringWriter())
                    {
                        PemWriter pemWriter = new PemWriter(publicWriter);
                        pemWriter.WriteObject(publicKey);
                        pemWriter.Writer.Flush();
                        PubKeyPem = SecureStringToString(StringToSecureString(Base64Encode(publicWriter.ToString())));
                    }

                    PrivKeyXml = GetPrivateKeyXmlString(rsa);
                }
            }

            public static string SecureStringToString(SecureString value)
            {
                IntPtr valuePtr = IntPtr.Zero;

                try
                {
                    valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                    return Marshal.PtrToStringUni(valuePtr);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                }
            }

            public static SecureString StringToSecureString(string input)
            {
                SecureString output = new SecureString();

                int l = input.Length;
                char[] s = input.ToCharArray(0, l);
                foreach (var c in s)
                {
                    output.AppendChar(c);
                }

                try
                {
                    return output;
                }
                finally
                {
                    s = null;
                }
            }

            public static string Base64Encode(string plainText)
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                try
                {
                    return Convert.ToBase64String(plainTextBytes);
                }
                finally
                {
                    plainTextBytes = null;
                }
            }

            #endregion

            private static AsymmetricCipherKeyPair GetRsaKeyPair(RSA rsa)
            {
                return GetRsaKeyPair(rsa.ExportParameters(true));
            }

            private static AsymmetricCipherKeyPair GetRsaKeyPair(RSAParameters rp)
            {
                Org.BouncyCastle.Math.BigInteger modulus = new Org.BouncyCastle.Math.BigInteger(1, rp.Modulus);
                Org.BouncyCastle.Math.BigInteger pubExp = new Org.BouncyCastle.Math.BigInteger(1, rp.Exponent);

                RsaKeyParameters pubKey = new RsaKeyParameters(
                    false,
                    modulus,
                    pubExp);

                RsaPrivateCrtKeyParameters privKey = new RsaPrivateCrtKeyParameters(
                    modulus,
                    pubExp,
                    new Org.BouncyCastle.Math.BigInteger(1, rp.D),
                    new Org.BouncyCastle.Math.BigInteger(1, rp.P),
                    new Org.BouncyCastle.Math.BigInteger(1, rp.Q),
                    new Org.BouncyCastle.Math.BigInteger(1, rp.DP),
                    new Org.BouncyCastle.Math.BigInteger(1, rp.DQ),
                    new Org.BouncyCastle.Math.BigInteger(1, rp.InverseQ));

                return new AsymmetricCipherKeyPair(pubKey, privKey);
            }

            public static string GetPrivateKeyXmlString(RSA rsa)
            {
                RSAParameters parameters = rsa.ExportParameters(true);

                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                      parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                      parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                      parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                      parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                      parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                      parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                      parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                      parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
            }

            public static void FromPrivateKeyXmlString(RSA rsa, string privateKeyXmlString)
            {
                RSAParameters parameters = new RSAParameters();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.LoadXml(privateKeyXmlString);

                if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
                {
                    foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "Modulus": parameters.Modulus = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "Exponent": parameters.Exponent = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "P": parameters.P = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "Q": parameters.Q = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "DP": parameters.DP = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "DQ": parameters.DQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "InverseQ": parameters.InverseQ = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                            case "D": parameters.D = (string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText)); break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Invalid XML RSA key.");
                }

                rsa.ImportParameters(parameters);
            }
        }
    }
}
