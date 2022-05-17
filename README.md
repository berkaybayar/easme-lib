# Introduction
 Collection of classes that will save you time and help you avoid repetitive code in CSharp. 
 
## Getting Started
```c#
//Add reference to your project
//Add library reference in the class you want to use and define EasMe classes
using EasMe;

```

---
# EasQL
 EasQL helps you with basic SQL commands. Execute queries, get tables, shrink and backup database features.
 
### Setting up
```c#
EaSQL _easql = new EasQL();
```

### GetTable Usage
```c#
SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
DataTable dt = _easql.GetTable("YOUR-CONNECTION-STRING",cmd);

SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
DataTable dt = _easql.GetTable("YOUR-CONNECTION-STRING",cmd);
```

### ExecNonQuery, ExecScalar, ExecStoredProcedure Usage
```c#
//ExecNonQuery executes query and returnes affected rows as int value
SqlCommand cmd = new SqlCommand("UPDATE Users SET Email = @email WHERE Id = @id");
cmd.Parameters.AddWithValue("@email","example@mail.com");
cmd.Parameters.AddWithValue("@id",1);
int affectedRows = _easql.ExecNonQuery("YOUR-CONNECTION-STRING",cmd);

//ExecScalar will return an object, convert it depending on what value you are expecting from query
SqlCommand cmd = new SqlCommand("SELECT Email FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
object obj = _easql.ExecScalar("YOUR-CONNECTION-STRING",cmd);

//ExecStoredProcedure will return int value which will depends on the procedure
SqlCommand cmd = new SqlCommand("sendMail");
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.AddWithValue("@Username", "exampleusername");
int result = _sql.ExecStoredProcedure("YOUR-CONNECTION-STRING", cmd);
```

### BackupDatabase and ShrinkDatabase Usage
```c#
//BackupDatabase will create a backup of your database in the given path and will add date in file name
_easql.BackupDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME","BACKUP-PATH");

//ShrinkDatabase will shrink your database and logs, this will not delete your data only will reduce the disk space of SQL logs
_easql.ShrinkDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME");

//Database log default name is set DATABASENAME_log but you can change it in SQL management studio if its default you only need to "_log" string after database name
//If you don't give paramete dblogname it will set dblogname to default
_easql.ShrinkDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME","DATABASE-LOG-NAME");
```

### TruncateTable, DropTable, DropDatabase Usage
```c#
_easql.TruncateTable("YOUR-CONNECTION-STRING","TABLE-NAME");

_easql.DropTable("YOUR-CONNECTION-STRING","TABLE-NAME");

_easql.DropDatabase("YOUR-CONNECTION-STRING", "DATABASE-NAME");
```

### GetAllTableName Usage
```c#
List<string> TableList = _easql.GetAllTableName("YOUR-CONNECTION-STRING");
```

---
# EasBox
 EasBox helps you with message boxes. Show easy and simple Message Boxes with your WinForm project.

### Usage
```c#
if(_easbox.Confirm("Are you sure you want to do this ?"))
{
 //ACTION
}

_easbox.Show("Are you sure you want to do this ?");
_easbox.Warn("You should run this app on administrator mode");
_easbox.Info("Successfully updated list");
_easbox.Error("An interal error occured");
_easbox.Stop("This action is not allowed");
```

---
# EasINI
 EasINI helps you with reading and writing .ini files.
 
### Set ini file path
```c#
EasINI _easini = new EasINI();

//To specify ini file path
EasINI _easini = new EasINI("FILE-PATH");
```

### .ini file structure
```
[SETTINGS]
URL="www.github.com"
```

### Read Usage
```c#
string url = _easini.Read("SETTINGS","APIURL");
```

### Write Usage
```c#
_easini.Write("SETTINGS","APIURL","www.google.com");
```

---
# EasLog
 EasLog helps you with creating logs with your application with interval options.
  
### Set log file path
```c#
EasLog _easlog = new EasLog();

//To specify logging file path
//If you don't specify path it will take current directory and create Logs folder
EasLog _easlog = new EasLog("FILE-PATH");
```

### Create Log Usage
```c#
//Default interval is daily
_easlog.Create("LOG-CONTENT");

//In order so set interval give another parameter
//0 (Default) => Daily
//1 => Hourly
//2 => Minutely
_easlog.Create("LOG-CONTENT",1);
```

---
# EasDel
 EasDel helps you with deleting files, directories and sub directories with logging feature.
 
### Enabling logging and setting file path
```c#
EasDel _easdel = new EasDel();

//Logging is disabled by default so enable it like this
EasDel _easdel = new EasDel(true);

//If you specify log file path, logging automaticly will be enabled
EasDel _easdel = new EasDel("FILE-PATH");
```

### Usage
```c#
//You can give file path as one one file or a folder it will work either way
_easdel.DeleteAllFiles("FILE-PATH");
```

---
# EasAPI
 EasAPI helps you with send get and post requests to APIs.

### APIResponse Class
```c#
//Status is true when response is 200 (OK)
//Content is Json string
public class APIResponse 
{
    public bool Status { get; set; }
    public string Content { get; set; }
}
```
### Get Usage
```c#
//Request without authentication header
APIResponse Response = _easapi.Get("API-URL");

//Request with authentication header
APIResponse Response = _easapi.Get("API-URL","AUTHENTICATION-HEADER-TOKEN");
```

### PostAsJson Usage
```c#
//Request without authentication header
//obj can be anonymous abject or a class
var obj = new { message = "EasMe makes this so much easier!"};
APIResponse Response = _easapi.PostAsJson("API-URL",obj);

//Request with authentication header
APIResponse Response = _easapi.PostAsJson("API-URL",obj,"AUTHENTICATION-HEADER-TOKEN");
```

### ParsefromAPIResponse Usage
```c#
//This will parse JObject string and return the value from API response 
//If can't find value it will return empty string
string ParsedResponse = _easapi.ParsefromAPIResponse(Response.Content,"message");


//If you want function to throw error if can't find the value, give another parameter as true
string ParsedResponse = _easapi.ParsefromAPIResponse(Response.Content,"message",true);
```

---
# EasMail
 EasMail helps you with SMTP, it makes sending mails a lot faster.
 
### Setting up
```c#
EasMail _easmail = new EasMail(string Host, string MailAddress, string Password, int Port, bool isSSL = false);
```

### MailSend Usage
```c#
//Definition
EasMail(string Host, string MailAddress, string Password, int Port, bool isSSL = false){};

_easmail.MailSend("YOUR-BODY-CONTENT","TO-EMAIL-ADDRESS","SUBJECT");
```

---
# EasReCaptcha
 EasReCaptcha helps you with validating Google ReCaptcha with your web app.

### CaptchaResponse Class
```c#
public class CaptchaResponse
{
    public bool Success { get; set; }
    public DateTime ChallengeTS { get; set; }
    public string ApkPackageName { get; set; }
    public string ErrorCodes { get; set; }
}
```

### Requirements and Usage
```c#
//-Requirements
//Newtonsoft.Json
//System.Net

//-appsettings.json
//  "ReCaptcha": {
//  "SiteKey": "YOUR-SITE-KEY",
//  "SecretKey": "YOUR-SECRET-KEY",
//  "Version": "v2"
//}

//-Program.cs
builder.Services.AddReCaptcha(builder.Configuration);
```

```html
//-View
<div class="g-recaptcha" data-sitekey="YOUR-SITE-KEY"></div>		
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
```

```c#
-Controller
var CaptchaResponse = HttpContext.Request.Form["g-recaptcha-response"];
string Secret = "your-secret-key";       
var Captcha = _easrecaptcha.Validate(Secret, CaptchaResponse);
```

