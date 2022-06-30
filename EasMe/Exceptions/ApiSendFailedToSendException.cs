using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ApiSendFailedToSendException : Exception
    {
        public ApiSendFailedToSendException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToSendException(string message) : base(message)
        {

        }
    }
}
