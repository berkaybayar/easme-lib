using EasMe.Exceptions;
using System.Runtime.InteropServices;
using System.Text;

namespace EasMe
{
    /// <summary>
    /// Write or read from INI file
    /// </summary>
    public class EasINI
    {

        private static string? Path { get; set; }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// Loads Service.ini file by given file path. If given null it will get service.ini file in current directory.
        /// </summary>
        /// <param name="INIFilePath"></param>
        /// <exception cref="NotExistException"></exception>
        public EasINI(string? iniFilePath = null)
        {
            if(iniFilePath == null) iniFilePath = Directory.GetCurrentDirectory() + @"\service.ini";
            if (!File.Exists(iniFilePath)) throw new NotExistException("Given INI file path does not exist: " + iniFilePath);
            Path = iniFilePath;
        }

        /// <summary>
        /// Writes a value to the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void Write(string Section, string Key, string Value)
        {
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
            StringBuilder buffer = new(255);
            GetPrivateProfileString(Section, Key, "", buffer, 255, Path);
            return Convert.ToString(buffer);

        }


    }

}
