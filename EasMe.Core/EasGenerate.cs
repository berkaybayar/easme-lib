using System;
using System.Linq;

namespace EasMe
{
    public class EasGenerate
    {

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
