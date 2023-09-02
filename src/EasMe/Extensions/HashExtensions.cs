using System.Data.HashFunction.xxHash;
using System.Security.Cryptography;
using System.Text;

namespace EasMe.Extensions;

public static class HashExtensions
{
  private static byte[] ComputeHash(HashAlgorithm algorithm, string rawData) {
    return algorithm.ComputeHash(Encoding.UTF8.GetBytes(rawData));
  }

  public static byte[] ComputeSaltedHash(HashAlgorithm algorithm, string rawData, string saltStr) {
    var plainText = Encoding.UTF8.GetBytes(rawData);
    var salt = Encoding.UTF8.GetBytes(saltStr);
    var plainTextWithSaltBytes =
      new byte[plainText.Length + salt.Length];

    for (var i = 0; i < plainText.Length; i++) plainTextWithSaltBytes[i] = plainText[i];
    for (var i = 0; i < salt.Length; i++) plainTextWithSaltBytes[plainText.Length + i] = salt[i];

    return algorithm.ComputeHash(plainTextWithSaltBytes);
  }

  public static byte[] MD5Hash(this string rawData) {
    return ComputeHash(MD5.Create(), rawData);
  }

  public static byte[] MD5HashSalted(this string rawData, string salt) {
    return ComputeSaltedHash(MD5.Create(), rawData, salt);
  }


  public static byte[] SHA256Hash(this string rawData) {
    return ComputeHash(SHA256.Create(), rawData);
  }

  public static byte[] SHA256HashSalted(this string rawData, string salt) {
    return ComputeSaltedHash(SHA256.Create(), rawData, salt);
  }


  public static byte[] SHA512Hash(this string rawData) {
    return ComputeHash(SHA512.Create(), rawData);
  }

  public static byte[] SHA512HashSalted(this string rawData, string salt) {
    return ComputeSaltedHash(SHA512.Create(), rawData, salt);
  }


  public static byte[] FileMD5Hash(this string path) {
    using var md5 = MD5.Create();
    using var stream = File.OpenRead(path);
    return md5.ComputeHash(stream);
  }

  public static byte[] FileXXHash(this string path) {
    var factory = xxHashFactory.Instance.Create();
    using var stream = File.OpenRead(path);
    return factory.ComputeHash(stream).Hash;
  }

  public static byte[] XXHash(this string input) {
    var factory = xxHashFactory.Instance.Create();
    var hashed = factory.ComputeHash(Encoding.UTF8.GetBytes(input));
    return hashed.Hash;
  }
}