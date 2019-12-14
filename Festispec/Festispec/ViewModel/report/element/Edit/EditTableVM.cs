using Festispec.Domain;
using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Data.Entity;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditTableVM : BaseElementEdit
    {
        public ICommand SaveElementCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public EditTableVM() : base()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Table;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => {
                ReportElementVM = message.ReportElementVM;
            });

            SaveElementCommand = new RelayCommand(EditElement, CanAddElement);
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
            CloseEditElement();
        }

        public void CloseEditElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }
        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }
    }
}
