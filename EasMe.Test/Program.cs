// See https://aka.ms/new-console-template for more information

using EasMe.Extensions;
using EasMe.Models;

Console.WriteLine("Hello, World!");

var res = Result.Warn(1,"test");
Console.WriteLine(res.ToJsonString());
var ser = System.Text.Json.JsonSerializer.Serialize(res);
Console.WriteLine(ser);