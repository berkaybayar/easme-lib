using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class SqlExceptionException : Exception
    {
        public SqlExceptionException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public SqlExceptionException(string message) : base(message)
        {

        }
    }
}
