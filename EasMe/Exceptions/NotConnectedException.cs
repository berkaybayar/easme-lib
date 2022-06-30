using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotConnectedException : Exception
    {
        public NotConnectedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotConnectedException(string message) : base(message)
        {

        }
    }
}
