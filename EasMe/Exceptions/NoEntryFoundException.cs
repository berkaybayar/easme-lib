using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NoEntryFoundException : Exception
    {
        public NoEntryFoundException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NoEntryFoundException(string message) : base(message)
        {

        }
    }
}
