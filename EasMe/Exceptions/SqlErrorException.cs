using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class SqlErrorException : Exception
    {
        public SqlErrorException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public SqlErrorException(string message) : base(message)
        {

        }
    }
}
