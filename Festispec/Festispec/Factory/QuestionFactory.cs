using System.Collections.Generic;
using System.Windows;
using Festispec.View.Windows.survey.question.QuestionTypes.ClosedQuestion;
using Festispec.View.Windows.survey.question.QuestionTypes.OpenQuestion;

namespace Festispec.Factory
{
    class QuestionFactory
    {
        private readonly Dictionary<string, Window> _questionTypes;

        public QuestionFactory()
        {
            _questionTypes = new Dictionary<string, Window>
            {
                ["Open vraag"] = new AddOpenQuestionWindow(),
                ["Gesloten vraag"] = new AddClosedQuestionWindow()
            };
        }

        public Window GetQuestionType(string name)
        {
            return _questionTypes[name];
        }
    }
}
