using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditDrawVM : BaseElementEdit
    {
        public EditDrawVM()
        {
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message =>
            {
                if (message.ReportElementVM.Type == ReportElementType.Draw)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
