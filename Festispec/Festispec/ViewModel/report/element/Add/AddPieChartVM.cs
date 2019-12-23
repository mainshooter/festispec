using Festispec.Message;
using Festispec.View.Pages.Report.element.Add;
using System.Linq;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddPieChartVM : BaseElementAdd
    {
        public AddPieChartVM() : base()
        {
            ReportElementVM = new PieChartVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message => 
            {
                if (message.NextPageType == typeof(AddPieChartPage))
                {
                    ReportElementVM = new PieChartVM();
                    ReportElementVM.DataParser = DataParsers.First();
                    ReportElementVM.SelectedSurveyQuestion = SurveyQuestions.First();
                }
            });
        }
    }
}
