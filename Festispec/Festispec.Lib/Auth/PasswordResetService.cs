using System;
using System.Linq;
using Festispec.Domain;

namespace Festispec.Lib.Auth
{
    class PasswordResetService
    {
        public bool GenerateResetCodeFor(string email)
        {
             var resetCode = $"{new string('*', 8).GetHashCode():X}";

             using (var context = new Entities())
             {
                 var employee = context.Employees.FirstOrDefault(e => e.Email == email);

                 if (employee == null)
                     return false;

                 employee.PasswordResetToken = resetCode;
                 context.SaveChanges();
                 return true;
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

        public bool UpdatePasswordFor(string email, string newPassword)
        {
            using(var context = new Entities())
            {
                var user = context.Employees.FirstOrDefault(e => e.Email == email);

                if (user == null) 
                    return false;
                
                var passwordService = new PasswordHashService();

                user.Password = passwordService.StringToPassword(newPassword);
                context.SaveChanges();

                return true;
            }
        }
    }
}
