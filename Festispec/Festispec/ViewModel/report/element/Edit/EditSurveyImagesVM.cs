using Festispec.Lib.Enums;
using Festispec.Message;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditSurveyImagesVM : BaseElementEdit
    {
        public EditSurveyImagesVM()
        {
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message =>
            {
                if (message.ReportElementVM.Type == ReportElementType.SurveyImages)
                {
                    ReportElementVM = message.ReportElementVM;
                }
            });
        }
    }
}
