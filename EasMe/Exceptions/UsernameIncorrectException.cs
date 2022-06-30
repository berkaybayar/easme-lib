using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class UsernameIncorrectException : Exception
    {
        public UsernameIncorrectException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public UsernameIncorrectException(string message) : base(message)
        {

        }
    }
}
