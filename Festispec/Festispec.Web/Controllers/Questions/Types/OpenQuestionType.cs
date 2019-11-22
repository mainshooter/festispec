using System.Web.Mvc;
using Festispec.Domain;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class OpenQuestionType : BaseQuestionType
    {
        public OpenQuestionType(Question questionData) : base(questionData)
        {
        }

        public override PartialViewResult RenderQuestionInput()
        {
            return PartialView("Survey/OpenQuestion", QuestionData);
        }
    }
}
