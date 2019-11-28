using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class OpenQuestionType : BaseQuestionType
    {
        public override string PathToPartial { get; } = "Survey/OpenQuestion";

        public OpenQuestionType(QuestionDetails questionData) : base(questionData)
        {
        }

        public override PartialViewResult GetPartial()
        {
            return PartialView(PathToPartial, QuestionData);
        }
    }
}
