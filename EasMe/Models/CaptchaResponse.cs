using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models
{
    public class CaptchaResponse
    {
        public bool Success { get; set; }
        public DateTime ChallengeTS { get; set; }
        public string ApkPackageName { get; set; }
        public string ErrorCodes { get; set; }
    }
}
