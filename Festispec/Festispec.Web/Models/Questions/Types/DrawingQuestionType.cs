using System.IO;
using Festispec.Lib.Survey.Question;
using System;

namespace Festispec.Web.Models.Questions.Types
{
    public class DrawingQuestionType : IQuestion
    {
        private QuestionDetails _details;
        public int Id { get; set; }

        public QuestionDetails Details
        {
            get => _details;
            set
            {
                _details = value;
                ByteArrayToImageUrl();
            }
        }

        public string ImageUrl { get => "Content/question-" + Details.Question + ".jpeg"; }
        public int AnswerValue { get; set; }
        public int AnswerText { get; set; }

        public void ByteArrayToImageUrl() => File.WriteAllBytes(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Content/question-" + Details.Question + ".jpeg"), Details.Images[0]);
    }
}