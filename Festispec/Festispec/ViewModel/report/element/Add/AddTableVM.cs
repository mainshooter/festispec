using Festispec.Message;
using Festispec.View.Pages.Report.element.Add;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddTableVM : BaseElementAdd
    {
        public AddTableVM() : base()
        {
            ReportElementVM = new TableVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message => 
            {
                if (message.NextPageType == typeof(AddTablePage))
                {
                    ReportElementVM = new TableVM();
                }
            });
        }
    }
}
