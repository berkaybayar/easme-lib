using System;
using System.Linq;

namespace EasMe
{
    public static class EasGenerate
    {
        /// <summary>
        /// Generate a random string with a given length and allowed characters
        /// </summary>
        /// <param name="length"></param>
        /// <param name="allowedChars"></param>
        /// <param name="onlyLetter"></param>
        /// <returns></returns>
        static string GenerateString(string chars,int length)
        {          
            var random = new Random();
            string resultToken = new(
               Enumerable.Repeat(chars, length)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            return resultToken;
        }
        public static string GenerateRandomString(int length, string allowedChars = "", bool onlyLetter = false)
        {
            string lowerAll = "abcdefghijklmnoprstuvwxyz";
            string upperAll = lowerAll.ToUpper();
            string digits = "0123456789";
            string allChars;
            if (onlyLetter)
            {

                allChars = lowerAll + upperAll + allowedChars;
                return GenerateString(allChars,length);                
            }
            else
            {
                allChars = lowerAll + upperAll + digits + allowedChars;
                return GenerateString(allChars, length);
                            
            }            
        }
      
        public static string GenerateRandomLetters(int length)
        {
            string lowerAll = "abcdefghijklmnoprstuvwxyz";
            string upperAll = lowerAll.ToUpper();
            string allChars = lowerAll + upperAll;
            return GenerateString(allChars, length);
        }
        public static string GenerateRandomNumbers(int digitCount)
        {
            string digits = "0123456789";
            return GenerateString(digits, digitCount);

        }
    }
}
