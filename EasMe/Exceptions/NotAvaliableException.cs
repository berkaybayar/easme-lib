using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class NotAvaliableException : Exception
    {
        public NotAvaliableException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public NotAvaliableException(string message) : base(message)
        {

        }
    }
}
