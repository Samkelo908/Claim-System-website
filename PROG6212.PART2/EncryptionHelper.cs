using System.Security.Cryptography;

namespace PROG6212.PART2
{
    public class EncryptionHelper
    {
     private static readonly byte[] key = Convert.FromBase64String("2k3J3fJNz5Pt5L4yJGtKaXhTfBG72A8bDcvZ8avHCA0=");
    private static readonly byte[] IV = Convert.FromBase64String("sS1XBhR1ld/YbKf1y1Asmw==");

        private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)

        {

            using (var memoryStream = new MemoryStream())

            {

                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))

                {

                    cryptoStream.Write(data, 0, data.Length);

                    cryptoStream.FlushFinalBlock();

                    return memoryStream.ToArray();

                }

            }

        }



        public static byte[] Encrypt(byte[] data)
        {

            using (Aes aes = Aes.Create())

            {

                aes.Key = key;

                aes.IV = IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))

                {

                    return PerformCryptography(data, encryptor);

                }

            }

        }



        public static byte[] Decrypt(byte[] data)
        {

            using (Aes aes = Aes.Create())

            {

                aes.Key = key;

                aes.IV = IV;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))

                {

                    return PerformCryptography(data, decryptor);

                }

            }

        }













    }

}