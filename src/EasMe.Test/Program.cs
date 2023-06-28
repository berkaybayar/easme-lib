using System.Net;
using EasMe;

Console.WriteLine("Hello World!");
var ipAddd = GetHostIpAddress();


Console.ReadLine();
static string GetHostIpAddress() {
  var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
  var ipAddress = ipHostInfo.AddressList.Select(x => x.ToString())
                            .Where(x => !x.StartsWith("192.168.") && !x.StartsWith("::"))
                            .ToList();
  return ipAddress.FirstOrDefault();
}
