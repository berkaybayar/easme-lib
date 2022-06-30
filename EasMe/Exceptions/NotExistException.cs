using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotExistException : Exception
    {
        public NotExistException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotExistException(string message) : base(message)
        {

        }
    }
}
