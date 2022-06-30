using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotValidException : Exception
    {
        public NotValidException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotValidException(string message) : base(message)
        {

        }
    }
}
