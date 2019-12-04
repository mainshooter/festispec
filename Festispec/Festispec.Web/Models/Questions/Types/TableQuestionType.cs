﻿using System.Web.Mvc;
using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions.Types
{
    public class TableQuestionType : IQuestion
    {
        public int Id { get; set; }
        public QuestionDetails Details { get; set; }
        public int AnswerValue { get; set; }
        public int AnswerText { get; set; }
    }
}