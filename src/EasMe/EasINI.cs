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
    private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString,
                                                         string lpFileName);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                      int size, string filePath);

    /// <summary>
    ///     Writes a value to the INI file
    /// </summary>
    /// <param name="Section"></param>
    /// <param name="Key"></param>
    /// <param name="Value"></param>
    public void Write(string Section, string Key, string Value) {
        WritePrivateProfileString(Section, Key, Value, _path);
    }

    /// <summary>
    ///     Reads a value from the INI file
    /// </summary>
    /// <param name="Section"></param>
    /// <param name="Key"></param>
    /// <returns></returns>
    public string? Read(string Section, string Key) {
        StringBuilder buffer = new(255);
        _ = GetPrivateProfileString(Section, Key, "", buffer, 255, _path);
        return Convert.ToString(buffer);
    }

    public static IniFile ReadFromPath(string path) {
        var str = File.ReadAllText(path);
        return ReadFromString(str);
    }

    public static IniFile ReadFromString(string data) {
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
            else if (line == Environment.NewLine || line == "\t" || line == "\r" || line == "\n" || line == "") {
            }
            else {
                throw new Exception("Ini file contains invalid line");
            }
        }

        return model;
    }

    public static void WriteToPath(string path, IniFile model) {
        var str = WriteToString(model);
        File.WriteAllText(path, str);
    }

    private static string WriteToString(IniFile model) {
        var sb = new StringBuilder();
        foreach (var section in model.Sections) {
            sb.AppendLine($"[{section.Name}]");
            foreach (var comment in section.Comments) sb.AppendLine($";{comment.Comment}");
            foreach (var data in section.Data) sb.AppendLine($"{data.Key}={data.Value}");
        }

        return sb.ToString();
    }
}

public class IniFile
{
    public List<IniSection> Sections { get; set; } = new();
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