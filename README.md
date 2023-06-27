# EasMe Library
This library provides various classes and methods that will help you save time and avoid repetitive tasks among different projects.
This is a work in progress and will be updated frequently.

## Table of Contents
- [Installation](#installation)
- [License](#license)
- [EasMe](#easme)
  - [Extensions](#extensions)
    - [String Extensions](#string-extensions)
    - [Byte Extensions](#byte-extensions)
    - [Claims Extensions](#claims-extensions)
    - [DateTime Extensions](#datetime-extensions)
    - [Object Extensions](#object-extensions)
    - [Bool Extensions](#bool-extensions)
    - [Dictionary Extensions](#dictionary-extensions)
    - [HttpContext Extensions](#httpcontext-extensions)
    - [List Extensions](#list-extensions)
    - [Number Extensions](#number-extensions)
    - [Json Extensions](#json-extensions)
    - [Xml Extensions](#xml-extensions)
    - [Hash Extensions](#hash-extensions)
    - [Validation Extensions](#validation-extensions)
  - [EasAPI](#easapi)
  - [EasCache](#eascache)
  - [EasCheck](#eascheck)
  - [EasConfig](#easconfig)
  - [EasDirectory](#easdirectory)
  - [EasEncrypt](#easencrypt)
  - [EasFile](#easfile)
  - [EasGenerate](#easgenerate)
  - [EasHttp](#eashttp)
  - [EasINI](#easini)
  - [EasJWT](#easjwt)
  - [EasMail](#easmail)
  - [EasMemoryCache](#easmemorycache)
  - [EasQL](#eassql)
  - [EasReCaptcha](#easrecaptcha)
  - [EasTask](#eastask)
  - [EasZip](#easzip)
  - [EasMe.Scheduler](#easscheduler)
- [EasMe.Authorization .NET 6](#easmeauthorization)
  - [Enums](#authorization-enums)
  - [Authentication with permissions](#authentication-with-permission)
  - [Authentication with http methods](#authentication-with-http-methods)
- [EasMe.Box .NET Framework 4.8](#easmebox)
- [EasMe.EntityFrameworkCore .NET 6](#easmeentityframeworkcore)
  - [Entity abstracts](#entity-abstracts)
  - [Repository abstracts](#repository-abstracts)
  - [Generic repository](#generic-repository)
- [EasMe.Logging .NET 6](#easmelogging)
  - [EasLog](#easlog)
  - [EasLogConsole](#easlogconsole)
  - [EasLogFactory](#easlogfactory)
  - [Configuration](#log-configuration)
  - [EasLogReader](#easlogreader)
- [EasMe.PostSharp .NET 6](#easmepostsharp)
- [EasMe.Result .NET 6](#easmeresult)
  - [Result](#result)
  - [Result with data](#result-with-data)
  - [Result Enums](#result-enums)
- [EasMe.SharpBuilder .NET 6](#easmesharpbuilder)
  - [Class builder](#easmesharpbuilder)
  - [File builder](#file-builder)
  - [Property builder](#property-builder)
- [EasMe.System .NET 6 (Windows only)](#easmesystem)
  - [Getting device information](#getting-device-information)
  - [Creating unique id](#creating-unique-id)
  - [Adding application to windows startup](#adding-application-to-windows-startup)
- [EasMe.Test](#easmetest)

## Installation
You can install the package via Nuget:
```
Install-Package EasMe
Install-Package EasMe.Authorization
Install-Package EasMe.Box
Install-Package EasMe.EntityFrameworkCore
Install-Package EasMe.Logging
Install-Package EasMe.PostSharp
Install-Package EasMe.Result
Install-Package EasMe.SharpBuilder
Install-Package EasMe.System
```

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

<a name="easme"></a>
## EasMe

### Extensions
#### String Extensions
```csharp
var str = "Hello World";
var removedText = str.RemoveText("World"); // Hello
var before = str.GetBefore("World"); // Hello 
var after = str.GetAfter("Wo"); // rld
var between = str.GetBetween("Hell", "orld"); // o W
var isNullOrEmpty = str.IsNullOrEmpty(); // false
var isNullOrWhiteSpace = str.IsNullOrWhiteSpace(); // false
var isNotNullOrEmpty = str.IsNotNullOrEmpty(); // true
var isNotNullOrWhiteSpace = str.IsNotNullOrWhiteSpace(); // true
var bytes = str.GetBytes(); // byte[]

var number = "1231";
var converted = number.ConvertTo<int>(); // 1231
var convertedNullable = number.ConvertTo<int?>(); // 1231

var formatText = "Hello {0}".Format("World"); // Hello World

var whiteSpaceRemoved = "Hello World".RemoveWhiteSpace(); // HelloWorld

var boolean = "true".ToBoolean(); // true
var boolean2 = "false".ToBoolean(); // false
var boolean3 = "0".ToBoolean(); // false
var boolean4 = "1".ToBoolean(); // true
var boolean5 = "asd".ToBoolean(); // true
var boolean6 = "".ToBoolean(); // false

var firstToUpper = "hello world".FirstToUpper(); // Hello world
var firstToUpperRestLower = "hello world".FirstToUpperRestLower(); // Hello world
var lastToUpper = "hello world".LastToUpper(); // hello WorlD
var lastToLower = "hello worLD".LastToLower(); // hello worLd

var longString = "Hello World,Hello World,Hello World";
var truncatedString = longString.Truncate(10); // Hello Worl...

var removedLineEndings = "Hello World\nHello World".RemoveLineEndings(); // Hello WorldHello World

var toHexString = "Hello World".ToHexString(); // 48656C6C6F20576F726C64
var fromHexString = "48656C6C6F20576F726C64".FromHexString(); // Hello World
var toBase64String = "Hello World".ToBase64String(); // SGVsbG8gV29ybGQ=
var fromBase64String = "SGVsbG8gV29ybGQ=".FromBase64String(); // Hello World

var replaceReverse = "Hello World, oldValue".Replace_Reverse("newValue", "oldValue"); // Hello World, 

var secureString1 = "Hello World".ToSecureString(); // System.Security.SecureString
var secureString2 = secureString1.ToInsecureString(); // Hello World

var secureStringsEqual = secureString1.SecureStringsEqual(secureString1); // true

var dateString = "2021-01-01";
var isValidDateTime = dateString.IsValidDateTime(); // true
```
#### Byte Extensions
```csharp
var bytes = new byte[] { 1, 2, 3, 4, 5 };
var hexString = bytes.ToHexString(); // 0102030405
var base64String = bytes.ToBase64String(); // AQIDBAU=

var compared = bytes.Compare(bytes); // true
```

#### Claims Extensions
```csharp
var claims = new List<Claim>
{
    new Claim("name", "John"),
    new Claim("surname", "Doe")
};
var asDictionary = claims.ToDictionary(); // Dictionary<string, string>

var user = new
{
    Name = "John",
    Surname = "Doe"
};
var claimsIdentity = user.ToClaimsIdentity(); // System.Security.Claims.ClaimsIdentity
var claimsIdentity2 = user.ToClaimsIdentity(out List<Exception> exceptions); // System.Security.Claims.ClaimsIdentity
```

#### DateTime extensions
```csharp
var dateTime = new DateTime(2021, 01, 01);
var nullableDateTime = (DateTime?)dateTime;
var inFuture = dateTime.InFuture(); // false
var inPast = dateTime.InPast(); // true

var notNullableDatetime = nullableDateTime.GetValueOrDefault(); // DateTime

var unixTime = dateTime.ToUnixTime(); //  1609459200
var isDayOlder = dateTime.IsDayOlder(dateTime, 1); // true
var isMinutesOlder = dateTime.IsMinutesOlder(dateTime, 1); // true
var isSecondsOlder = dateTime.IsSecondsOlder(dateTime, 1); // true
var isHoursOlder = dateTime.IsHoursOlder(dateTime, 1); // true
var isMonthsOlder = dateTime.IsMonthsOlder(dateTime, 1); // true
var isYearsOlder = dateTime.IsYearsOlder(dateTime, 1); // true

var readableDateString = dateTime.ToReadableDateString(); // 2 years and 1 month ago
```
#### Object Extensions
```csharp
var user = new
{
    Name = "John",
    Surname = "Doe"
};
var userDto = user.As<UserDto>(x => {
    x.Name = user.Name;
    x.Surname = user.Surname;
}); 

var isValidModel = user.IsValidModel(); // true
var isNull = user.IsNull(); // false
var isNotNull = user.IsNotNull(); // true
var isDefault = user.IsDefault(); // false
```
#### Bool Extensions
```csharp
var nullableBool = (bool?)true;
var notNullableBool = nullableBool.GetValueOrDefault(); // true
```
#### Dictionary Extensions
```csharp
var dictionary = new Dictionary<string, string>
{
    { "name", "John" },
    { "surname", "Doe" }
};
var asQueryString = dictionary.ToQueryString(); // ?name=John&surname=Doe
var asObject = dictionary.ToObject<User>(); // User object with name and surname properties
```
#### HttpContext Extensions
```csharp
var httpContext = new HttpContextAccessor().HttpContext; // Microsoft.AspNetCore.Http.HttpContext
var headerValuesAsDictionary = httpContext.Request.GetHeaderValues(); // Dictionary<string, string>
var xForwardedForOrRemoteIp = httpContext.Request.GetXForwardedForOrRemoteIp(); //
var headerIpAddressList = httpContext.Request.GetHeaderIpAddressList(); // string[]
var remoteIpAddress = httpContext.Request.GetRemoteIpAddress(); // string
var userAgent = httpContext.Request.GetUserAgent(); // string
var isLocal = httpContext.Request.IsLocal(); // true
var query = httpContext.Request.GetQuery(); // Dictionary<string, string>
var queryValue = httpContext.Request.GetQueryValue("name"); // John
var queryAsDictionary = httpContext.Request.GetQueryAsDictionary(); // Dictionary<string, string>
```
#### List Extensions
```csharp
var list = new List<string> { "John", "Jane" };
list.AddIfNotExists("John"); // false
list.AddIfNotExists("Doe"); // true
list.ForEach(x => Console.WriteLine(x)); // John, Doe
var joinString = list.JoinString(","); // John, Doe
var foreachResult = list.ForEachResult<bool>(x => {
    var isJohn = x == "John";
    return isJohn;
}); // List<bool>

var dataTable = list.ToDataTable(); // System.Data.DataTable
var selectRandom = list.SelectRandom(); // John or Doe
var shuffle = list.Shuffle(); // List<string>
var asObjectList = list.ToObjectList(); // List<object>

list.UpdateAll(x => {
    x += " Doe";
}); // John Doe, Jane Doe

list.UpdateAllWhere(x => x.EndsWith("n")), x => {
    x += " Doe";
}); // John Doe, Jane Doe
```
#### Number Extensions
```csharp
var dateTime = 1609459200.UnixTimeStampToDateTime(); // 2021-01-01 00:00:00
var dateTime2 = 1609459200.TicksToDateTime(); // 2021-01-01 00:00:00

var isBetween = 5.IsBetween(1, 10); // true
var isBetweenOrEqual = 5.IsBetweenOrEqual(1, 10); // true
var isInRange = 5.IsInRange(1, 10); // true

var nullableInt = (int?)5;
var notNullableInt = nullableInt.GetValueOrDefault(); // 5

var nullableLong = (long?)5;
var notNullableLong = nullableLong.GetValueOrDefault(); // 5
```
#### Json Extensions
```csharp
var user = new User
{
    Name = "John",
    Surname = "Doe"
};
var json = user.ToJsonString(); // {"Name":"John","Surname":"Doe"}
var user2 = json.FromJsonString<User>(); // User object with name and surname properties
```

#### Xml Extensions
```csharp
var xElement = new XElement("User", new XElement("Name", "John"), new XElement("Surname", "Doe"));
var xElementList = new List<XElement>(){
    xElement,
    xElement
}

 xmlDeserialize = xElement.XmlDeserialize<User>(); // User object with name and surname properties

var xmlDeserializeList = xElementList.XmlDeserialize<List<User>>(); // List<User> object with name and surname properties

var userObject = new User
{
    Name = "John",
    Surname = "Doe"
};
var asXml = userObject.ToXmlString(); // <User><Name>John</Name><Surname>Doe</Surname></User>
var cleanXmlString = asXml.ToCleanXmlString(); // <User><Name>John</Name><Surname>Doe</Surname></User>
var asXElement = userObject.ToXElement(); // <User><Name>John</Name><Surname>Doe</Surname></User>
var asXElementPropertiesAsAttributes = userObject.ToXElement(true); // <User Name="John" Surname="Doe" />
var asXElementPropertiesAsElement = userObject.ToXElement(false); // <User><Name>John</Name><Surname>Doe</Surname></User>
```

#### Hash Extensions
```csharp
var md5Byte = "text".MD5Hash(); // byte[]
var md5ByteSalted = "text".MD5Hash("salt"); // byte[]
var sha256Byte = "text".SHA256Hash(); // byte[]
var sha256ByteSalted = "text".SHA256Hash("salt"); // byte[]
var sha512Byte = "text".SHA512Hash(); // byte[]
var sha512ByteSalted = "text".SHA512Hash("salt"); // byte[]
var xxHashByte = "text".XXHash(); // byte[]

var filePath = "C:\\Users\\John\\Desktop\\test.txt";
var md5File = filePath.FileMD5Hash(); // byte[]
var xxFile = filePath.FileXXHash(); // byte[]
```

#### Validation Extensions
```csharp
var isValidEmail = "mail@mail.com".IsValidEmail(); //true
var isValidIp = "".IsValidIpAddress(); //false
var isValidIp2 = "11.11.11.11".IsValidIpAddress(out IpAddress ipAddress); //true

//Validates full file path with extension. If given file path is relative path it will be false
var isValidFilePath = "\\Test.txt".IsValidFilePath(); //false

var isValidMacAddress = "00-00-00-00-00-00".IsValidMacAddress(); //true
var isValidPort = 8080.IsValidPort(); //true
var hasSpecialCharacters = "Test".ContainsSpecialChars(); //false
var isStrongPassword = "Test123!".IsStrongPassword(
                                    "!+'-", //Allowed special characters
                                    4, //Minimum length
                                    32, //Maximum length
                                    1, //Minimum number of uppercase letters
                                    1, //Minimum number of lowercase letters
                                    1, //Minimum number of digits
                                    1 //Minimum number of special characters
                                    ); 
var isUrlImage = "https://www.test.com/test.jpg".IsUrlImage(); //true     
var isValidUrl = "https://www.test.com".IsValidUrl(); //true
var isValidConnectionString = "Server=.;Database=Test;Trusted_Connection=True;".IsValidConnectionString(); //true
var isValidCreditCard = "1111-1111-1111-1111".IsValidCreditCard("01/01",999); //true         
```
### EasAPI
Every method has a token and timeout parameter as optional. Depending on the http request method a body is required.
```csharp
var dummyUrl = "https://127.0.0.1/";
var dummyAuthorizationToken = "123123213";
var get = EasAPI.Get(dummyUrl); // HttpResponseMessage
var getWithAuthorization = EasAPI.Get(dummyUrl,dummyAuthorizationToken); // HttpResponseMessage
var getWithAuthorizationAndTimeOut = EasAPI.Get(dummyUrl,dummyAuthorizationToken,10); // HttpResponseMessage
var getAndReadJson = EasAPI.GetAndReadJson(dummyUrl); // JObject
var postAsJsonAndReadJson = EasAPI.PostAsJsonAndReadJson(dummyUrl, new { Name = "John" }); // JObject 
var postAsJson = EasAPI.PostAsJson(dummyUrl, new { Name = "John" }); // HttpResponseMessage
var delete = EasAPI.Delete(dummyUrl); // HttpResponseMessage
var putAsJson = EasAPI.PutAsJson(dummyUrl, new { Name = "John" }); // HttpResponseMessage
var send = EasAPI.Send(dummyUrl, new HttpRequestMessage(),dummyAuthorizationToken, 10); // HttpResponseMessage
```
### EasCache
Simple memory caching
```csharp
List<User> GetUsersFromDb(){ /* db access */}

var easCache = new EasCache<List<User>>(GetUsersFromDb, 10); // 10 minutes
var users = easCache.Get(); // List<User>
```

Caching with key and value
```csharp
User GetUserFromDb(int userId){ /* db access */}

var easCache = new EasCache<int ,List<User> >(GetUserFromDb, 10); // 10 minutes
var user = easCache.Get(231); // Nullable<User>
```
### EasCheck
```csharp
var password = "123123";
var passwordScore = EasCheck.CheckPasswordStrength(password); // PasswordScore Enum
```

PasswordScore enum
```csharp
public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}
```
### EasConfig
Easily access app.config or web.config
```csharp
var connString = EasConfig.GetConnectionString("DefaultConnection"); // string
var someValue = EasConfig.GetValue("someValue"); // string
var someIntValue = EasConfig.GetValue<int>("someIntValue"); // int
```
### EasDirectory
Get known folder paths
```csharp
var downloadsPath = EasDirectory.GetPath(KnownFolder.Downloads); // string
var contactsPath = EasDirectory.GetPath(KnownFolder.Contacts); // string
var favoritesPath = EasDirectory.GetPath(KnownFolder.Favorites); // string
var linksPath = EasDirectory.GetPath(KnownFolder.Links); // string
var savedGamesPath = EasDirectory.GetPath(KnownFolder.SavedGames); // string
var savedSearchesPath = EasDirectory.GetPath(KnownFolder.SavedSearches); // string
```

KnownFolder enum
```csharp
public enum KnownFolder
{
    Contacts,
    Downloads,
    Favorites,
    Links,
    SavedGames,
    SavedSearches
}
```
### EasEncrypt
Encryption and decryption with Aes algorithm

#### Startup.cs configuration
```csharp
EasEncrypt.SetKey("SuperSecretEncryptionKey");

//Only if you want to use time seeding, see below
EasEncrypt.UseTimeSeeding(EasEncrypt.Sensitivity.Minutes); 

//Only if debug build
EasEncrypt.DontUseEncryption();
```

#### Usage
```csharp
var encryptor = EasEncrypt.Create();
var encrypted = encryptor.Encrypt("123123"); // encrypted string
var decrypted = encryptor.Decrypt(encrypted); // 123123
```

#### Time seeding
Time seeding creates a salt key based on the time the instance is created.
You can set the sensitivity of the time seeding when enabling time seeding with EasEncrypt.UseTimeSeeding method.
If sensitivity set to seconds, the salt key will change every second. Meaning the encrypted key will also change.
Created salt key will be stored inside of the instance. You can use same instance to decrypt the encrypted text,
even after the time has changed. 

**WARNING:** If you want to store the encrypted text in a database and decrypt it later, be aware that the salt key might change and you may not be able to recover the original text.

#### Static seed
Static seed is used to alter the time seeded salt key. 
You can set the static seed with EasEncrypt.UseTimeSeeding method.
This mostly set to project build number so every build will have a different salt key.
Default value is set to 0, if you have not set it, it will not be used.

### EasFile
```csharp
var sourcePath = @"C:\Users\John\Desktop\test.txt";
var destinationPath = @"C:\Users\John\Desktop\test2.txt";
var sourceDirectoryPath = @"C:\Users\John\Desktop\test";

EasFile.DeleteAll(destinationPath); // Deletes all files and directories inside of the destination path
EasFile.CopyAll(sourcePath, destinationPath); // Copies the file or directory from source to destination
EasFile.MoveAll(sourcePath, destinationPath); // Moves the file or directory from source to destination

// Deletes all files and directories inside of the source path where the name contains "test"
EasFile.DeleteDirectoryWhere(sourceDirectoryPath,x => x.Name.Contains("test"), true); 

// Deletes all files inside of the source path where the name contains "test"
EasFile.DeleteFileWhere(sourceDirectoryPath,x => x.Name.Contains("test")); 

var fileInfo1 = new FileInfo(sourcePath); // FileInfo
var fileInfo2 = new FileInfo(destinationPath); // FileInfo

//Reads 64 bytes on each loop till the end of the file to compare
var filesAreEqual = EasFile.FilesAreEqual(fileInfo1, fileInfo2); // bool

//Reads 1 byte on each loop till the end of the file to compare
var filesAreEqualOneByte = EasFile.FilesAreEqual_OneByte(fileInfo1, fileInfo2); // bool

//Reads all bytes at once and hashes them with MD5 to compare
var filesAreEqualMD5Hash = EasFile.FilesAreEqual_MD5Hash(fileInfo1, fileInfo2); // bool

//Reads all bytes at once and hashes them with xxHash to compare
var filesAreEqualXXHash = EasFile.FilesAreEqual_XXHash(fileInfo1, fileInfo2); // bool
```
### EasGenerate
```csharp

const string DefaultCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; //Hardcoded
var randomString = EasGenerate.RandomString(10); // Random string with 10 characters
var randomInt = EasGenerate.RandomInt(100, 200); // Random int between 100 and 200
var randomNumber = EasGenerate.RandomNumber(100); // Random number with 100 digits
var randomString1 = EasGenerate.RandomString(10, true); // Random string without digits
var randomString1 = EasGenerate.RandomString(10, true, "'^%!"); // Random string without digits and with custom characters
```

### EasHttp
```csharp
var statusCodeShortMessage = EasHttp.GetStatusCodeShortMessage(200); // OK
var statusCodeLongMessage = EasHttp.GetStatusCodeLongMessage(500); // Internal Server Error
```
### EasINI
You can read and write to .ini files with EasINI class. 
Also you can parse the .ini file to IniFile model.
```csharp
var iniFile = EasINI.Parse("C:\\Users\\John\\Desktop\\test.ini"); 

var easIni = new EasINI(iniFile);
easIni.Write("Section", "Key", "Value");
var value  = easIni.Read("Section", "Key");

var iniFile = EasINI.ParseByPath(iniFile);
var str = iniFile.WriteToString();
iniFile.WriteToPath(iniFile);
```

IniFile models 
```csharp
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
```
### EasJWT
```csharp
var easJwt = new EasJWT("Secret","Issuer","Audience");// or new EasJWT("Secret");
var claimsIdentity = new ClaimsIdentity(new Claim[]
{
    new Claim(ClaimTypes.Name, "John"),
    new Claim(ClaimTypes.Role, "Admin"),
    new Claim(ClaimTypes.Role, "User"),
});
var token = easJwt.GenerateToken(claimsIdentity,60); // Generates token with claims for 60 minutes

var claimsDictionary = new Dictionary<string, object>
{
    {"Name", "John"},
    {"Role", "Admin"},
    {"Role", "User"},
}; 
var token2 = easJwt.GenerateToken(claimsDictionary,60); // Generates token with claims for 60 minutes

// Validates token and returns ClaimsPrincipal, if token is expired or is not valid it throws exception.
var claimsPrincipal = easJwt.ValidateToken(token); 
```
### EasMail
```csharp
//Create EasMail instance with smtp server, mail address, password, port and ssl
var easEmail = new EasMail("smtp.gmail.com", "mailAddress", "password", 57, true);

easEmail.Send("Subject","Body","mailAddress", true); // Sends mail to mailAddress with html body
easEmail.Send("Subject","Body","mailAddress", false); // Sends mail to mailAddress 
```
### EasMemoryCache
Improved caching from EasCache. Both are viable and working the way how caching and receiving data is different.
This is a singleton class without any service or injection required.
```csharp
EasMemoryCache.This.Set("Key", "Value", 60); // Sets cache for 60 seconds
var value = EasMemoryCache.This.Get("Key"); // Gets cache value
var valueStr = EasMemoryCache.This.Get<string>("Key"); // Gets cache value as string

var keyExists = EasMemoryCache.This.Exists("Key"); // Checks if cache exists
EasMemoryCache.This.Remove("Key"); // Removes cache
EasMemoryCache.This.Clear(); // Clears all cache

// Gets cache value or sets it if it doesn't exist and then returns it
var value = EasMemoryCache.This.GetOrSet("Key", () => "Value", 60); 
```

### EasQL
Simple and easy to use SQL database access.
This is not related to EntityFrameworkCore it uses Microsoft.Data.SqlClient.
You can use static methods or create an instance of EasQL class and pass connection string.
Here is an example of creating instance. When using static methods connection string must be passes everytime.
```csharp
var easQL = new EasQL("Server=.;Database=Test;Trusted_Connection=True;");
var command = new SqlCommand("SELECT * FROM TestTable");
var result = easQL.GetTable(command); //DataTable
var result2 = easQL.ExecNonQuery(command); //int
var result3 = easQL.ExecNonQueryAsync(command); //Task<int>
var result4 = easQL.ExecScalar(command); //object
var result5 = easQL.ExecScalarAsync(command); //Task<object>

easQL.BackupDatabase("C:\\Users\\John\\Desktop\\Test.bak", "DbName"); // Backups database
easQL.BackupDatabaseAsync("C:\\Users\\John\\Desktop\\Test.bak", "DbName"); // Backups database and returns Task
easQL.ShrinkDatabase("DbName","DbLogFileName");

easQL.TruncateTable("TableName");
easQL.TruncateTableAsync("TableName"); // Returns Task
easQL.DropTable("TableName");
easQL.DropTableAsync("TableName"); // Returns Task
var tables = easQL.GetAllTableNames();
```

### EasReCaptcha
```csharp
//validates google captcha 
var secret = "Secret";
var captchaResponse = "Response";
var result = EasReCaptcha.Validate(secret, captchaResponse);
```

Captcha validation result model
```csharp
public class CaptchaResponseModel
{
    public bool Success { get; set; } = false;
    public DateTime ChallengeTS { get; set; }
    public string ApkPackageName { get; set; }
    public string ErrorCodes { get; set; }
}
```

### EasTask
A task manager that can run tasks in background in a queue.
```csharp
var easTask = new EasTask();
easTask.AddTask(() => Console.WriteLine("Task 1"));
easTask.AddTask(() => Console.WriteLine("Task 2"));
easTask.AddTask(() => Console.WriteLine("Task 3"));

//On Application Exit or wait all tasks to finish
easTask.Flush();

//Disposes easTask and all the related tasks and threads
//This method will not wait for tasks to be ended 
easTask.Dispose(); 
```
### EasZip
```csharp
var fileList = new List<string>();
var zipFile = "C:\\Users\\John\\Desktop\\Test.zip";
EasZip.MakeZip(fileList, zipFile); // Makes zip file from fileList
EasZip.UnZip(zipFile, "C:\\Users\\John\\Desktop\\Test"); // Unzips zipFile to destination
```
### EasScheduler
```csharp
//Not implemented
```

## EasMe.Authorization
Enhance your AspNetCore authorization with permission based authorization rather than role based.
There are 2 options with this library. You can set permissions for specific http methods for each user.
Or you can set role for each endpoint or controller. It is easy to use and manage.
It is recommended to use a role group and set permissions for each group then assign the role group to a user.
In login stage get all the permissions and add it to claims. 

**WARNING:** Both of the authorization methods do not check for user Authentication. 
Meaning you must use built-in [Authorize] attribute to first check if user authentication is required on desired endpoint or controller.
If you don't do this RequirePermission ActionFilter or HttpMethodAuthorizationMiddleware will not work.
### Authorization Enums
```csharp
public enum HttpMethod
{
    GET,
    POST,
    PUT,
    DELETE,
    HEAD,
    OPTIONS,
    PATCH,
}

var claimType = EasMeClaimType.HttpMethodPermissions; // Claim type for http method permissions
var claimType2 = EasMeClaimType.EndPointPermissions; // Claim type for endpoint permissions
```
### Authentication with permission
This attribute is used to check if user has permission to access endpoint. 
This is different from role based authorization because instead of assigning roles to actions and only allowing specific roles.
With this you can create a role group and easily get the all the permissions for that group.
Then add that list of permissions to user claims to pre-defined ClaimType (see above).  

#### Controller action
```csharp
public enum Permissions
{
    AccountSettings,
    UpdateUser,
    UpdateUserPassword,
    //... etc.
}
[RequirePermission(Permissions.AccountSettings)] //Action filter that checks if user has permission to access endpoint
[HttpGet] // or something else does not matter
public IActionResult AccountSettings(){ /* Your code */}
```

### Authentication with http methods
#### Program.cs
```csharp
var builder = WebApplication.CreateBuilder(args);
//Other configurations
var app = builder.Build();
//Other configurations
app.UseMiddleware<HttpMethodAuthorizationMiddleware>();
```
### Creating user claims
Example of creating JWT token with user claims and permissions.
Use the example if you are using JWT authentication.
Otherwise change the code to fit your needs.
This example uses EasJWT library to create JWT token.
In the example both EndPointPermissions and HttpMethodPermissions are added to claims.
You can use both at same time. Or you can use only one of them.


If using EndPointPermissions:
Once user is authenticated and tries to access an endpoint RequirePermission attribute will check if user has permission to access endpoint.
If user does not have permission then 403 Forbidden will be returned.


If using HttpMethodPermissions:
Once user is authenticated and tries to access an endpoint HttpMethodAuthorizationMiddleware checks has permission for HttpMethod.

```csharp

var easJwt = new EasJwt("SecretToken"); // Create EasJWT instance
var user = new User(); // Get user from database
var httpMethodPermissions = new List<string>(){
    HttpMethod.GET.ToString(),
    HttpMethod.POST.ToString(),
    //... etc.
}; // Get permissions from database
var endpointPermissions = new List<string>(){
    Permissions.AccountSettings.ToString(),
    Permissions.UpdateUser.ToString(),
    Permissions.UpdateUserPassword.ToString(),
    //... etc.
}; // Get permissions from database
var dictionary = user.AsDictionary(); // Convert user to dictionary
dictionary.Add(EasMeClaimType.HttpMethodPermissions, string.Join(",",httpMethodPermissions)); // Add permissions to dictionary
dictionary.Add(EasMeClaimType.EndPointPermissions, string.Join(",",endpointPermissions)); // Add permissions to dictionary
var jwtToken = easJwt.GenerateToken(dic, 60); 
```
## EasMe.Box

## EasMe.EntityFrameworkCore
### Entity abstracts
### Repository abstracts
### Generic repository

## EasMe.Logging
### EasLog
### EasLogConsole
### EasLogFactory
### Log Configuration
### EasLogReader


## EasMe.PostSharp

## EasMe.Result
### Result
### Result with data
### Result Enums

## EasMe.SharpBuilder
### Class builder
### File builder
### Property builder

## EasMe.System
### Getting device information
### Creating unique id
### Adding application to windows startup

## EasMe.Test
This Console Application project is for testing newly added functionality.
Not a unit test project.
