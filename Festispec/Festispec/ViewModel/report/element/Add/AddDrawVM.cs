using Festispec.Message;
using Festispec.View.Pages.Report.element.Add;
using System.Linq;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddDrawVM : BaseElementAdd
    {
        public AddDrawVM(): base()
        {
            ReportElementVM = new DrawVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddDrawPage))
                {
                    ReportElementVM = new DrawVM();
                    ReportElementVM.DataParser = DataParsers.First();
                    ReportElementVM.SelectedSurveyQuestion = SurveyQuestions.First();
                }
            });
        }
    }
}
