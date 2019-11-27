using System.Web.Mvc;
using Festispec.Domain;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class UploadQuestionType : BaseQuestionType
    {
        public override string PathToPartial { get; } = "Survey/UploadQuestion";

        public UploadQuestionType(QuestionDetails questionData) : base(questionData)
        {
        }

        public override PartialViewResult GetPartial()
        {
            return PartialView(PathToPartial, QuestionData);
        }
    }
}