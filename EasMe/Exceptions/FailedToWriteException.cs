using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToWriteException : Exception
    {
        public FailedToWriteException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToWriteException(string message) : base(message)
        {

        }
    }
}
