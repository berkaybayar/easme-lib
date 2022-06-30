using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class DeserializationFailedException : Exception
    {
        public DeserializationFailedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public DeserializationFailedException(string message) : base(message)
        {

        }
    }
}
