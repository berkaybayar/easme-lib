using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    public class EasINI
    {

        public string Path;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /*
        When calling the class need to give .ini file path
        var _ini = new EasINI("C:\Users\admin\Desktop\service.ini")
        default it will look for service.ini in the same directory as the program
         */
        public EasINI(string INIFilePath)
        {
            Path = INIFilePath;
        }
        public EasINI()
        {
            Path = Directory.GetCurrentDirectory() + @"\service.ini";
        }
        /*
         WRITE USAGE        
         EasINI ini = new EasINI(INI);
         WriteINI("SETTINGS", "URL", "www.google.com", ".\service.ini");
         */
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);
        }

        /* 
         READ USAGE
         EasINI ini = new EasINI(INI);
        
         var a = ini.ReadINI("SETTINGS", "URL");
         ini.ReadINI("SETTINGS", "URL");          
        */
        public string Read(string Section, string Key)
        {
            StringBuilder buffer = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", buffer, 255, this.Path);
            return Convert.ToString(buffer);
        }


    }

}
