# EasMe Library
This library provides various classes and methods that will help you save time and avoid repetitive tasks among different projects.
This is a work in progress and will be updated frequently.

## Table of Contents
- [Installation](#installation)
- [License](#license)
- [EasMe](#easme)
  - [Extensions](#extensions)
  - [EasAPI](#easapi)
  - [EasCache](#eascache)
  - [EasCheck](#eascheck)
  - [EasConfig](#easconfig)
  - [EasDirectory](#easdirectory)
  - [EasEncrypt](#easencrypt)
  - [EasFile](#easfile)
  - [EasGenerate](#easgenerate)
  - [EasHash](#eashash)
  - [EasHttp](#eashttp)
  - [EasINI](#easini)
  - [EasJWT](#easjwt)
  - [EasMail](#easmail)
  - [EasMemoryCache](#easmemorycache)
  - [EasQL](#eassql)
  - [EasReCaptcha](#easrecaptcha)
  - [EasTask](#eastask)
  - [EasValidate](#easvalidate)
  - [EasZip](#easzip)
  - [EasMe.Scheduler](#easscheduler)
- [EasMe.Authorization .NET 6](#easmeauthorization)
  - [Enums](#authorization-enums)
  - [Action permission authorization](#haspermissionattribute)
  - [Http method permission authorization](#httpmethodauthorizationmiddleware)
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
Simple encryption and decryption with AES
```csharp
var encryptor = new EasEncrypt();
```
### EasFile
### EasGenerate
### EasHash
### EasHttp
### EasINI
### EasJWT
### EasMail
### EasMemoryCache
### EasQL
### EasReCaptcha
### EasTask
### EasValidate
### EasZip
### EasScheduler

## EasMe.Authorization
### Authorization Enums
### HasPermissionAttribute
### HttpMethodAuthorizationMiddleware

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
