using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal class WebLogModel
    {
        private string _ip = "";
        public string Ip { 
            get 
            {
                return _ip;
            } 
            set 
            {
                if (!EasValidate.IsValidIPAddress(Ip, out string version))
                {
                    _ip = "Not Valid Ip Address";                    
                    return;
                }
                if (string.IsNullOrEmpty(Ip))
                {
                    _ip = "null";
                    return;
                }
                _ip = Ip;
                
            }
        }
        public string? HttpMethod { get; set; }
        public string? RequestUrl { get; set; }
        public string? Headers { get; set; }
        

    }


}
