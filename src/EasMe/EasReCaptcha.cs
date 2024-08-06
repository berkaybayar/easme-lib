﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using EasMe.Models;
using Newtonsoft.Json.Linq;

namespace EasMe;

public static class EasReCaptcha
{
  /// <summary>
  ///   Validates given CaptchtaResponse from Google by SecretKey.
  /// </summary>
  /// <param name="secret"></param>
  /// <param name="captchaResponse"></param>
  /// <returns></returns>
  public static CaptchaResponseModel Validate(string secret, string? captchaResponse) {
    try {
      if (captchaResponse.IsNullOrEmpty())
        return new CaptchaResponseModel {
          Success = false
        };
      var response = new CaptchaResponseModel();
      var client = new WebClient();
      var result = client.DownloadString(string.Format(
                                                       $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={captchaResponse}"));
      var obj = JObject.Parse(result);
      response.Success = (bool)obj.SelectToken("success");
      response.ChallengeTS = (DateTime)obj.SelectToken("challenge_ts");
      response.ApkPackageName = (string)obj.SelectToken("apk_package_name");
      response.ErrorCodes = (string)obj.SelectToken("error-codes");
      return response;
    }
    catch (Exception ex) {
      throw new ValidationException("Could not validate reCaptcha.", ex);
    }
  }
}