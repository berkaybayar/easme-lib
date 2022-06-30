using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotAuthenticatedException(string message) : base(message)
        {

        }
    }
}
