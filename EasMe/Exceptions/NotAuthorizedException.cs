using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotAuthorizedException(string message) : base(message)
        {

        }
    }
}
