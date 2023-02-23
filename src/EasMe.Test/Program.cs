

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

var asd = 0;
EasMemoryCache.This.Set("test",asd);
var asd2 = EasMemoryCache.This.Get<int>("test");
Console.Read();
