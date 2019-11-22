using System;
using System.Web.Mvc;
using Festispec.Domain;

namespace Festispec.Web.Controllers.Questions.Types
{
    public abstract class BaseQuestionType : Controller, IQuestion
    {
        public Question QuestionData { get; }
        protected BaseQuestionType(Question questionData)
        {
            QuestionData = questionData;
        }

        public virtual PartialViewResult RenderQuestionInput()
        {
            throw new NotImplementedException();
        }
    }
}