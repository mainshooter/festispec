using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions.Types
{
    public class MultipleChoiseQuestionType : IQuestion
    {
        public int Id { get; set; }
        public QuestionDetails Details { get; set; }
        public string PathToPartial { get; } = "Survey/MultipleChoiseQuestion";
        public int AnswerValue { get; set ; }
        public int AnswerText { get; set; }
        public string Variable { get; set; }
    }
}