﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Exceptions
{
    public class FailedToRemoveException : Exception
    {
        public FailedToRemoveException(string message, Exception? Inner = null) : base(message, Inner)
        {

        }
        public FailedToRemoveException(string message) : base(message)
        {

        }
    }
}