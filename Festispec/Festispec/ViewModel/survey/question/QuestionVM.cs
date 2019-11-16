using Festispec.ViewModel.survey.answer;
using System.Collections.ObjectModel;

namespace Festispec.ViewModel.survey.question
{
    public class QuestionVM
    {
        private Domain.Question _question;

        public int Id => _question.Id;
        public ObservableCollection<SurveyAnswerVM> Answers { get; set; }

        public string Question
        {
            get => _question.Question1;
            set => _question.Question1 = value;
        }

        public int Order
        { 
            get => _question.Order;
            set => _question.Order = value;
        }

        public string Type
        { 
            get => _question.Type;
            set => _question.Type = value;
        }

        public string Variable
        { 
            get => _question.Variables;
            set => _question.Variables = value;
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
