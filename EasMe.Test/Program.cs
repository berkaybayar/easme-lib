
using EasMe;
using EasMe.Extensions;
using System.Net.Sockets;
using System.Net;
using System.Text;
var mac = "D8BBC11DE939";

Console.WriteLine(FixMac(mac));

 static string FixMac(string mac)
{
    var sb = new StringBuilder();
    var count = 0;
    foreach (var item in mac)
    {
        if (count == 2)
        {
            sb.Append('-');
            count = 0;
        }
        sb.Append(item);
        count++;
    }
    return sb.ToString();
}