namespace EasMe.Extensions;

public static class ByteExtensions
{
    public static string ToBase64String(this byte[] bytes) {
        return Convert.ToBase64String(bytes);
    }

    public static string ToHexString(this byte[] bytes) {
        return Convert.ToHexString(bytes);
    }

    /// <summary>
    ///     Compares 2 byte arrays for equality.
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
}