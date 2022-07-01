using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToSerializeException : Exception
    {
        public FailedToSerializeException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToSerializeException(string message) : base(message)
        {

        }
    }
}
