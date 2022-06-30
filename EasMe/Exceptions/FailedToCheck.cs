using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToCheck : Exception
    {
        public FailedToCheck(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToCheck(string message) : base(message)
        {

        }
    }
}
