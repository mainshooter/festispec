using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Exceptions.Auth
{
    public class PasswordNoMatchException : Exception
    {
        public PasswordNoMatchException()
        {
        }

        public PasswordNoMatchException(string message) : base(message)
        {
        }

        public PasswordNoMatchException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
