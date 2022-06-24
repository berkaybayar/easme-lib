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


    }

}
