using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class AlreadyInUseException : Exception
    {
        public AlreadyInUseException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public AlreadyInUseException(string message) : base(message)
        {

        }
    }
}
