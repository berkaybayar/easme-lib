using System;
using System.Linq;

namespace EasMe
{
    public class EasGenerate
    {
        /// <summary>
        /// Generate a random string with a given length and allowed characters
        /// </summary>
        /// <param name="length"></param>
        /// <param name="allowedChars"></param>
        /// <param name="onlyLetter"></param>
        /// <returns></returns>
        public string GenerateRandomString(int length, string allowedChars = "", bool onlyLetter = false)
        {
            string lowerAll = "abcdefghijklmnoprstuvwxyz";
            string upperAll = lowerAll.ToUpper();
            string digits = "0123456789";
            string allChars;
            if (onlyLetter)
            {
                allChars = lowerAll + upperAll + allowedChars;
            }
            else
            {
                allChars = lowerAll + upperAll + digits + allowedChars;
            }
            var random = new Random();
            string resultToken = new string(
               Enumerable.Repeat(allChars, length)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            return resultToken;
        }

    }
}
