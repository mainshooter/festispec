using Festispec.Lib.Survey.Question;

namespace Festispec.Web.Models.Questions.Types
{
    public class SliderQuestionType : IQuestion
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public QuestionDetails Details { get; set; }
        public string Variable { get; set; }
        public string DetailsJson { get; set; }
        public int AnswerValue { get; set; }
        public int AnswerText { get; set; }

        public SliderQuestionType()
        {
            Type = Lib.Enums.QuestionType.SliderQuestion;
        }
    }
}