using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditLineChartVM : BaseElementEdit
    {

        public EditLineChartVM(): base()
        {
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                if (message.ReportElementVM.Type == ReportElementType.Linechart)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
