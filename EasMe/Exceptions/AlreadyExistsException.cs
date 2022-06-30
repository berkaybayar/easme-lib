using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public AlreadyExistsException(string message) : base(message)
        {

        }
    }
}
