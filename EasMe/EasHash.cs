using System.Security.Cryptography;
using System.Text;

namespace EasMe
{
    public static class EasHash
    {
        /// <summary>
        /// Converts hashed byte array to string
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BuildString(this byte[] bytes)
        {
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
        public static byte[] ConvertStringToByteArray(this string yourStr) => Encoding.UTF8.GetBytes(yourStr);
        public static string ConvertBytesToString(this byte[] byteArray) => Encoding.Default.GetString(byteArray);
        private static byte[] ComputeHash(HashAlgorithm algorithm, string rawData) => algorithm.ComputeHash(rawData.ConvertStringToByteArray());
        public static byte[] ComputeSaltedHash(HashAlgorithm algorithm, string rawData, string saltStr)
        {
            var plainText = ConvertStringToByteArray(rawData);
            var salt = ConvertStringToByteArray(saltStr);
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




        private static string HexBytesToString(this byte[] bt)
        {

            try
            {
                string s = string.Empty;
                for (int i = 0; i < bt.Length; i++)
                {
                    byte b = bt[i];
                    int n, n1, n2;
                    n = (int)b;
                    n1 = n & 15;
                    n2 = (n >> 4) & 15;
                    if (n2 > 9)
                        s += ((char)(n2 - 10 + (int)'A')).ToString();
                    else
                        s += n2.ToString();
                    if (n1 > 9)
                        s += ((char)(n1 - 10 + (int)'A')).ToString();
                    else
                        s += n1.ToString();
                    if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
                }
                return s;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Compares 2 byte arrays for equality.
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns>true if arrays are equal</returns>
        public static bool CompareByteArrays(this byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }


    }
}
