using System;
using System.Collections.Generic;
using System.Text;

namespace Festispec.Lib.Interfaces
{
    interface IPasswordValidator
    {
        string StringToPassword(string toPassword);
        bool PasswordsCompare(string unHashed, string hashed);
    }
}
