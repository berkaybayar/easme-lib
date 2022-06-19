using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace EasMe
{
    /// <summary>
    /// Write or read from INI file
    /// </summary>
    public class EasINI
    {

        private string Path;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public EasINI(string INIFilePath)
        {
            Path = INIFilePath;
        }

        public EasINI()
        {
            Path = Directory.GetCurrentDirectory() + @"\service.ini";
        }

        /// <summary>
        /// Writes a value to the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }
        /// <summary>
        /// Reads a value from the INI file
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string Read(string Section, string Key)
        {
            StringBuilder buffer = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", buffer, 255, this.Path);
            return Convert.ToString(buffer);
        }


    }

}
