using Festispec.Domain;
using Festispec.Lib.Survey.Question;

namespace Festispec.Interface
{
    public interface IQuestion
    {
        QuestionDetails QuestionDetails { get; set; }
        string QuestionType { get; }
        bool ValidateQuestionDetails();
        Question ToModel();
    }
}
