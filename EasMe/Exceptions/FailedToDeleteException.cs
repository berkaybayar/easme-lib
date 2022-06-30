using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToDeleteException : Exception
    {
        public FailedToDeleteException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToDeleteException(string message) : base(message)
        {

        }
    }
}
