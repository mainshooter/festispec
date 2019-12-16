using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditTableVM : BaseElementEdit
    {
        public EditTableVM() : base()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Table;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => {
                ReportElementVM = message.ReportElementVM;
            });
        }
    }
}
