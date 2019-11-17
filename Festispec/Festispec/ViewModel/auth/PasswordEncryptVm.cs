using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.auth
{
    class PasswordEncryptVM
    {
        public string ToPassword(string text)
        {
            var data = Encoding.ASCII.GetBytes(text);
            data = new SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }
    }
}
