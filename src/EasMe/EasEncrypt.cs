using System.Security.Cryptography;
using System.Text;

namespace EasMe;

/// <summary>
///     Basic string encryption and decryption class.
/// </summary>
public class EasEncrypt
{
    private static string _encryptKey = null!;

    public EasEncrypt(string key)
    {
        _encryptKey = key;
    }

    public string EncryptString(string plainText)
    {
        var iv = new byte[16];
        byte[] array;

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_encryptKey);
        aes.IV = iv;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using var streamWriter = new StreamWriter(cryptoStream);
        streamWriter.Write(plainText);

        array = memoryStream.ToArray();

        return Convert.ToBase64String(array);
    }

    public string DecryptString(string cipherText)
    {
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_encryptKey);
        aes.IV = iv;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream memoryStream = new(buffer);
        using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);
        return streamReader.ReadToEnd();
    }
}