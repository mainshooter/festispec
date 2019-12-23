using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditBarChartVM : BaseElementEdit
    {
        public EditBarChartVM(): base()
        {
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                if (message.ReportElementVM.Type == ReportElementType.Barchart)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
