using EasMe.Extensions;
using EasMe.Logging.Internal;
using EasMe.Logging.Models;
using EasMe.Result;

namespace EasMe.Logging;

/// <summary>
///     Simple static json and console logger with useful options.
/// </summary>
public class EasLog : IEasLog
{
    private static readonly object _fileLock = new();

    private static readonly EasTask EasTask = new();
    private static readonly List<Exception> _exceptions = new();
    private readonly string _folderName;

    internal string _LogSource;

    internal EasLog(string source, string folderName)
    {
        if (folderName.IsNullOrEmpty()) folderName = EasLogFactory.Config.LogFolderPath;
        _LogSource = source;
        _folderName = folderName;
    }

    internal EasLog(string source)
    {
        _LogSource = source;
        _folderName = EasLogFactory.Config?.LogFolderPath ?? ".\\Logs";
    }

    public static IReadOnlyCollection<Exception> Exceptions => _exceptions;


    public void LogResult(Result.Result result)
    {
        var EasLogLevel = result.Severity.ToEasLogLevel();
        if (!EasLogLevel.IsLoggable()) return;
        if (result.Errors.Count > 0)
        {
            WriteLog(_LogSource, EasLogLevel, null, $"ErrorCode:{result.ErrorCode}",
                $"Errors:{string.Join(",", result.Errors)}");
            return;
        }

        WriteLog(_LogSource, EasLogLevel, null, $"ErrorCode:{result.ErrorCode}");
    }

    public void LogResult<T>(ResultData<T> result)
    {
        var EasLogLevel = result.Severity.ToEasLogLevel();
        if (!EasLogLevel.IsLoggable()) return;
        if (result.Errors.Count > 0)
        {
            WriteLog(_LogSource, EasLogLevel, null, $"ErrorCode:{result.ErrorCode}",
                $"Errors:{string.Join(",", result.Errors)}");
            return;
        }

        WriteLog(_LogSource, EasLogLevel, null, $"ErrorCode:{result.ErrorCode}", $"Data:{result.Data}");
    }

    public void LogResult(Result.Result result, object message)
    {
        var EasLogLevel = result.Severity.ToEasLogLevel();
        if (!EasLogLevel.IsLoggable()) return;
        if (result.Errors?.Count > 0)
        {
            WriteLog(_LogSource, EasLogLevel, null, $"Message:{message}", $"ErrorCode:{result.ErrorCode}",
                $"Errors:{string.Join(",", result.Errors)}");
            return;
        }

        WriteLog(_LogSource, EasLogLevel, null, $"Message:{message}", $"ErrorCode:{result.ErrorCode}");
    }

    public void LogResult<T>(ResultData<T> result, object message)
    {
        var EasLogLevel = result.Severity.ToEasLogLevel();
        if (!EasLogLevel.IsLoggable()) return;
        if (result.Errors.Count > 0)
        {
            WriteLog(_LogSource, EasLogLevel, null, $"Message:{message}", $"ErrorCode:{result.ErrorCode}",
                $"Errors:{string.Join(",", result.Errors)}", $"Data:{result.Data}");
            return;
        }

        WriteLog(_LogSource, EasLogLevel, null, $"Message:{message}", $"ErrorCode:{result.ErrorCode}",
            $"Data:{result.Data}");
    }


    public void Info(params object[] param)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, param);
    }

    public void Info(object obj1)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1);
    }

    public void Info(object obj1, object obj2)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2);
    }

    public void Info(object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2, obj3);
    }

    public void Info(object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2, obj3, obj4);
    }

    public void Info(object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2, obj3, obj4, obj5);
    }

    public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        object obj8)
    {
        if (!EasLogLevel.Information.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Information, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }

    public void Error(params object[] param)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, param);
    }

    public void Error(object obj1)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1);
    }

    public void Error(object obj1, object obj2)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2);
    }

    public void Error(object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2, obj3);
    }

    public void Error(object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2, obj3, obj4);
    }

    public void Error(object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2, obj3, obj4, obj5);
    }

    public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        object obj8)
    {
        if (!EasLogLevel.Error.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Error, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }

    public void Warn(params object[] param)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, param);
    }

    public void Warn(object obj1)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1);
    }

    public void Warn(object obj1, object obj2)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2);
    }

    public void Warn(object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2, obj3);
    }

    public void Warn(object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2, obj3, obj4);
    }

    public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5);
    }

    public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        object obj8)
    {
        if (!EasLogLevel.Warning.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }


    public void Exception(Exception ex)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex);
    }

    public void Exception(Exception ex, params object[] param)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, param);
    }

    public void Exception(Exception ex, object obj1)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1);
    }

    public void Exception(Exception ex, object obj1, object obj2)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2);
    }

    public void Exception(Exception ex, object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3);
    }

    public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4);
    }

    public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5);
    }

    public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        object obj7)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        object obj7, object obj8)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }


    public void Fatal(params object[] param)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, param);
    }

    public void Fatal(object obj1)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1);
    }

    public void Fatal(object obj1, object obj2)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2);
    }

    public void Fatal(object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2, obj3);
    }

    public void Fatal(object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2, obj3, obj4);
    }

    public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2, obj3, obj4, obj5);
    }

    public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        object obj8)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }


    public void Fatal(Exception ex, params object[] param)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, param);
    }

    public void Fatal(Exception ex, object obj1)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1);
    }

    public void Fatal(Exception ex, object obj1, object obj2)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2);
    }

    public void Fatal(Exception ex, object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3);
    }

    public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4);
    }

    public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5);
    }

    public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        object obj7)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        object obj7, object obj8)
    {
        if (!EasLogLevel.Fatal.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Fatal, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }


    public void Debug(params object[] param)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, param);
    }

    public void Debug(object obj1)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1);
    }

    public void Debug(object obj1, object obj2)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2);
    }

    public void Debug(object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2, obj3);
    }

    public void Debug(object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2, obj3, obj4);
    }

    public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5);
    }

    public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        object obj8)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }

    public void Debug(Exception ex, params object[] param)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, param);
    }

    public void Debug(Exception ex, object obj1)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1);
    }

    public void Debug(Exception ex, object obj1, object obj2)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2);
    }

    public void Debug(Exception ex, object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2, obj3);
    }

    public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2, obj3, obj4);
    }

    public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5);
    }

    public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        object obj7)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6,
        object obj7, object obj8)
    {
        if (!EasLogLevel.Debug.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }


    public void Trace(params object[] param)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, param);
    }

    public void Trace(object obj1)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1);
    }

    public void Trace(object obj1, object obj2)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2);
    }

    public void Trace(object obj1, object obj2, object obj3)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2, obj3);
    }

    public void Trace(object obj1, object obj2, object obj3, object obj4)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2, obj3, obj4);
    }

    public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5);
    }

    public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5, obj6);
    }

    public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
    }

    public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7,
        object obj8)
    {
        if (!EasLogLevel.Trace.IsLoggable()) return;
        WriteLog(_LogSource, EasLogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
    }


    public bool IsLogLevelEnabled(EasLogLevel easLogLevel)
    {
        return easLogLevel.IsLoggable();
    }

    private string GetExactLogPath(EasLogLevel severity)
    {
        var date = DateTime.Now.ToString(EasLogFactory.Config.DateFormatString);
        var path = EasLogFactory.Config.SeparateLogLevelToFolder
            ? Path.Combine(_folderName, date,
                severity + "_" + EasLogFactory.Config.LogFileName + date + EasLogFactory.Config.LogFileExtension)
            : Path.Combine(_folderName,
                EasLogFactory.Config.LogFileName + date + EasLogFactory.Config.LogFileExtension);
        return path;
    }

    public static void Flush()
    {
        EasTask.Flush();
    }

    private void WriteLog(string source, EasLogLevel severity, Exception? exception = null, params object[] param)
    {
        WebInfo? webInfo = null;
        if (EasLogFactory.Config.WebInfoLogging) webInfo = new WebInfo();
        var loggingAction = new Action(() =>
        {
            var paramToLog = param.ToLogString();
            var log = LogModel.Create(severity, source, paramToLog, exception, webInfo);
            if (EasLogFactory.Config.ConsoleAppender) EasLogConsole.Log(log.Level, log.ToJsonString() ?? "");
            var logFilePath = GetExactLogPath(log.Level);
            try
            {
                var folderPath = Path.GetDirectoryName(logFilePath);
                if (folderPath is not null)
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                lock (_fileLock)
                {
                    File.AppendAllText(logFilePath, log.ToJsonString() + "\n");
                }
            }
            catch (Exception ex)
            {
                lock (_exceptions)
                {
                    _exceptions.Add(ex);
                }
            }
        });
        EasTask.AddToQueue(loggingAction);
    }
}