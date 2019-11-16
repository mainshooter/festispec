using Festispec.ViewModel.survey.answer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey.question
{
    public class QuestionVM
    {
        private Domain.Question _question;
        public int Id {
            get {
                return _question.Id;
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
        public string Type { 
            get {
                return _question.Type;
            }
            set {
                _question.Type = value;
            }
        }
        public ObservableCollection<SurveyAnswerVM> Answers { get; set; }
        public string Variable { 
            get {
                return _question.Variables;
            }
            set {
                _question.Variables = value;
            }
        }

        public QuestionVM()
        {
            _question = new Domain.Question();
        }

        public QuestionVM(Domain.Question question)
        {
            _question = question;
        }

        public Domain.Question ToModel()
        {
            return _question;
        }
    }
}
