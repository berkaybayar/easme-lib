using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public InvalidTokenException(string message) : base(message)
        {

        }
    }
}
