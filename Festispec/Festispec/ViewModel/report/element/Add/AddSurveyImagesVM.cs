using Festispec.Message;
using Festispec.View.Pages.Report.element.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddSurveyImagesVM : BaseElementAdd
    {
        public AddSurveyImagesVM()
        {
            ReportElementVM = new SurveyImageVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(AddSurveyImagesPage))
                {
                    ReportElementVM = new SurveyImageVM();
                    ReportElementVM.DataParser = DataParsers.First();
                    ReportElementVM.SelectedSurveyQuestion = SurveyQuestions.First();
                }
            });
        }
    }
}
