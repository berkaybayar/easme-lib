

using System.Diagnostics;
using Ardalis.Result;
using EasMe;
using EasMe.Extensions;
using EasMe.Logging;
using EasMe.PostSharp.CacheAspects;
using EasMe.PostSharp.ExceptionAspects;
using EasMe.PostSharp.PerformanceAspects;
using EasMe.Test;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

Console.WriteLine(desktopPath);
Console.Read();
