using Festispec.Domain;
using Festispec.Lib.Survey.Question;

namespace Festispec.Interface
{
    public interface IQuestion
    {
        int Id { get; }
        string Question { get; set; }
        QuestionDetails QuestionDetails { get; set; }
        string QuestionType { get; }
        bool ValidateQuestionDetails();
        Question ToModel();
    }
}
