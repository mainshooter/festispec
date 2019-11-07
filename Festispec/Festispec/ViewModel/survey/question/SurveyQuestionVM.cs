using Festispec.Domain;
using Festispec.ViewModel.survey.question.questionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey.question
{
    public class SurveyQuestionVM
    {
        private Question _question;
        public SurveyQuestionVM(Question question)
        {
            _question = question;
        }

        public SurveyQuestionVM()
        {
            _question = new Question();
        }

        public int Id {
            get {
                return _question.Id;
            }
            private set {
                _question.Id = value;
            }
        }

        public IQuestion QuestionType { get; set; }

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
    }
}
