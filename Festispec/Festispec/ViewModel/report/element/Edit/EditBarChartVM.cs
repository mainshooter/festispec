using Festispec.Domain;
using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.CommandWpf;
using System.Data.Entity;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditBarChartVM : BaseElementEdit
    {
        public ICommand SaveElementCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public EditBarChartVM(): base()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Barchart;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => {
                ReportElementVM = message.ReportElementVM;
            });

            SaveElementCommand = new RelayCommand(EditElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseEditElement);
        }

        public void EditElement()
        {
            ToastVM toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            if (CanUseOptions())
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Attach(this.ReportElementVM.ToModel());
                    context.Entry(ReportElementVM.ToModel()).State = EntityState.Modified;
                    context.SaveChanges();
                }
                toast.ShowInformation("Rapportelement bijgewerkt.");
                CloseEditElement();
            }
            else
            {
                toast.ShowError("Deze query kan niet met dit element en/of vraag word niet ondersteunt door deze query");
            }
        }

        public void CloseEditElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }

        private bool CanUseOptions()
        {
            if (ReportElementVM.DataParser != null && ReportElementVM.DataParser.QuestionTypeIsSupported)
            {
                var dataParser = ReportElementVM.DataParser;
                bool containsSupportedType = dataParser.SupportedVisuals.Contains(ReportElementVM.Type);
                if (containsSupportedType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
