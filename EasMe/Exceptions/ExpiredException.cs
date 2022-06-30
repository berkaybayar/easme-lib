using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ExpiredException : Exception
    {
        public ExpiredException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ExpiredException(string message) : base(message)
        {

        }
    }
}
