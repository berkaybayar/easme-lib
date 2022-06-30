using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class LoggingFailedException : Exception
    {
        public LoggingFailedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public LoggingFailedException(string message) : base(message)
        {

        }
    }
}
