# Introduction
 Collection of classes that will save you time and help you avoid repetitive code in CSharp. 

## Setting Up
```
Install-Package EasMe.Core -Version 1.0.0
```

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
//Timeout set to 0 by default meaning there is no timeout
public DataTable GetTable(string Connection, SqlCommand cmd, int Timeout = 0){};


//Get Table without parameter
SqlCommand cmd = new SqlCommand("SELECT * FROM Users");
DataTable dt = _easql.GetTable("YOUR-CONNECTION-STRING",cmd);

//Get Table with parameter
SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
DataTable dt = _easql.GetTable("YOUR-CONNECTION-STRING",cmd);
```

### ExecNonQuery, ExecScalar Usage
```c#
//Timeout set to 0 by default meaning there is no timeout
public int ExecNonQuery(string Connection, SqlCommand cmd, int Timeout = 0){};
public object ExecScalar(string Connection, SqlCommand cmd, int Timeout = 0){};

//ExecNonQuery executes query and returnes affected rows as int value
SqlCommand cmd = new SqlCommand("UPDATE Users SET Email = @email WHERE Id = @id");
cmd.Parameters.AddWithValue("@email","example@mail.com");
cmd.Parameters.AddWithValue("@id",1);
int affectedRows = _easql.ExecNonQuery("YOUR-CONNECTION-STRING",cmd);

//ExecScalar will return an object, convert it depending on what value you are expecting from query
SqlCommand cmd = new SqlCommand("SELECT Email FROM Users WHERE Id = @id");
cmd.Parameters.AddWithValue("@id",1);
object obj = _easql.ExecScalar("YOUR-CONNECTION-STRING",cmd);
```

### BackupDatabase and ShrinkDatabase Usage
```c#
//Timeout set to 0 by default meaning there is no timeout
public void BackupDatabase(string Connection, string DatabaseName, string BackupPath, int Timeout = 0){};
public void ShrinkDatabase(string Connection, string DatabaseName, string DatabaseLogName = "_log"){};

//BackupDatabase will create a backup of your database in the given path and will add date in file name
//Backup Path must be a folder also if database is big you might want to consider running this on another thread
_easql.BackupDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME","BACKUP-PATH");

//ShrinkDatabase will shrink your database and logs, this will not delete your data only will reduce the disk space of SQL logs
_easql.ShrinkDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME");

//Database log default name is set DATABASENAME_log but you can change it in SQL management studio if its default you only need to "_log" string after database name
//If you don't give paramete dblogname it will set dblogname to default
_easql.ShrinkDatabase("YOUR-CONNECTION-STRING","DATABASE-NAME","DATABASE-LOG-NAME");
```

### TruncateTable, DropTable, DropDatabase Usage
```c#
public void TruncateTable(string Connection, string TableName){};
public void DropTable(string Connection, string TableName){};
public void DropDatabase(string Connection, string DatabaseName){};

_easql.TruncateTable("YOUR-CONNECTION-STRING","TABLE-NAME");
_easql.DropTable("YOUR-CONNECTION-STRING","TABLE-NAME");
_easql.DropDatabase("YOUR-CONNECTION-STRING", "DATABASE-NAME");
```

### GetAllTableName Usage
```c#
public List<string> GetAllTableName(string Connection){};

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
public string Read(string Section, string Key){};

string url = _easini.Read("SETTINGS","URL");
```

### Write Usage
```c#
public void Write(string Section, string Key, string Value){};

_easini.Write("SETTINGS","URL","www.google.com");
```

---
# EasLog
EasLog helps you with creating logs with your application with interval options.
  
### Set log file path
```c#
public EasLog(int Interval = 0){};
public EasLog(string FilePath, int Interval = 0){};

//If you don't specify path it will take current directory and create Logs folder
EasLog _easlog = new EasLog();

//To specify logging file path
EasLog _easlog = new EasLog("FILE-PATH");

//Intervals
//0 (Default) => Daily
//1 => Hourly
//2 => Minutely

//Interval with file path
EasLog _easlog = new EasLog("FILE-PATH",2);

//Interval without file path, logs will be created in current directory
EasLog _easlog = new EasLog(2);
```

### Create Log Usage
```c#
public void Create(string LogContent){};

_easlog.Create("LOG-CONTENT");
```

---
# EasDel
EasDel helps you with deleting files, directories and sub directories with logging feature.
 
### Enabling logging and setting file path
```c#
public EasDel(string LogPath){};
public EasDel(bool isEnableLogging = false){};

EasDel _easdel = new EasDel();

//Logging is disabled by default so enable it like this, will create logs in current directory
EasDel _easdel = new EasDel(true);

//If you specify log file path, logging automaticly will be enabled
EasDel _easdel = new EasDel("FILE-PATH");
```

### Usage
```c#
public void DeleteAllFiles(string FilePath){};

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
public APIResponse Get(string URL, string TOKEN = null){};

//Request without authentication header
APIResponse Response = _easapi.Get("API-URL");

//Request with authentication header
APIResponse Response = _easapi.Get("API-URL","AUTHENTICATION-HEADER-TOKEN");
```

### PostAsJson Usage
```c#
public APIResponse PostAsJson(string URL, object Data, string TOKEN = null){};

//Request without authentication header
//obj can be anonymous abject or a class
var obj = new { message = "EasMe makes this so much easier!"};
APIResponse Response = _easapi.PostAsJson("API-URL",obj);

//Request with authentication header
APIResponse Response = _easapi.PostAsJson("API-URL",obj,"AUTHENTICATION-HEADER-TOKEN");
```

### ParsefromAPIResponse Usage
```c#
public string ParsefromAPIResponse(string Response, string Parse,bool isThrow = false){};

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
public void SendMail(string Body, string SendTo, string Subject){};

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

### Requirements
```
Newtonsoft.Json
System.Net
```

### Program.cs
```c#
builder.Services.AddReCaptcha(builder.Configuration);
```

### appsettings.json
```json
"ReCaptcha": {
  "SiteKey": "YOUR-SITE-KEY",
  "SecretKey": "YOUR-SECRET-KEY",
  "Version": "v2"
}
```

### View
```html
<div class="g-recaptcha" data-sitekey="YOUR-SITE-KEY"></div>		
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
```

### Validating ReCaptcha Response from View
```c#
var CaptchaResponse = HttpContext.Request.Form["g-recaptcha-response"];
string Secret = "your-secret-key";       
var Captcha = _easrecaptcha.Validate(Secret, CaptchaResponse);

if (!Captcha.Success)
{
    ModelState.AddModelError("", "Captcha is not valid");
    return View();
}
```

---
## EasValidate
EasValidate helps you with validating strings. Email, IP address etc.

### Define Class 
```c#
EasValidate _easvalidate = new EasValidate();
```

### IsValid Usage
```c#
//Definitions
public bool IsValidEmail(string email){};
public bool IsValidIPAddress(string ipAddress){};
public bool IsValidMACAddress(string macAddress){};
public bool IsValidPort(string port){};

//Usage
if(_easvalidate.IsValidEmail("example@mail.com"))
{
  //DO SOMETHING
}
```
 
### HasSpecialChars and IsStrongPassword Usage
```c#
//Definitions
//Letters and digits is allowed by default
public bool HasSpecialChars(string yourString, string allowedChars){};
public bool IsStrongPassword(string password, string allowedChars, int minLength, int maxLength, bool allowSpace = false){};


//Usage
if(_easvalidate.HasSpecialChars("exampleUsername","_"))
{  
  //STRING HAS SPECIAL CHARACTERS THAT IS NOT ALLOWED
}

if(_easvalidate.IsStrongPassword("exampleUsername","+-*/^%&()",6,32))
{  
  //PASSWORD IS STRONG
}
```
 
---
## EasJWT
EasJWT helps you with generating and validating JWT Tokens.

### Define Class 
```c#
//Definition
public EasJWT(string secret, string issuer = "", string audience = "")

//Defining
EasJWT _easJWT = new EasJWT("YOUR-SECRET-KEY");
```

### Generating Token
```c#
//Definition
public string GenerateJWTToken(ClaimsIdentity claimsIdentity, int expireMinutes){};

//Usage
Claim[] claims = new Claim[] 
{
    new Claim(ClaimTypes.Name, "John"),
    new Claim(ClaimTypes.Role, "user")
};
string token = GenerateJWTToken(new ClaimsIdentity(claims), 10);
```

### Validating Token
```c#
//Definition
public ClaimsPrincipal ValidateJWTToken(string token,bool validateIssuer = false, bool validateAudience = false){};

//Usage
var claimsPrincipal = ValidateJWTToken(token);
string name = claims.Identity.Name;
```

---
## EasGenerate
EasGenerate helps you with generating random string with several options.

### Define Class
```c#
EasGenerate _easgenerate = new EasGenerate();
```

### Usage
```c#
//Definition
public string GenerateRandomString(int length, string allowedChars = "", bool onlyLetter = false){};

//Usage
var password = _easgenerate.GenerateRandomString(16,"[]{}()'^+*");
```
