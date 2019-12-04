using System;
using System.Linq;
using Festispec.Domain;

namespace Festispec.Lib.Auth
{
    class PasswordResetService
    {
        public void GenerateResetCodeFor(string email)
        {
             var resetCode = $"{new string('*', 8).GetHashCode():X}";

             using (var context = new Entities())
             {
                 var employee = context.Employees.FirstOrDefault(e => e.Email == email);
                 employee.PasswordResetToken = resetCode;
                 context.SaveChanges();
             }
        }

        public void SendEmailWithResetCode(string email, string resetCode)
        {
            throw new NotImplementedException();
        }

        public bool ValidateResetCode(string resetCode)
        {
            throw new NotImplementedException();
        }

        public void UpdatePasswordFor(string email)
        {
            throw new NotImplementedException();
        }
    }
}
