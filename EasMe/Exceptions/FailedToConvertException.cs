using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToConvertException : Exception
    {
        public FailedToConvertException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToConvertException(string message) : base(message)
        {

        }
    }
}
