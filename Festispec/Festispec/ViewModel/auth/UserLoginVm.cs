using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Domain;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.auth
{
    public class UserLoginVm
    {
        public string Email { get; set; }
        public ICommand DoLogin { get; set; }

        public UserLoginVm()
        {
            DoLogin = new RelayCommand<PasswordBox>(Login);
        }

        public void Login(PasswordBox passwordBox) 
        {
            var password = passwordBox.Password;

            using (var context = new Entities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Email == Email);

                if (employee != null && PasswordsCompare(password, employee.Password))
                {
                    Application.Current.Resources["session"] = new SessionVm(employee);
                    //Vanuit hier kun je doorverwijzen naar een andere pagina oid
                }
                else
                {
                    MessageBox.Show("Invalid password.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool PasswordsCompare(string pwUnHashed, string pwHashed)
        {
            return pwUnHashed.Equals(pwHashed);
        }
    }
}
