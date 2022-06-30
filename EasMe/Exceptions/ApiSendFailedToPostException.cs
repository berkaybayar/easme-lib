using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ApiSendFailedToPostException : Exception
    {
        public ApiSendFailedToPostException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToPostException(string message) : base(message)
        {

        }
    }
}
