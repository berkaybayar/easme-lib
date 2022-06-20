using EasMe.Models;
using Newtonsoft.Json.Linq;
using System.Net;


namespace EasMe
{

    public static class EasReCaptcha
    {

        /*
        -Requirements
        Newtonsoft.Json
        System.Net
        
        -appsettings.json
          "ReCaptcha": {
          "SiteKey": "YOUR-SITE-KEY",
          "SecretKey": "YOUR-SECRET-KEY",
          "Version": "v2"
        }
        -Program.cs
        builder.Services.AddReCaptcha(builder.Configuration);

        -View
        <div class="g-recaptcha" data-sitekey="YOUR-SITE-KEY"></div>		
        <script src="https://www.google.com/recaptcha/api.js" async defer></script>

        -Controller
        var CaptchaResponse = HttpContext.Request.Form["g-recaptcha-response"];
        string Secret = "your-secret-key";       
        var Captcha = _recaptcha.Validate(Secret, CaptchaResponse);
        */


        public static CaptchaResponseModel Validate(string Secret, string CaptchaResponse)
        {

            try
            {
                var response = new CaptchaResponseModel();
                var client = new WebClient();
                var result = client.DownloadString(string.Format($"https://www.google.com/recaptcha/api/siteverify?secret={Secret}&response={CaptchaResponse}"));
                var obj = JObject.Parse(result);
                response.Success = (bool)obj.SelectToken("success");
                response.ChallengeTS = (DateTime)obj.SelectToken("challenge_ts");
                response.ApkPackageName = (string)obj.SelectToken("apk_package_name");
                response.ErrorCodes = (string)obj.SelectToken("error-codes");
                return response;
            }
            catch (Exception ex)
            {
                return new CaptchaResponseModel { Success = false, ErrorCodes = ex.Message };
            }


        }
    }

}
