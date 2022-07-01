using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToDeserializeException : Exception
    {
        public FailedToDeserializeException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToDeserializeException(string message) : base(message)
        {

        }
    }
}
