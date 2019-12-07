using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions.Types
{
    public class ClosedQuestionType : IQuestion
    {
        public int Id { get; set; }
        public QuestionDetails Details { get; set; }
        public string Variable { get; set; }
    }
}