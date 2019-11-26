using System.Collections.Generic;

namespace Festispec.Lib.Survey.Question
{
    public class QuestionDetails
    {
        public string Question { get; set; }
        public QuestionChoices Choices { get; set; }
        public string Description { get; set; }
        public List<byte[]> Images { get; set; }

        public QuestionDetails()
        {
            Question = "";
            Choices = new QuestionChoices();
            Description = "";
            Images = new List<byte[]>();
        }
    }
}
