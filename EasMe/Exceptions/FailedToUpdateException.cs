using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToUpdateException : Exception
    {
        public FailedToUpdateException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToUpdateException(string message) : base(message)
        {

        }
    }
}
