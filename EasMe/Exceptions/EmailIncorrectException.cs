using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class EmailIncorrectException : Exception
    {
        public EmailIncorrectException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public EmailIncorrectException(string message) : base(message)
        {

        }
    }
}
