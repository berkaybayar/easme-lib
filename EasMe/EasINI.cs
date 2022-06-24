using System.Runtime.InteropServices;
using System.Text;

namespace EasMe
{
    /// <summary>
    /// Write or read from INI file
    /// </summary>
    public static class EasINI
    {

        private static string? Path;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void LoadFile(string INIFilePath)
        {
            Path = INIFilePath;
        }
        public static void LoadDefaultFile()
        {
            Path = Directory.GetCurrentDirectory() + @"\service.ini";            
        }

        private static void CheckLoaded()
        {
            if (string.IsNullOrEmpty(Path)) throw new EasException(Error.NOT_LOADED, "INI file path not loaded, CAll LoadFile() or LoadDefaultFile() in your application startup.");
        }
        /// <summary>
        /// Writes a value to the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void Write(string Section, string Key, string Value)
        {
            CheckLoaded();
            WritePrivateProfileString(Section, Key, Value, Path);
        }
        /// <summary>
        /// Reads a value from the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string? Read(string Section, string Key)
        {
            CheckLoaded();
            StringBuilder buffer = new(255);
            _ = GetPrivateProfileString(Section, Key, "", buffer, 255, Path);
            return Convert.ToString(buffer);
        }


    }

}
