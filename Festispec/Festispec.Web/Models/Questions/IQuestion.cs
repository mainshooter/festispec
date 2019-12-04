using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions
{
    public interface IQuestion
    {
        int Id { get; set; }
        QuestionDetails Details { get; set; }
        int AnswerValue { get; set; }
        int AnswerText { get; set; }
    }
}
