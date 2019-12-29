using System.Windows;
using System.Windows.Input;
using Festispec.Lib.Auth;
using Festispec.Message;
using Festispec.View.Pages.PasswordReset;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.auth
{
    public class VerifyCodeVm : ViewModelBase
    {
        public ICommand DoVerify { get; set; }
        public string Code { get; set; }

        public VerifyCodeVm()
        {
            DoVerify = new RelayCommand(Verify);
        }

        private void Verify()
        {
            if (PasswordResetService.ValidateResetCode(Code))
            {
                MessengerInstance.Send(new ChangePageMessage() { NextPageType = typeof(ResetPasswordPage) });
            }
            else
            {
                MessageBox.Show("Code ongeldig.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
