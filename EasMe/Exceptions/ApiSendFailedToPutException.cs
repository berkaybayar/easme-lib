using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ApiSendFailedToPutException : Exception
    {
        public ApiSendFailedToPutException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToPutException(string message) : base(message)
        {

        }
    }
}
