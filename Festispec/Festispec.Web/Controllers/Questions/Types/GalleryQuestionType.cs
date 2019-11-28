using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class GalleryQuestionType : BaseQuestionType
    {
        public override string PathToPartial { get; } = "Survey/GalleryQuestion";

        public GalleryQuestionType(QuestionDetails questionData) : base(questionData)
        {
        }

        public override PartialViewResult GetPartial()
        {
            return PartialView(PathToPartial, QuestionData);
        }
    }
}