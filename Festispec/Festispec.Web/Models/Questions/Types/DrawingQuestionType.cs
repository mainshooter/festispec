using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions.Types
{
    public class DrawingQuestionType : IQuestion
    {
        private QuestionDetails _details;
        public int Id { get; set; }
        public QuestionDetails Details { get; set; }
        public string DetailsJson { get; set; }
        public int AnswerValue { get; set; }
        public int AnswerText { get; set; }
    }
}