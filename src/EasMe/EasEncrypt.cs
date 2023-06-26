using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using EasMe.Extensions;

namespace EasMe;


public class EasEncrypt
{
    public enum Sensitivity
    {
        Month,
        Day, 
        Hour,
        Minutes, 
        Seconds, 
    }
    private static bool _dontUseEncryption;
    private readonly byte[] _salt;

    private static byte[] _ivBytes;
    private static byte[] _keyBytes;

    private static bool _useTimeSeeding = false;
    private static Sensitivity _encryptionSensitivity= Sensitivity.Minutes;
    private static int _seed = 0;

    // private static string _oldSaltTest = ""; 
    public EasEncrypt() {
        _salt = CreateSalt();
        
        // var oldSalt = _salt.ToHexString();
        // if (oldSalt != _oldSaltTest) {
        //     _oldSaltTest = oldSalt;
        //     Console.WriteLine($"Salt: {_salt.ToHexString()}");
        // }
    }

    public static void SetStaticKey(string key) {
        _keyBytes = key.SHA256Hash();
        _ivBytes = new byte[16];
        Array.Copy(_keyBytes, 0, _ivBytes, 0, 16);
    }


    
    /// <summary>
    /// Enables time seeding and disables static salt key.
    /// Each instance of EasEncrypt will have different salt key depending on the time it is created and the sensitivity set.
    /// Be careful using this, same text encrypted text will differ depending on the time it is encrypted.
    /// Old encrypted texts will not be able to be decrypted depending on sensitivity.
    /// </summary>
    /// <param name="encryptionSensitivity">
    /// Time sensitivity of the encryption. If set to month, same text encrypted in different months will be different.
    /// </param>
    /// <param name="seed">
    /// Static changes the time depending on the seed. It is recommended to have a calculated build version number here.
    /// </param>
    public static void UseTimeSeeding(Sensitivity encryptionSensitivity, int seed = 0) {
        _useTimeSeeding = true;
        _encryptionSensitivity = encryptionSensitivity;
        _seed = seed;
    }
    /// <summary>
    /// Whether to disable encryption.
    /// Only call this when debugging and only when necessary to see plain text.
    /// </summary>
    public static void DontUseEncryption() {
        _dontUseEncryption = true;
    }


    /// <summary>
    /// Gets secondary seed value depending on sensitivity.
    /// </summary>
    /// <param name="now"></param>
    /// <param name="sensitivityNum"></param>
    /// <returns></returns>
    private static int GetSeedValueBySensitivity(DateTime now, int sensitivityNum) {
        if (sensitivityNum >= 4)
            return now.Second;
        if (sensitivityNum >= 3)
            return now.Minute;
        if (sensitivityNum >= 2)
            return now.Hour;
        if (sensitivityNum >= 1)
            return now.Day;
        return now.Month;
    }

    private static DateTime GetSeedDateBySensitivity(DateTime now, int sensitivityNum) {
        if (sensitivityNum >= 4)
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
        if (sensitivityNum >= 3)
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
        if (sensitivityNum >= 2)
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
        if (sensitivityNum >= 1)
            return new DateTime(now.Year, now.Month, now.Day);
        return new DateTime(now.Year, now.Month, now.Day);
    }

    /// <summary>
    /// Gets the salt date depending on sensitivity.
    /// </summary>
    /// <returns></returns>
    private DateTime GetSaltDate() {
        var now = DateTime.UtcNow;
        var sensitivityNum = (int)_encryptionSensitivity;
        var seeded = GetSeedValueBySensitivity(now, sensitivityNum);
        if (sensitivityNum >= 4)
            now = now.AddSeconds(seeded - _seed);
        if (sensitivityNum >= 3)
            now = now.AddMinutes(seeded + _seed);
        if (sensitivityNum >= 2)
            now = now.AddHours(seeded + _seed);
        if (sensitivityNum >= 1)
            now = now.AddDays(seeded - _seed);
        now = now.AddMonths(seeded + _seed);
        now = now.AddYears(seeded - _seed);
        return GetSeedDateBySensitivity(now, sensitivityNum);
    }

    /// <summary>
    /// Creates salt. If time seeding is enabled, it will use the time as the salt.
    /// </summary>
    /// <returns></returns>
    private byte[] CreateSalt() {
        if (_useTimeSeeding) {
            var saltDate = GetSaltDate();
            var ticks = saltDate.Ticks;
            var saltBytes = BitConverter.GetBytes(ticks);
            // Console.WriteLine($"{saltDate} - {ticks} - {saltBytes.ToHexString()}");
            return saltBytes;
        }
        var salt = new byte[16];
        Array.Copy(_keyBytes, 0, salt, 0, 16);
        return salt;
    }

    /// <summary>
    /// Encrypts the plain text.
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public string Encrypt(string plainText) {
        if (_dontUseEncryption) return plainText;
        if(_keyBytes is null) throw new Exception("Key bytes are null. Call EasEncrypt.SetStaticKey first.");
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var saltedPlaintextBytes = SaltedBytes(plainBytes);
        using var aes = Aes.Create();
        aes.Key = _keyBytes;
        aes.IV = _ivBytes;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var encryptedBytes = encryptor.TransformFinalBlock(saltedPlaintextBytes, 0, saltedPlaintextBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// Decrypts the encrypted text.
    /// </summary>
    /// <param name="encryptedText"></param>
    /// <returns></returns>
    public string Decrypt(string encryptedText) {
        if (_dontUseEncryption) return encryptedText;
        if(_keyBytes is null) throw new Exception("Key bytes are null. Call EasEncrypt.SetStaticKey first.");
        var encryptedBytes = Convert.FromBase64String(encryptedText);
        using var aes = Aes.Create();
        aes.Key = _keyBytes;
        aes.IV = _ivBytes;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        var saltRemovedBytes = UnSaltedBytes(decryptedBytes);
        return Encoding.UTF8.GetString(saltRemovedBytes);
    }

    /// <summary>
    /// Adds salt to bytes. Used before encrpytion.
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    private byte[] SaltedBytes(byte[] bytes) {
        var combinedBytes = new byte[bytes.Length + _salt.Length];
        Buffer.BlockCopy(bytes, 0, combinedBytes, 0, bytes.Length);
        Buffer.BlockCopy(_salt, 0, combinedBytes, bytes.Length, _salt.Length);
        return combinedBytes;
    }

    /// <summary>
    /// Removes the salt from the bytes. Used after encryption.
    /// </summary>
    /// <param name="combined"></param>
    /// <returns></returns>
    private byte[] UnSaltedBytes(byte[] combined) {
        var bytesLength = combined.Length - _salt.Length;
        if (bytesLength < 0) return combined;
        var bytes = new byte[bytesLength];
        Buffer.BlockCopy(combined, 0, bytes, 0, bytesLength);
        return bytes;
    }
}