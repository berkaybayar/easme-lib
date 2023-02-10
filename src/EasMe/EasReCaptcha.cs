using EasMe.Exceptions;
using EasMe.Extensions;
using EasMe.Models;
using Newtonsoft.Json.Linq;
using System.Net;


namespace EasMe
{

    public static class EasReCaptcha
    {

        /// <summary>
        /// Validates given CaptchtaResponse from Google by SecretKey.
        /// </summary>
        /// <param name="Secret"></param>
        /// <param name="CaptchaResponse"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static CaptchaResponseModel Validate(string Secret, string? CaptchaResponse)
        {
            try
            {
                if (CaptchaResponse.IsNullOrEmpty())
                {
                    return new CaptchaResponseModel
                    {
                        Success = false,
                    };
                }
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
                throw new FailedToValidateException("Could not validate reCaptcha.", ex);
            }


        }
    }

}
