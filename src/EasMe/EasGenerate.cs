using System.Security.Cryptography;
using System.Text;
using EasMe.Exceptions;

namespace EasMe;

public static class EasGenerate
{
    private const string CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    /// <summary>
    ///     Generates and returns a random GUID string without "-"
    /// </summary>
    /// <returns></returns>
    public static string GenerateGuidString()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }

    public static string GetRandomString(ushort length)
    {
        var charArray = CHARACTERS.ToCharArray();
        var result = new StringBuilder(length);
        for (var i = 0; i < length; i++)
        {
            var random = RandomNumberGenerator.GetInt32(charArray.Length);
            var randomChar = charArray[random];
            result.Append(randomChar);
        }

        return result.ToString();
    }

    /// <summary>
    ///     Generate a random string with a given length and characters. Max allowed length value is 1024.
    /// </summary>
    /// <param name="chars"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private static string GenerateString(string chars, int length)
    {
        if (length > 2048)
            throw new TooBigValueException(
                "Given length to create random string is too big. Max allowed length value is 1024.");
        if (length < 1)
            throw new TooSmallValueException(
                "Given length to create random string is too small. Min allowed length value is 1.");
        var random = new Random();
        string resultToken = new(
            Enumerable.Repeat(chars, length)
                .Select(token => token[random.Next(token.Length)]).ToArray());
        return resultToken;
    }

    /// <summary>
    ///     Generates and returns random string with a given length and allowed characters. By defualt it will allow letters
    ///     and digits. Max allowed length value is 1024.
    /// </summary>
    /// <param name="length"></param>
    /// <param name="allowedChars"></param>
    /// <param name="onlyLetter"></param>
    /// <returns></returns>
    public static string GenerateRandomString(int length, bool onlyLetter = false, string allowedChars = "")
    {
        const string lowerAll = "abcdefghijklmnoprstuvwxyzq";
        const string upperAll = "ABCDEFGHIJKLMNOPRSTUVWXYZQ";
        const string digits = "0123456789";
        string allChars;
        if (onlyLetter)
        {
            allChars = lowerAll + upperAll + allowedChars;
            return GenerateString(allChars, length);
        }

        allChars = lowerAll + upperAll + digits + allowedChars;
        return GenerateString(allChars, length);
    }

    /// <summary>
    ///     Generates and returns random string that only contains digits. Max allowed lengthvalue is 1024.
    /// </summary>
    /// <param name="digitCount"></param>
    /// <returns></returns>
    public static long GenerateRandomNumbers(int digitCount)
    {
        const string digits = "0123456789";
        var random = GenerateString(digits, digitCount);
        var randomNumberStringAsInt32 = Convert.ToInt64(random);
        return randomNumberStringAsInt32;
    }
}