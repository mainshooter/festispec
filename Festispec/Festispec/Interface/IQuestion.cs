using System.Windows.Input;
using Festispec.Domain;
using Festispec.Lib.Survey.Question;
using Festispec.ViewModel;

namespace Festispec.Interface
{
    public interface IQuestion
    {
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
