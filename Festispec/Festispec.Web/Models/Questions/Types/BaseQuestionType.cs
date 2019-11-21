using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Festispec.Domain;

namespace Festispec.Web.Models.Questions.Types
{
    public abstract class BaseQuestionType : IQuestion
    {
        public Question QuestionData { get; }
        protected BaseQuestionType(Question questionData)
        {
            QuestionData = questionData;
        }

        public virtual string RenderHtml()
        {
            throw new NotImplementedException();
        }
    }
}