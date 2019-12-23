using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditTableVM : BaseElementEdit
    {
        public EditTableVM() : base()
        {
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                if (message.ReportElementVM.Type == ReportElementType.Table)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
