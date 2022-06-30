using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NoOnlineNetworkException : Exception
    {
        public NoOnlineNetworkException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NoOnlineNetworkException(string message) : base(message)
        {

        }
    }
}
