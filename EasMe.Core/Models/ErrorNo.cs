using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models
{
    public static class ErrorNo
    {
       
        public static Dictionary<int, string> List = new Dictionary<int, string>()
        {
            {0,"Success"},
            {1,"Information"},
            {2,"Warning"},
            {3,"Error"},
            {5,"Invalid model"},
            {6,"Exception occured"},
            {7,"Null or empty reference"},
            {8,"Timeout error"},

            {10,"SQL error"},
            {11,"SQL update failed"},
            {12,"SQL insert failed"},
            {13,"SQL delete failed"},
            {14,"SQL delete failed"},
            {15,"SQL table create failed"},
            {16,"SQL table delete failed"},
            {17,"SQL table truncate failed"},
                        
            {20,"Error in logging"},
            {21,"Error in serializing"},
            {22,"Error in deserializing"},
            {23,"Error in creating log file or directory"},
                        
            {30,"Not exists"},            
            {31,"Already exists"},
            {32,"Already used"},
            {33,"Already in use"},
            {34,"Expired"},

            {40,"Authentication failed"},            
            {41,"Not logged in"},
            {42,"Token invalid"},            
            {43,"Password incorrect"},
            {44,"Username incorrect"},
            {45,"Email incorrect"},
            {46,"Mail already sent"},
            {47,"Mail failed to send"},
                        
            {50,"No online network connection"},
            {51,"Failed to connect"},
            {52,"Failed to get response"},
            {53,"Failed to read response"},
            {54,"Failed to write response"},
            {55,"Failed to send request"}

        };
    
    }

}
