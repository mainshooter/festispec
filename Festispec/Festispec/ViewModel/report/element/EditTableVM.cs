using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class EditTableVM : ViewModelBase
    {
        private ReportElementVM _reportElementVM;
        public ReportVM ReportVM { get; set; }

        public ReportElementVM ReportElementVM
        {
            get
            {
                return _reportElementVM;
            }
            set
            {
                _reportElementVM = value;
                RaisePropertyChanged("ReportElementVM");
            }
        }

        public ICommand SaveElementCommand { get; set; }
        public ICommand ReturnCommand { get; set; }
        public EditTableVM()
        {
            this.MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM = message.ReportElement;
                this.ReportVM = message.NextReportVM;
            });

            SaveElementCommand = new RelayCommand(EditElement);
            ReturnCommand = new RelayCommand(CloseEditElement);
        }
        public void EditElement()
        {
            using (var context = new Entities())
            {
                context.ReportElements.Attach(this.ReportElementVM.ToModel());
                context.Entry(ReportElementVM.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation("Rapportelement bijgewerkt.");
            ReportVM.RefreshElements();
            CloseEditElement();
        }
        public void CloseEditElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });

        }
    }
}
