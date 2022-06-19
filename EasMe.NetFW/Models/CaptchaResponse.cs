using System;

namespace EasMe.Models
{
    public class CaptchaResponse
    {
        public bool Success { get; set; } = false;
        public DateTime ChallengeTS { get; set; }
        public string ApkPackageName { get; set; }
        public string ErrorCodes { get; set; }
    }
}
