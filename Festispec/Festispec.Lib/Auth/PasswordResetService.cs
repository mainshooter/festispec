using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Festispec.Domain;

namespace Festispec.Lib.Auth
{
    public class PasswordResetService
    {
        public static string GenerateResetCode()
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvqxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool SaveResetCodeFor(string email, string code)
        {
            using (var context = new Entities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Email == email);

                if (employee == null)
                    return false;

                employee.PasswordResetToken = code;
                employee.ResetTokenEndTime = DateTime.Now.AddDays(1);
                context.SaveChanges();
                return true;
            }
        }

        public static void SendEmailWithResetCode(string email, string resetCode)
        {
                var mail = new MailMessage();
                var smtp = new SmtpClient("smtp.mailtrap.io");

                mail.From = new MailAddress("noreply@festispec.nl");
                mail.To.Add(email);
                mail.Subject = "Request for password reset";
                mail.Body = "Your resetcode is: " + resetCode;

                smtp.Port = 2525;
                smtp.Credentials = new NetworkCredential("fffce1440f7eb2", "cbaaaf6b397a4a");
                smtp.EnableSsl = true;
                smtp.Send(mail);
        }

        public static bool ValidateResetCode(string resetCode)
        {
            using (var context = new Entities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.PasswordResetToken == resetCode);
                return employee != null && !(employee.ResetTokenEndTime < DateTime.Now);
            }
        }

        public static bool UpdatePasswordFor(string email, string password)
        {
            using (var context = new Entities())
            {
                var user = context.Employees.FirstOrDefault(e => e.Email == email);

                if (user == null)
                    return false;

                var passwordService = new PasswordHashService();

                user.Password = passwordService.StringToPassword(password);
                user.ResetTokenEndTime = null;
                user.PasswordResetToken = null;
                context.SaveChanges();

                return true;
            }
        }

        public static bool AccountExists(string email)
        {
            using (var context = new Entities())
            {
                var user = context.Employees.FirstOrDefault(e => e.Email == email);
                return user != null;
            }
        }
    }
}
