using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Festispec.Lib.Auth
{
    class PasswordService
    {
        public string StringToPassword(string text)
        {
            var data = Encoding.ASCII.GetBytes(text);
            data = new SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }

        public bool PasswordsCompare(string stringUnHashed, string hashed)
        {
            return hashed == StringToPassword(stringUnHashed);
        }
    }
}
