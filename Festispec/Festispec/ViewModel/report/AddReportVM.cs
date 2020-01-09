using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.auth;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddReportVM : ViewModelBase
    {
        private string _role = UserSessionVM.Current.Employee.Department.Name;
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

        public ICommand BackCommand { get; set; }

        public AddReportVM()
        {
            MessengerInstance.Register<ChangeSelectedOrderMessage>(this, message => 
            {
                ReportVM = new ReportVM(message.SelectedOrderVM);
            });
            

            SaveEditCommand = new RelayCommand(SaveEdit);
            BackCommand = new RelayCommand(Back);
        }

        private bool Save()
        {
            if (ReportVM == null)
            {
                return false;
            }
            using (var context = new Entities())
            {
                if (!ReportVM.ValidateInputs()) return false;
                ReportVM.Status = "Concept";
                context.Reports.Add(ReportVM.ToModel());
                context.SaveChanges();
                ReportVM.Order.Report = ReportVM;
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
            if (_role == "Directie" || _role == "Sales")
            {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
                MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage() { NextReportVM = _reportVM });
            }
            Back();
        }
    }
}
