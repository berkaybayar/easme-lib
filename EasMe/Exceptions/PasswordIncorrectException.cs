using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class PasswordIncorrectException : Exception
    {
        public PasswordIncorrectException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public PasswordIncorrectException(string message) : base(message)
        {

        }
    }
}
