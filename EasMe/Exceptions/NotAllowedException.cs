using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotAllowedException : Exception
    {
        public NotAllowedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotAllowedException(string message) : base(message)
        {

        }
    }
}
