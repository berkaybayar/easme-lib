using EasMe.Extensions;
using EasMe.Logging.Models;

namespace EasMe.Logging;

public class EasLogReader
{
  private readonly string _filePath;
  private string[] _logFileContent;

  public EasLogReader(string filePath) {
    _filePath = filePath;
    _logFileContent = Array.Empty<string>();
    Load();
  }

  private void Load() {
    if (!File.Exists(_filePath)) throw new Exception("Could not locate log file with given path: " + _filePath);
    try {
      var fileContent = File.ReadAllText(_filePath);
      var lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      _logFileContent = lines;
    }
    catch (Exception e) {
      throw new Exception("Failed reading log file with given path: " + _filePath, e);
    }
  }

  /// <summary>
  ///   Gets deserialized list of all logs.
  /// </summary>
  /// <returns
  /// </returns>
  /// <exception cref="EasException"></exception>
  public IEnumerable<LogModel> GetLogs() {
    try {
      var list = new List<LogModel>();
      foreach (var line in _logFileContent) {
        var deserialized = line.FromJsonString<LogModel>();
        if (deserialized == null) throw new Exception("Failed to deserialize");
        list.Add(deserialized);
      }

      if (list.Count == 0)
        throw new Exception(
                            "Failed getting log file content as List<BaseModel>, log file does not have logs recorded.");
      return list;
    }
    catch (Exception ex) {
      throw new Exception("Failed to deserialize log file content.", ex);
    }
  }
}