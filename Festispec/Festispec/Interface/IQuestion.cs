using System.Windows.Input;
using Festispec.Survey.Question;

namespace Festispec.ViewModel.survey.question.questionTypes
{
    public interface IQuestion
    {
        QuestionDetails QuestionDetails { get; set; }
        string QuestionType { get; }
        ICommand SaveCommand { get; set; }
        ICommand GoBackCommand { get; set; }
        void Save();
        void Delete();
        void GoBack();
        void Refresh();
    }
}
