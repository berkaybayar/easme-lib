namespace EasMe;

public static class ByteExtensions
{
  public static string ToBase64String(this byte[] bytes) {
    return Convert.ToBase64String(bytes);
  }

  public static string ToHexString(this byte[] bytes) {
    return Convert.ToHexString(bytes);
  }

  /// <summary>
  ///   Compares 2 byte arrays for equality.
  /// </summary>
  /// <param name="array1"></param>
  /// <param name="array2"></param>
  /// <returns>true if arrays are equal</returns>
  public static bool Compare(this byte[] array1, byte[] array2) {
    if (array1.Length != array2.Length) return false;

    for (var i = 0; i < array1.Length; i++)
      if (array1[i] != array2[i])
        return false;

    return true;
  }

  public static bool Compare(this byte[] array1, byte[] array2, int bitsToCompare) {
    if (array1.Length != array2.Length) return false;
    var bytesToCompare = bitsToCompare / 8;
    var bitsToCompareMod = bitsToCompare % 8;
    for (var i = 0; i < bytesToCompare; i++)
      if (array1[i] != array2[i])
        return false;
    if (bitsToCompareMod == 0) return true;
    var mask = (byte)(0xFF << (8 - bitsToCompareMod));
    return (array1[bytesToCompare] & mask) == (array2[bytesToCompare] & mask);
  }

  public static bool Compare(this byte[] array1, byte[] array2, ByteCompareLevel compareLevel) {
    if (array1.Length != array2.Length) return false;

    return compareLevel switch {
      ByteCompareLevel.ByteByByte => array1.Compare(array2),
      ByteCompareLevel.EightBits => array1.Compare(array2, 8),
      ByteCompareLevel.SixteenBits => array1.Compare(array2, 16),
      ByteCompareLevel.ThirtyTwoBits => array1.Compare(array2, 32),
      ByteCompareLevel.SixtyFourBits => array1.Compare(array2, 64),
      _ => throw new ArgumentOutOfRangeException(nameof(compareLevel), compareLevel, null)
    };
  }

  public static int CalculateHammingDistance(byte[] byteArray1, byte[] byteArray2) {
    if (byteArray1 == null || byteArray2 == null || byteArray1.Length != byteArray2.Length) throw new ArgumentException("Both byte arrays must be of the same length and not null.");

    var hammingDistance = 0;

    for (var i = 0; i < byteArray1.Length; i++) {
      var xorResult = (byte)(byteArray1[i] ^ byteArray2[i]);

      // Count the set bits (1s) in the XOR result
      while (xorResult > 0) {
        hammingDistance += xorResult & 1;
        xorResult >>= 1;
      }
    }

    return hammingDistance;
  }
}