using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Auth;
using Festispec.Message;
using Festispec.View.Pages.Employee;
using Festispec.View.Pages.PasswordReset;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.auth
{
    public class ResetPasswordVm : ViewModelBase
    {
        public string Email { get; set; }
        public ICommand DoReset { get; set; }

        public ResetPasswordVm()
        {
            DoReset = new RelayCommand<PasswordBox>(Reset);
        }

        private void Reset(PasswordBox password)
        {
            var result = PasswordResetService.UpdatePasswordFor(Email, password.Password);

            if (result)
            {
                MessageBox.Show("Wachtwoord bijgewerkt.", "Gelukt", MessageBoxButton.OK, MessageBoxImage.Information);
                MessengerInstance.Send(new ChangePageMessage() { NextPageType = typeof(LoginPage) });
            }
            else
            {
                MessageBox.Show("Fout bij het bijwerken van het wachtwoord.", "Oeps", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
