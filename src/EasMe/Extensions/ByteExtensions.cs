using System.Text;

namespace EasMe.Extensions;

public static class ByteExtensions {
    /// <summary>
    ///     Converts hashed byte array to string
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string BuildString(this byte[] bytes) {
        bytes.ToString();
        StringBuilder builder = new();
        for (var i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
        return builder.ToString();
    }

    public static string ToBase64String(this byte[] bytes) {
        return Convert.ToBase64String(bytes);
    }

    public static string ConvertToString(this byte[] byteArray) {
        return Encoding.ASCII.GetString(byteArray);
    }

    public static string BytesToHexString(this byte[] bt) {
        try {
            var s = string.Empty;
            for (var i = 0; i < bt.Length; i++) {
                var b = bt[i];
                int n, n1, n2;
                n = b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + 'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + 'A')).ToString();
                else
                    s += n1.ToString();
                if (i + 1 != bt.Length && (i + 1) % 2 == 0) s += "-";
            }

            return s;
        }
        catch {
            return "";
        }
    }

    /// <summary>
    ///     Compares 2 byte arrays for equality.
    /// </summary>
    /// <param name="array1"></param>
    /// <param name="array2"></param>
    /// <returns>true if arrays are equal</returns>
    public static bool CompareTo(this byte[] array1, byte[] array2) {
        if (array1.Length != array2.Length) return false;

        for (var i = 0; i < array1.Length; i++)
            if (array1[i] != array2[i])
                return false;

        return true;
    }
}