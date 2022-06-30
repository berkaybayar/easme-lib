using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToCopyException : Exception
    {
        public FailedToCopyException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToCopyException(string message) : base(message)
        {

        }
    }
}
