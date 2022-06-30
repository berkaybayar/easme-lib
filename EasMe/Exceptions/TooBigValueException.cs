using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class TooBigValueException : Exception
    {
        public TooBigValueException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public TooBigValueException(string message) : base(message)
        {

        }
    }
}
