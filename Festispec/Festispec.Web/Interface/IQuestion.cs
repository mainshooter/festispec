﻿using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions
{
    public interface IQuestion
    {
        int Id { get; set; }
        string Type { get; set; }
        string Variable { get; set; }
        QuestionDetails Details { get; set; }
        string DetailsJson { get; set; }
        int AnswerValue { get; set; }
        int AnswerText { get; set; }
    }
}
