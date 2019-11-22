using System.Web.Mvc;
using Festispec.Domain;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class ClosedQuestionType : BaseQuestionType
    {
        public ClosedQuestionType(Question questionData) : base(questionData)
        {
        }

        public override PartialViewResult RenderQuestionInput()
        {
            return PartialView("Survey/ClosedQuestion", QuestionData);
        }
    }
}