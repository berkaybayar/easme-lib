using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToValidateException : Exception
    {
        public FailedToValidateException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToValidateException(string message) : base(message)
        {

        }
    }
}
