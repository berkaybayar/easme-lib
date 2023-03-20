namespace EasMe.Models;

public class CaptchaResponseModel
{
    public bool Success { get; set; } = false;
    public DateTime ChallengeTS { get; set; }
    public string ApkPackageName { get; set; }
    public string ErrorCodes { get; set; }
}