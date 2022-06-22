namespace EasMe
{
    /// <summary>
    /// Error helper for EasLog also contains enum ErrorList
    /// </summary>
    public static class EasLogHelper
    {

        public static string? EnumGetKeybyValue(int value)
        {
            return Enum.GetName(typeof(Error), value);
        }
        /// <summary>
        /// Convert an error code to a readable string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? ConvertEnumStringToReadable(string value)
        {
            var result = value.Replace("_", " ").ToLower();
            var firstChar = char.ToUpper(result[0]);
            return firstChar + result.Substring(1) + ".";
        }
        /// <summary>
        /// Convert an error code to a readable string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? ConvertEnumStringToReadable(Error value)
        {
            var toStr = value.ToString();
            return ConvertEnumStringToReadable(toStr);
        }

    }

}
