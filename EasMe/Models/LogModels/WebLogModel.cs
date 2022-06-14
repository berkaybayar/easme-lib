using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal class WebLogModel : BaseLogModel
    {

        public string? Ip { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestUrl { get; set; }
        public string? Headers { get; set; }
        

    }


}
