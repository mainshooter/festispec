using System.Collections.Generic;

namespace Festispec.Model
{
    public class QuestionDetails
    {
        public string Question { get; set; }
        public QuestionChoices Choices { get; set; }
        public string Description { get; set; }
        public List<byte[]> Images { get; set; }

        public QuestionDetails()
        {
            Choices = new QuestionChoices();
            Images = new List<byte[]>();
        }
    }
}
