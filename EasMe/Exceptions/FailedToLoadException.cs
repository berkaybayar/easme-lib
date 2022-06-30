using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToLoadException : Exception
    {
        public FailedToLoadException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToLoadException(string message) : base(message)
        {

        }
    }
}
