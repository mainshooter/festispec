using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using Festispec.Lib.Auth;
using Festispec.Message;
using Festispec.View.Pages.PasswordReset;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.auth
{
    public class RequestPasswordResetVm : ViewModelBase
    {
        public ICommand DoRequestReset { get; set; }
        public string Email { get; set; }

        public RequestPasswordResetVm()
        {
            DoRequestReset = new RelayCommand(RequestReset);
        }

        private void RequestReset()
        {
            if (PasswordResetService.AccountExists(Email))
            {
               var code = PasswordResetService.GenerateResetCode();
               PasswordResetService.SaveResetCodeFor(Email, code); 
               PasswordResetService.SendEmailWithResetCode(Email, code);

               MessageBox.Show("Er is een email vorzonden naar het ingevoerde emailadres.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
               MessengerInstance.Send(new ChangePageMessage() { NextPageType = typeof(VerifyCodePage) });
            }
            else
            { 
                MessageBox.Show("Er is geen gebruiker gevonden met het ingevoerde e-mailadres.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
