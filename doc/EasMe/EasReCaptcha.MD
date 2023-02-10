# EasReCaptcha
Google Captcha Helper for AspNetCore Web projects

You must create your site secret key in Google Captcha website

## Requirements
- Newtonsoft.Json
- System.Net

## Definitions
```c#
public static CaptchaResponseModel Validate(string Secret, string? CaptchaResponse);
```

## CaptchaResponseModel
```c#
public bool Success { get; set; } = false;
public DateTime ChallengeTS { get; set; }
public string ApkPackageName { get; set; }
public string ErrorCodes { get; set; }
```

## Usage
appsettings.json
```json
  "ReCaptcha": {  
    "SiteKey": "YOUR-SITE-KEY",  
    "SecretKey": "YOUR-SECRET-KEY",  
    "Version": "v2"
    }
```

View 
```html
<div class="g-recaptcha" data-sitekey="YOUR-SITE-KEY"></div>		
<script src="https://www.google.com/recaptcha/api.js" async defer></script>
```

Controller
```c#
var CaptchaResponse = HttpContext.Request.Form["g-recaptcha-response"];
string Secret = "your-super-secret-key";       
var Captcha = EasReCaptcha.Validate(Secret, CaptchaResponse);
```

