using EasMe.Exceptions;
namespace EasMe
{
    public static class EasGenerate
    {
        /// <summary>
        /// Generate a random string with a given length and characters. Max allowed length value is 1024.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="allowedChars"></param>
        /// <param name="onlyLetter"></param>
        /// <returns></returns>
        static string GenerateString(string chars, int length)
        {
            if (length > 1024) throw new TooBigValueException("Give length to create random string is too big. Max allowed length value is 1024.");
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
        public static string GenerateRandomString(int length, string allowedChars = "", bool onlyLetter = false)
        {
            string lowerAll = "abcdefghijklmnoprstuvwxyz";
            string upperAll = lowerAll.ToUpper();
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
        /// Generates and returns random string that only contains letters. Max allowed length value is 1024.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomLetters(int length)
        {
            string lowerAll = "abcdefghijklmnoprstuvwxyz";
            string upperAll = lowerAll.ToUpper();
            string allChars = lowerAll + upperAll;
            return GenerateString(allChars, length);
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
            var RandomStringAsInt32 = Convert.ToInt32(random);
            return RandomStringAsInt32;

        }
    }
}
