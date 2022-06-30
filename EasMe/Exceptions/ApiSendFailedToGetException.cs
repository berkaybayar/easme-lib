using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ApiSendFailedToGetException : Exception
    {
        public ApiSendFailedToGetException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToGetException(string message) : base(message)
        {

        }
    }
}
