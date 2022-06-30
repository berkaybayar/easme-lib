using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToCreateException : Exception
    {
        public FailedToCreateException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToCreateException(string message) : base(message)
        {

        }
    }
}
