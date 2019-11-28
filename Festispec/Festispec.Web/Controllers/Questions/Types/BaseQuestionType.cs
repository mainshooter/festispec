using System;
using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public abstract class BaseQuestionType : Controller, IQuestion
    {
        public abstract string PathToPartial { get; }
        protected QuestionDetails QuestionData { get; }

        protected BaseQuestionType(QuestionDetails questionData)
        {
            QuestionData = questionData;
        }

        public virtual PartialViewResult GetPartial()
        {
            throw new NotImplementedException();
        }
    }
}