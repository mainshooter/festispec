using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditPieChartVM : BaseElementEdit
    {
        public EditPieChartVM(): base()
        {
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                if (message.ReportElementVM.Type == ReportElementType.Piechart)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
