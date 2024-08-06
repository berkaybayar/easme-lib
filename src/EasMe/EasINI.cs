using System.Runtime.InteropServices;
using System.Text;

namespace EasMe;

public class EasINI
{
  private readonly string _path;

  public EasINI(string? iniFilePath = null) {
    iniFilePath ??= Directory.GetCurrentDirectory() + @"\service.ini";
    if (!File.Exists(iniFilePath))
      throw new ArgumentNullException("iniFilePath");
    _path = iniFilePath;
  }

  [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool WritePrivateProfileString(string lpAppName,
                                                       string lpKeyName,
                                                       string lpString,
                                                       string lpFileName);

  [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
  private static extern int GetPrivateProfileString(string section,
                                                    string key,
                                                    string def,
                                                    StringBuilder retVal,
                                                    int size,
                                                    string filePath);

  /// <summary>
  ///   Writes a value to the INI file
  /// </summary>
  /// <param name="section"></param>
  /// <param name="key"></param>
  /// <param name="value"></param>
  public void Write(string section, string key, string value) {
    WritePrivateProfileString(section, key, value, _path);
  }

  /// <summary>
  ///   Reads a value from the INI file
  /// </summary>
  /// <param name="section"></param>
  /// <param name="key"></param>
  /// <returns></returns>
  public string? Read(string section, string key) {
    StringBuilder buffer = new(255);
    _ = GetPrivateProfileString(section,
                                key,
                                "",
                                buffer,
                                255,
                                _path);
    return Convert.ToString(buffer);
  }

  public static IniFile ParseByPath(string path) {
    var str = File.ReadAllText(path);
    return ParseByString(str);
  }

  private static IniFile ParseByString(string data) {
    var model = new IniFile();
    var split = data.Split(Environment.NewLine);
    for (var i = 0; i < split.Length; i++) {
      var line = split[i];
      if (line.StartsWith("[") && line.EndsWith("]")) {
        IniSection section = new();
        section.Name = line[1..^1];
        model.Sections?.Add(section);
      }
      else if (line.Contains('=')) {
        var key = new IniData();
        var splitKeyValue = line.Split('=');
        key.Key = splitKeyValue[0];
        key.Value = splitKeyValue[1];
        model.Sections?.Last().Data?.Add(key);
      }
      else if (line.StartsWith(';')) {
        IniComment comment = new();
        comment.Comment = line.Replace(";", "").Trim();
        comment.LineNo = i;
        model.Sections?.Last().Comments?.Add(comment);
      }
      else if (line == Environment.NewLine || line == "\t" || line == "\r" || line == "\n" || line == "") { }
      else {
        throw new Exception("Ini file contains invalid line");
      }
    }

    return model;
  }
}

public class IniFile
{
  public List<IniSection> Sections { get; set; } = new();

  public string WriteToString() {
    var sb = new StringBuilder();
    foreach (var section in Sections) {
      sb.AppendLine($"[{section.Name}]");
      foreach (var comment in section.Comments) sb.AppendLine($";{comment.Comment}");
      foreach (var data in section.Data) sb.AppendLine($"{data.Key}={data.Value}");
    }

    return sb.ToString();
  }

  public void WriteToPath(string path) {
    var str = WriteToString();
    File.WriteAllText(path, str);
  }
}

public class IniSection
{
  public string? Name { get; set; }
  public List<IniData> Data { get; set; } = new();
  public List<IniComment> Comments { get; set; } = new();
}

public class IniData
{
  public string? Key { get; set; }
  public string? Value { get; set; }
}

public class IniComment
{
  public int LineNo { get; set; } = -1;
  public string? Comment { get; set; }
}