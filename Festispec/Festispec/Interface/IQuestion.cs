using System.Windows.Input;
using Festispec.Domain;
using Festispec.Survey.Question;

namespace Festispec.ViewModel.survey.question.questionTypes
{
    public interface IQuestion
    {
        MainViewModel MainViewModel { get; set; }
        QuestionDetails QuestionDetails { get; set; }
        ICommand SaveCommand { get; set; }
        ICommand GoBackCommand { get; set; }
        int Id { get; }
        string QuestionType { get; }
        string Question { get; set; }
        int Order { get; set; }
        void Save();
        void GoBack();
        bool ValidateQuestionDetails();
        Question ToModel();
    }
}
