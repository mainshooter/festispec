using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Festispec.Lib.Interfaces;

namespace Festispec.Lib.Auth
{
    public class PasswordService : IPasswordValidator
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
