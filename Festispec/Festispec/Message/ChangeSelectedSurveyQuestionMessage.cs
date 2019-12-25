using Festispec.Interface;
using Festispec.ViewModel.survey;

namespace Festispec.Message
{
    public class ChangeSelectedSurveyQuestionMessage
    {
        public IQuestion NextQuestion { get; set; }
        public SurveyVM SurveyVM { get; set; }
    }
}
