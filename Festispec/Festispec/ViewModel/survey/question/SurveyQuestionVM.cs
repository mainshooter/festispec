using Festispec.Domain;
using Festispec.ViewModel.survey.question.questionTypes;

namespace Festispec.ViewModel.survey.question
{
    public class SurveyQuestionVM
    {
        private Question _question;
        private IQuestion _questionType;

        public int Id {
            get {
                return _question.Id;
            }
            private set {
                _question.Id = value;
            }
        }

        public string Type
        {
            get
            {
                return _question.Type;
            }
            set
            {
                _question.Type = value;
            }
        }

        public string Question {
            get {
                return _question.Question1;
            }
            set {
                _question.Question1 = value;
            }
        }

        public int Order {
            get {
                return _question.Order;
            }
            set {
                _question.Order = value;
            }
        }

        public string Variable {
            get {
                return _question.Variables;
            }
            set {
                _question.Variables = value;
            }
        }

        public SurveyQuestionVM(Question question)
        {
            _question = question;
            Type = question.Type;
        }

        public SurveyQuestionVM(string type)
        {
            _question = new Question();
            Type = type;
        }
    }
}
