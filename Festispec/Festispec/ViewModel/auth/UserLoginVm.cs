using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Auth;
using Festispec.Message;
using Festispec.View.Pages;
using Festispec.View.Pages.PasswordReset;
using Festispec.View.Pages.Customer.Event;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.auth
{
    public class UserLoginVM : ViewModelBase
    {
        public string Email { get; set; }
        public ICommand DoLogin { get; set; }
        public ICommand GotoResetPassword { get; set; }
        public ICommand OfflineCommand { get; set; }

        public UserLoginVM()
        {
            DoLogin = new RelayCommand<PasswordBox>(Login);
            GotoResetPassword = new RelayCommand(ToResetPassword);
            OfflineCommand = new RelayCommand(ShowOffline);
        }

        public void Login(PasswordBox passwordBox)
        {
            var password = passwordBox.Password;

            using (var context = new Entities())
            {
                var employee = context.Employees.FirstOrDefault(e => e.Email == Email);
                var passwordService = new PasswordHashService();

                if (employee == null)
                {
                    MessageBox.Show("Er is geen gebruiker gevonden met het ingevoerde emailadres.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (!passwordService.PasswordsCompare(password, employee.Password))
                {
                    MessageBox.Show("Ongeldig wachtwoord.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    var userSession = UserSessionVm.Current;
                    userSession.Employee = new EmployeeVM(employee);
                    //Vanuit hier kun je doorverwijzen naar een andere pagina oid
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(DashboardPage) });
                    MessengerInstance.Send<ChangeLoggedinUserMessage>(new ChangeLoggedinUserMessage() { LoggedinEmployee = userSession.Employee });
                }
            }
        }

        private void ToResetPassword()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(RequestPasswordPage) });
        }
        private void ShowOffline()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(OfflineEventListPage) });
        }
    }
}
