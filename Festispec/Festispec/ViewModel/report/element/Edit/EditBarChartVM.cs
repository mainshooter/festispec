using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditBarChartVM : BaseElementEdit
    {
        public EditBarChartVM(): base()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Barchart;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => {
                ReportElementVM = message.ReportElementVM;
            });
        }
    }
}
