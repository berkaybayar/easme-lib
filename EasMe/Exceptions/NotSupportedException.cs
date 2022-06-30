using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotSupportedException : Exception
    {
        public NotSupportedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotSupportedException(string message) : base(message)
        {

        }
    }
}
