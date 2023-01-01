
using EasMe;
using EasMe.Extensions;
using EasMe.Test;

EasLogFactory.LoadConfig(x =>
{
	x.SeperateLogLevelToFolder = true;
	x.ConsoleAppender = true;
	x.MinimumLogLevel = Severity.TRACE;
	x.TraceLogging = true;
	x.DontLog = false;
	x.ExceptionHideSensitiveInfo = false;
	x.LogFileName = "Test_";
	x.StackLogCount = 0;
	
});
var logger = EasLogFactory.CreateLogger("test");
logger.Warn("Test");
logger.Fatal("Test");
logger.Error("Test");
logger.Info("Test");
Console.WriteLine("test");