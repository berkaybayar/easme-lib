using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    public class EasException : Exception
    {
        public EasException(Error error, string message, Exception? Inner = null) : base(error.ToString() + " : " + message, Inner)
        {
        }
        
        public EasException(Error error) : base(error.ToString())
        {

        }
        public EasException(Error error,Exception Inner) : base(error.ToString(),Inner)
        {

        }
        public EasException(Error error,Error error2) : base(error.ToString() + " | " + error2.ToString())
        {

        }
        public EasException(Error error, Error error2, string message) : base(error.ToString() + " | " + error2.ToString() + " : " + message)
        {

        }
        public EasException(Error error, Error error2,Exception Inner) : base(error.ToString() + " | " + error2.ToString(), Inner)
        {

        }
        public EasException(Error error, Error error2, string message, Exception Inner) : base(error.ToString() + " | " + error2.ToString() + " : " + message, Inner)
        {

        }
    }
}
