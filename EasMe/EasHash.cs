using EasMe.Extensions;
using System.Security.Cryptography;

namespace EasMe
{
    public static class EasHash
    {

        private static byte[] ComputeHash(HashAlgorithm algorithm, string rawData) => algorithm.ComputeHash(rawData.ConvertToByteArray());
        public static byte[] ComputeSaltedHash(HashAlgorithm algorithm, string rawData, string saltStr)
        {
            var plainText = rawData.ConvertToByteArray();
            var salt = saltStr.ConvertToByteArray();
            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        public static byte[] MD5Hash(this string rawData) => ComputeHash(MD5.Create(), rawData);
        public static byte[] MD5HashSalted(this string rawData, string salt) => ComputeSaltedHash(MD5.Create(), rawData, salt);

        public static byte[] SHA1Hash(this string rawData) => ComputeHash(SHA1.Create(), rawData);
        public static byte[] SHA1HashSalted(this string rawData, string salt) => ComputeSaltedHash(SHA1.Create(), rawData, salt);


        public static byte[] SHA256Hash(this string rawData) => ComputeHash(SHA256.Create(), rawData);
        public static byte[] SHA256HashSalted(this string rawData, string salt) => ComputeSaltedHash(SHA256.Create(), rawData, salt);


        public static byte[] SHA384Hash(this string rawData) => ComputeHash(SHA384.Create(), rawData);
        public static byte[] SHA384HashSalted(this string rawData, string salt) => ComputeSaltedHash(SHA384.Create(), rawData, salt);


        public static byte[] SHA512Hash(this string rawData) => ComputeHash(SHA512.Create(), rawData);
        public static byte[] SHA512HashSalted(this string rawData, string salt) => ComputeSaltedHash(SHA512.Create(), rawData, salt);









    }
}
