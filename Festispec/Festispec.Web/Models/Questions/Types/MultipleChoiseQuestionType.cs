using Festispec.Domain;
using Festispec.Lib.Survey.Question;
using Newtonsoft.Json;

namespace Festispec.Web.Models.Questions.Types
{
    public class MultipleChoiseQuestionType : IQuestion
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public QuestionDetails Details { get; set; }
        public string Variable { get; set; }
        public string DetailsJson { get; set; }
        public int AnswerValue { get; set ; }
        public int AnswerText { get; set; }

        public MultipleChoiseQuestionType()
        {
            Type = Lib.Enums.QuestionType.MultipleChoiseQuestion;
        }

        public MultipleChoiseQuestionType(Question question)
        {
            Id = question.Id;
            Type = question.Type;
            Details = JsonConvert.DeserializeObject<QuestionDetails>(question.Question1);
            Variable = question.Variables;
        }
    }
}