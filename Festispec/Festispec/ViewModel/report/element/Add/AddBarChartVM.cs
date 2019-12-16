using Festispec.Message;
using Festispec.View.Pages.Report.element.Add;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddBarChartVM : BaseElementAdd
    {


        public AddBarChartVM(): base()
        {
            ReportElementVM = new BarChartVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message => { 
                if (message.NextPageType == typeof(AddBarChartPage))
                {
                    ReportElementVM = new BarChartVM();
                }
            });
        }
    }
}
