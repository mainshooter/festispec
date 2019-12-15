using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.answer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class DataVM: ViewModelBase
    {
        private IQuestion _question;
        public virtual string Type { get; set; }

        public IQuestion Question { 
            get {
                return _question;
            }
            set {
                _question = value;
                RaisePropertyChanged("Question");
            }
        }

        [PreferredConstructor]
        public DataVM()
        {
        }

        public DataVM(string json)
        {
            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            Type = dic["Type"];
        }

        public string ToJson()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Type", Type);
            dic.Add("QuestionId", Question.Id.ToString());
            return JsonConvert.SerializeObject(dic);
        }

        protected List<SurveyAnswerVM> GetQuestionAnswers()
        {
            var answers = new List<SurveyAnswerVM>();
            using (var context = new Entities())
            {
                var dbResult = context.Answers.Where(answer => answer.QuestionId.Equals(Question.Id)).ToList();
                answers = new List<SurveyAnswerVM>(dbResult.Select(answer => new SurveyAnswerVM(answer)).ToList());
            }
            return answers;
        }
    }
}
