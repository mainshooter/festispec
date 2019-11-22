using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Festispec.Domain;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class TableQuestionType : BaseQuestionType
    {
        public TableQuestionType(Question questionData) : base(questionData)
        {
        }

        public override PartialViewResult RenderQuestionInput()
        {
            return PartialView("Survey/TableQuestion", QuestionData);
        }
    }
}