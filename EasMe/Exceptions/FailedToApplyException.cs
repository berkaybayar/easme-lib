using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToApplyException : Exception
    {
        public FailedToApplyException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToApplyException(string message) : base(message)
        {

        }
    }
}
