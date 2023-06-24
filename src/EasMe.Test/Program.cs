using System.Text;
using EasMe;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

Console.WriteLine();

//var url = "http://64.52.81.79:8080/api/8/stream/server_zones";
//var req = EasAPI.SendGetRequest(url);
//var resp = req.Content.ReadAsStringAsync().GetAwaiter().GetResult();
////var jObject = JObject.Parse(resp);
////var valueList = jObject["value"]?.Children().ToList();
//var jObjectList = JsonConvert.DeserializeObject<Dictionary<string,dynamic>>(resp);
////var valueList = jObject?.Value as IEnumerable<dynamic>;
//var count = 0;
//foreach (var item in jObjectList) {
//    var processingElementValue = item.Value?.processing;
//    if (processingElementValue == null) continue;
//    var processingElement = Convert.ToInt32(processingElementValue);
//    count += processingElement;
//}
//Console.WriteLine(count);




//var path = @"C:\Users\kkass\OneDrive\Masaüstü\pazfilestruct.txt";
//var lines = File.ReadAllLines(path);
//var dic = new List<dynamic>();
//foreach (var line in lines) {
//    try {
//        var split = line.Split(">");
//        var pazName = split[0].Trim();
//        var fileSubPath = split[1].Trim();
//        var firstDicNameFromSubPath = fileSubPath.Split("/")[0];

//        dic.Add(new {
//            PAZ = pazName, 
//            PATH = fileSubPath,
//            FIRST_DIC = firstDicNameFromSubPath
//        });
//    }
//    catch (Exception e) {
//        Console.WriteLine(e.Message + "  DATA: " + line);
//    }
//}
//var sorted = dic.GroupBy(x => x.FIRST_DIC).ToList();

//var newpath = @"C:\Users\kkass\OneDrive\Masaüstü\new-pazfilestruct.txt";
//var sb = new StringBuilder();

//foreach (var item in sorted) {
//    var pazFileList = item.ToList();
//    var distinct = pazFileList.Select(x => x.PAZ).Distinct().ToList();

//    var pazFilesString = string.Join("\n\t\t\t", distinct);
//    sb.AppendLine($"######  {item.Key} > \n\t\t\t{pazFilesString}");
//}
//File.WriteAllText(newpath, sb.ToString());




//var lines = File.ReadAllLines("message.txt");
//var pazDicPath = Path.Combine(Directory.GetCurrentDirectory(), "paz");
//var moveDirPath = Path.Combine(Directory.GetCurrentDirectory(), "move");
//if(!Directory.Exists(moveDirPath)) Directory.CreateDirectory(moveDirPath);
//var pazFiles = Directory.GetFiles(pazDicPath, "*.paz", SearchOption.AllDirectories);
//foreach (var line in lines) {
//    var trim = line.Trim();
//    var pazFilePath = pazFiles.FirstOrDefault(x => x.Contains(trim));
//    if (pazFilePath == null) {
//        Console.WriteLine($"Paz file not found for {trim}");
//        continue;
//    }
//    var pazFileName = Path.GetFileName(pazFilePath);
//    var moveFilePath = Path.Combine(moveDirPath, pazFileName);
//    File.Copy(pazFilePath, moveFilePath);

//}




//var sb = new StringBuilder();
//foreach (var item in sorted) {
//    sb.AppendLine($"{item.PAZ} > {item.PATH}");
//}
//File.WriteAllText(newpath, sb.ToString());