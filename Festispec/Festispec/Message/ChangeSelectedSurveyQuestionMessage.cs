using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Message
{
    public class ChangeSelectedSurveyQuestionMessage
    {
        public IQuestion NextQuestion { get; set; }
        public SurveyVM SurveyVM { get; set; }
    }
}
