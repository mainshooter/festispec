﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Festispec.Lib.Auth;
using Festispec.Message;
using Festispec.View.Pages.Employee;
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
            if (password.Password.Length > 999999 || password.Password.Length <= 0)
            {
                MessageBox.Show("Wachtwoord is te lang of te kort.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
