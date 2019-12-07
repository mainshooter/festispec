using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions
{
    public interface IQuestion
    {
        int Id { get; set; }
        string Variable { get; set; }
        QuestionDetails Details { get; set; }
    }
}
