using System.Security.Cryptography;
using System.Text;

namespace EasMe;

/// <summary>
///     Basic string encryption and decryption class.
/// </summary>
public class EasEncrypt
{
    private static string _encryptKey = null!;

    private static readonly byte[] _salt = { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
    public EasEncrypt(string key)
    {
        _encryptKey = key;
    }


    public string EncryptString(string clearText)
    {
        var clearBytes = Encoding.Unicode.GetBytes(clearText);
        using var aes = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(_encryptKey, _salt);
        aes.Key = pdb.GetBytes(32);
        aes.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(clearBytes, 0, clearBytes.Length);
        cs.Close();
        clearText = Convert.ToBase64String(ms.ToArray());
        return clearText;
    }
    public string DecryptString(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        var cipherBytes = Convert.FromBase64String(cipherText);
        using var aes = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(_encryptKey, _salt);
        aes.Key = pdb.GetBytes(32);
        aes.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(cipherBytes, 0, cipherBytes.Length);
        cs.Close();
        cipherText = Encoding.Unicode.GetString(ms.ToArray());
        return cipherText;
    }
}