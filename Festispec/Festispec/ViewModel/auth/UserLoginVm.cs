using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Auth;
using Festispec.Message;
using Festispec.View.Pages;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.auth
{
    public class UserLoginVM : ViewModelBase
    {
        public string Email { get; set; }
        public ICommand DoLogin { get; set; }

        public UserLoginVM()
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
                    userSession.Employee = new EmployeeVM(employee);
                    //Vanuit hier kun je doorverwijzen naar een andere pagina oid
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(DashboardPage) });
                    MessengerInstance.Send<ChangeLoggedinUserMessage>(new ChangeLoggedinUserMessage() { LoggedinEmployee = userSession.Employee});
                }
            }
        }
    }
}
