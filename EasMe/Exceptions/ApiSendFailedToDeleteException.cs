using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class ApiSendFailedToDeleteException : Exception
    {
        public ApiSendFailedToDeleteException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public ApiSendFailedToDeleteException(string message) : base(message)
        {

        }
    }
}
