using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class TableQuestionType : BaseQuestionType
    {
        public override string PathToPartial { get; } = "Survey/TableQuestion";

        public TableQuestionType(QuestionDetails questionData) : base(questionData)
        {
        }

        public override PartialViewResult GetPartial()
        {
            return PartialView(PathToPartial, QuestionData);
        }
    }
}