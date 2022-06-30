using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToReadException : Exception
    {
        public FailedToReadException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToReadException(string message) : base(message)
        {

        }
    }
}
