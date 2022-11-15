using EasMe.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Test
{
    public static class Tester
    {
        public static void Start()
        {
            var res = EasINI.ReadFromPath(@"C:\Users\berka\Desktop\service.ini");
            EasINI.WriteToPath(@"C:\Users\berka\Desktop\service2.ini", res);
        }
    }
}
