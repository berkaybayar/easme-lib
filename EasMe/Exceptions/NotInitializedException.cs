using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotInitializedException : Exception
    {
        public NotInitializedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotInitializedException(string message) : base(message)
        {

        }
    }
}
