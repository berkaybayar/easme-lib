using System.Collections;
using System.Data.HashFunction;
using System.Data.HashFunction.xxHash;
using System.Security.Cryptography;
using System.Text;
using EasMe.Extensions;

namespace EasMe;

public static class EasHash
{
    private static byte[] ComputeHash(HashAlgorithm algorithm, string rawData)
    {
        return algorithm.ComputeHash(rawData.ConvertToByteArray());
    }

    public static byte[] ComputeSaltedHash(HashAlgorithm algorithm, string rawData, string saltStr)
    {
        var plainText = rawData.ConvertToByteArray();
        var salt = saltStr.ConvertToByteArray();
        var plainTextWithSaltBytes =
            new byte[plainText.Length + salt.Length];

        for (var i = 0; i < plainText.Length; i++) plainTextWithSaltBytes[i] = plainText[i];
        for (var i = 0; i < salt.Length; i++) plainTextWithSaltBytes[plainText.Length + i] = salt[i];

        return algorithm.ComputeHash(plainTextWithSaltBytes);
    }

    public static byte[] MD5Hash(this string rawData)
    {
        return ComputeHash(MD5.Create(), rawData);
    }

    public static byte[] MD5HashSalted(this string rawData, string salt)
    {
        return ComputeSaltedHash(MD5.Create(), rawData, salt);
    }

    public static byte[] SHA1Hash(this string rawData)
    {
        return ComputeHash(SHA1.Create(), rawData);
    }

    public static byte[] SHA1HashSalted(this string rawData, string salt)
    {
        return ComputeSaltedHash(SHA1.Create(), rawData, salt);
    }


    public static byte[] SHA256Hash(this string rawData)
    {
        return ComputeHash(SHA256.Create(), rawData);
    }

    public static byte[] SHA256HashSalted(this string rawData, string salt)
    {
        return ComputeSaltedHash(SHA256.Create(), rawData, salt);
    }


    public static byte[] SHA384Hash(this string rawData)
    {
        return ComputeHash(SHA384.Create(), rawData);
    }

    public static byte[] SHA384HashSalted(this string rawData, string salt)
    {
        return ComputeSaltedHash(SHA384.Create(), rawData, salt);
    }


    public static byte[] SHA512Hash(this string rawData)
    {
        return ComputeHash(SHA512.Create(), rawData);
    }

    public static byte[] SHA512HashSalted(this string rawData, string salt)
    {
        return ComputeSaltedHash(SHA512.Create(), rawData, salt);
    }


    public static string FileMD5Hash(string path)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(path);
        return Convert.ToHexString(md5.ComputeHash(stream));
    }

    public static string FileXXHash(string path)
    {
        var factory = xxHashFactory.Instance.Create();
        using var stream = File.OpenRead(path);
        var hashed = factory.ComputeHash(stream).AsHexString();
        return hashed;
    }

    public static IHashValue XXHash(this string input)
    {
        var factory = xxHashFactory.Instance.Create();
        var hashed = factory.ComputeHash(Encoding.UTF8.GetBytes(input));
        return hashed;
    }

    public static string XXHashAsHexString(this string input)
    {
        var factory = xxHashFactory.Instance.Create();
        var hashed = factory.ComputeHash(Encoding.UTF8.GetBytes(input)).AsHexString();
        return hashed;
    }

    public static string XXHashAsBase64String(this string input)
    {
        var factory = xxHashFactory.Instance.Create();
        var hashed = factory.ComputeHash(Encoding.UTF8.GetBytes(input)).AsBase64String();
        return hashed;
    }

    public static BitArray XXHashAsBitArray(string input)
    {
        var factory = xxHashFactory.Instance.Create();
        var hashed = factory.ComputeHash(Encoding.UTF8.GetBytes(input)).AsBitArray();
        return hashed;
    }
}