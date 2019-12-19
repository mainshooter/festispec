using Festispec.Lib.Enums;
using Festispec.Message;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditTextVM : BaseElementEdit
    {
        public EditTextVM()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Text;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                ReportElementVM = message.ReportElementVM;
            });
        }
    }
}
