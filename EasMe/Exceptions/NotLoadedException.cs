using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotLoadedException : Exception
    {
        public NotLoadedException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotLoadedException(string message) : base(message)
        {

        }
    }
}
