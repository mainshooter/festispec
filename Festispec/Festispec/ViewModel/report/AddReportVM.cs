using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Report;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddReportVM : ViewModelBase
    {
        private ReportVM _reportVM;

        public ReportVM ReportVM
        {
            get => _reportVM;
            set
            {
                _reportVM = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SaveEditCommand { get; set; }
        public ICommand SaveBackCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public AddReportVM()
        {
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                ReportVM = message.NextReportVM;
            });

            SaveEditCommand = new RelayCommand(SaveEdit);
            SaveBackCommand = new RelayCommand(SaveBack);
            BackCommand = new RelayCommand(Back);
        }

        private bool Save()
        {
            using (var context = new Entities())
            {
                if (!ReportVM.ValidateInputs()) return false;

                ReportVM.Status = "Concept";
                ReportVM.ToModel().OrderId = ReportVM.Order.Id;
                context.Reports.Add(ReportVM.ToModel());
                context.SaveChanges();
                return true;
            }
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        private void SaveEdit()
        {
            if (!Save()) return;
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage() { NextReportVM = _reportVM });
        }

        private void SaveBack()
        {
            if (!Save()) return;
            Back();
        }
    }
}
