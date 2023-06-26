using System.Security.Cryptography;
using System.Text;

namespace EasMe;

/// <summary>
///     Encryption manager for encrypting and decrypting strings. Different instances of this class will have different
///     keys.
///     For client and server matching key, key is generated based on a time formula. It will be same if created in same
///     second.
/// </summary>
public class EasEncrypt
{
    private static string _additionalKey = "XmMqPLuQkkrKaNmCvRGyceZNDdquhTJokckfPdcKPjeekkooeaSmBGDNwEaqDFJq";
    private static short _seed;
    private static bool _dontUseEncryption;
    private readonly byte[] _ivBytes;
    private readonly byte[] _keyBytes;

    public EasEncrypt() {
        var key = CreateKey();
        using var sha256 = SHA256.Create();
        var keyHash = sha256.ComputeHash(key);
        _keyBytes = new byte[32];
        _ivBytes = new byte[16];
        Array.Copy(keyHash, _keyBytes, 32);
        Array.Copy(keyHash, 0, _ivBytes, 0, 16);
    }

    /// <summary>
    ///     Set whether to use encryption or not. If set to false, encryption will not be used. You can pass debug variables
    ///     here for testing.
    /// </summary>
    /// <param name="dontUseEncryption"></param>
    public static void SetDontUseEncryption(bool dontUseEncryption) {
        _dontUseEncryption = dontUseEncryption;
    }

    public static void SetAdditionalKey(string key) {
        _additionalKey = key;
    }

    public static void SetSeed(short seed) {
        _seed = seed;
    }

    private static DateTime GetDate() {
        var now = DateTime.UtcNow;
        var second = now.Second;
        var ms = now.Millisecond;
        now = now.AddHours(+second + _seed);
        now = now.AddYears(-second - _seed);
        now = now.AddMonths(+second + _seed);
        now = now.AddDays(-second - _seed);
        now = now.AddMinutes(+second + _seed);
        now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        return now;
    }


    private static byte[] CreateKey() {
        var time = GetDate();
        var ticks = time.Ticks;
        var str = ticks + "|" + _additionalKey;
        var key = str.SHA256Hash();
        return key;
    }

    public string Encrypt(string plainText) {
        if (_dontUseEncryption) return plainText;
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        using var aes = Aes.Create();
        aes.Key = _keyBytes;
        aes.IV = _ivBytes;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedText) {
        if (_dontUseEncryption) return encryptedText;
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        using var aes = Aes.Create();
        aes.Key = _keyBytes;
        aes.IV = _ivBytes;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    public Stream DecryptStream(Stream stream) {
        if (_dontUseEncryption) return stream;
        using var aes = Aes.Create();
        aes.Key = _keyBytes;
        aes.IV = _ivBytes;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var cryptoStream = new CryptoStream(stream, decryptor, CryptoStreamMode.Read);
        return cryptoStream;
    }

    public Stream EncryptStream(Stream stream) {
        if (_dontUseEncryption) return stream;
        using var aes = Aes.Create();
        aes.Key = _keyBytes;
        aes.IV = _ivBytes;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var cryptoStream = new CryptoStream(stream, encryptor, CryptoStreamMode.Read);
        return cryptoStream;
    }
}