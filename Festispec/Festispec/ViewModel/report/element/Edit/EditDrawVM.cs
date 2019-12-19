using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditDrawVM : BaseElementEdit
    {
        public EditDrawVM()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Barchart;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message =>
            {
                ReportElementVM = message.ReportElementVM;
            });
        }
    }
}
