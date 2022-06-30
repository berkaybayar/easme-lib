using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotEnabledException : Exception
    {
        public NotEnabledException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotEnabledException(string message) : base(message)
        {

        }
    }
}
