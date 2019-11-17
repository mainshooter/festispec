using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.auth;
using Festispec.View;
using Festispec.Lib.Auth;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.auth
{
    public class UserLoginVm : ViewModelBase
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
                var passwordService = new PasswordService();

                if (employee == null)
                {
                    MessageBox.Show("Er is geen gebruiker gevonden met de ingevoerde email.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Information);
                } else if (!passwordService.PasswordsCompare(password, employee.Password))
                {
                    MessageBox.Show("Ongeldig wachtwoord.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var userSession = UserSession.Current;
                    userSession.Employee = employee;
                    MessageBox.Show("Login succesvol", "Geslaagd", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Vanuit hier kun je doorverwijzen naar een andere pagina oid
                }
            }
        }
    }
}
