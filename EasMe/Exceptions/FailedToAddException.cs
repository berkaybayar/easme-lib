using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToAddException : Exception
    {
        public FailedToAddException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToAddException(string message) : base(message)
        {

        }
    }
}
