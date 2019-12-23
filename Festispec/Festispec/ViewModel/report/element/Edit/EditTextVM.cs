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
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                if (message.ReportElementVM.Type == ReportElementType.Text)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
