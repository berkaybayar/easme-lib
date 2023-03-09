using System.Security.Cryptography;
using System.Text;
using EasMe.Exceptions;
namespace EasMe
{
    public static class EasGenerate
    {
        const string CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        /// <summary>
        /// Generates and returns a random GUID string without "-"
        /// </summary>
        /// <returns></returns>
        public static string GenerateGuidString() => System.Guid.NewGuid().ToString().Replace("-","");
        public static string GetRandomString(ushort length)
        {
            var charArray = CHARACTERS.ToCharArray();
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                var random = RandomNumberGenerator.GetInt32(charArray.Length);
                var randomChar = charArray[random];
                result.Append(randomChar);
            }
            return result.ToString();
        }

        /// <summary>
        /// Generate a random string with a given length and characters. Max allowed length value is 1024.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="allowedChars"></param>
        /// <param name="onlyLetter"></param>
        /// <returns></returns>
        static string GenerateString(string chars, int length)
        {
            if (length > 2048) throw new TooBigValueException("Given length to create random string is too big. Max allowed length value is 1024.");
            if (length < 1) throw new TooSmallValueException("Given length to create random string is too small. Min allowed length value is 1.");
            var random = new Random();
            string resultToken = new(
               Enumerable.Repeat(chars, length)
               .Select(token => token[random.Next(token.Length)]).ToArray());
            return resultToken;
        }
        /// <summary>
        /// Generates and returns random string with a given length and allowed characters. By defualt it will allow letters and digits. Max allowed length value is 1024.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="allowedChars"></param>
        /// <param name="onlyLetter"></param>
        /// <returns></returns>
        public static string GenerateRandomString(int length, bool onlyLetter = false, string allowedChars = "")
        {
            string lowerAll = "abcdefghijklmnoprstuvwxyzq";
            string upperAll = "ABCDEFGHIJKLMNOPRSTUVWXYZQ";
            string digits = "0123456789";
            string allChars;
            if (onlyLetter)
            {

                allChars = lowerAll + upperAll + allowedChars;
                return GenerateString(allChars, length);
            }
            else
            {
                allChars = lowerAll + upperAll + digits + allowedChars;
                return GenerateString(allChars, length);

            }
        }

        /// <summary>
        /// Generates and returns random string that only contains digits. Max allowed lengthvalue is 1024.
        /// </summary>
        /// <param name="digitCount"></param>
        /// <returns></returns>
        public static int GenerateRandomNumbers(int digitCount)
        {
            string digits = "0123456789";
            var random = GenerateString(digits, digitCount);
            var RandomNumberStringAsInt32 = Convert.ToInt32(random);
            return RandomNumberStringAsInt32;

        }
    }
}
