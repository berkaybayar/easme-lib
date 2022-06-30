using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NullReferenceException : Exception
    {
        public NullReferenceException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NullReferenceException(string message) : base(message)
        {

        }
    }
}
