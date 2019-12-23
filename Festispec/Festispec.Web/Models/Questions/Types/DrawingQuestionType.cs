using System.IO;
using Festispec.Lib.Survey.Question;
using System;

namespace Festispec.Web.Models.Questions.Types
{
    public class DrawingQuestionType : IQuestion
    {
        private QuestionDetails _details;
        
        public int Id { get; set; }
        
        public QuestionDetails Details { get; set; }

        public string ImageUrl { get => "Content/question-" + Details.Question + ".jpeg"; }
        public string DetailsJson { get; set; }
        public int AnswerValue { get; set; }
        public int AnswerText { get; set; }
    }
}