using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Festispec.Domain;

namespace Festispec.Web.Models.Questions.Types
{
    public class DrawingQuestionType : BaseQuestionType
    {
        public DrawingQuestionType(Question questionData) : base(questionData)
        {
        }

        public override string RenderHtml()
        {
            return base.RenderHtml();
        }
    }
}