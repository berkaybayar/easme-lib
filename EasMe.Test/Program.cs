
using EasMe;
using EasMe.Extensions;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Diagnostics;


var stopwatch = new Stopwatch();
stopwatch.Start();

Console.WriteLine(EasProxy.GetNetworkInfo_Client().ToJsonString());
stopwatch.Stop();
Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

