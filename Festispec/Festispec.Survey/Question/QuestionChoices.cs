using System.Collections.Generic;

namespace Festispec.Survey.Question
{
    public class QuestionChoices
    {
        public List<string> Cols { get; set; }
        public List<string> Options { get; set; }

        public QuestionChoices()
        {
            Cols = new List<string>();
            Options = new List<string>();
        }
    }
}
