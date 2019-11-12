using System.Windows.Input;
using Festispec.Domain;
using Festispec.Survey.Question;

namespace Festispec.ViewModel.survey.question.questionTypes
{
    public interface IQuestion
    {
        MainViewModel MainViewModel { get; set; }
        QuestionDetails QuestionDetails { get; set; }
        string QuestionType { get; }
        ICommand SaveCommand { get; set; }
        ICommand GoBackCommand { get; set; }
        void Save();
        void GoBack();
        bool ValidateQuestionDetails();
        Question ToModel();
    }
}
