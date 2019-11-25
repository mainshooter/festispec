using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Festispec.Domain;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Controllers.Questions.Types
{
    public class DrawingQuestionType : BaseQuestionType
    {
        public override string PathToPartial { get; } = "Survey/DrawingQuestion";

        public DrawingQuestionType(QuestionDetails questionData) : base(questionData)
        {
        }

        public override PartialViewResult GetPartial()
        {
            return PartialView(PathToPartial, QuestionData);
        }
    }
}