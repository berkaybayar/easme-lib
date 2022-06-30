using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ApiSendFailedToPatchException : Exception
    {
        public ApiSendFailedToPatchException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToPatchException(string message) : base(message)
        {

        }
    }
}
