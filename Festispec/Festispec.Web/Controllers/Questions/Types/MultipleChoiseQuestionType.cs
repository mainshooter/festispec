using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class MultipleChoiseQuestionType : BaseQuestionType
    {
        public override string PathToPartial { get; } = "Survey/MultipleChoiseQuestion";

        public MultipleChoiseQuestionType(QuestionDetails questionData) : base(questionData)
        {
        }

        public override PartialViewResult GetPartial()
        {
            return PartialView(PathToPartial, QuestionData);
        }
    }
}