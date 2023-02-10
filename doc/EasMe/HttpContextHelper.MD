# HttpContextHelper
Access HttpContext via HttpContextAccessor for AspNetCore Web applications

## Usage
Program.cs
```c#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
```
Anywhere in the application

```c#
using EasMe.Helpers;

//Be careful with this when using Async Tasks if not awaited
//Context might be null on those cases since thread will go on and Context can be disposed
//It is recommended to access and save this to a variable before starting an Async task in the background 
//Example: Logging
var context = HttpContextHelper.Current;
```