using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotReadyException : Exception
    {
        public NotReadyException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotReadyException(string message) : base(message)
        {

        }
    }
}
