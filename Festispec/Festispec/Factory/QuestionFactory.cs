using System.Collections.Generic;
using System.Windows;
using Festispec.Domain;
using Festispec.View.Windows.survey.question.QuestionTypes.ClosedQuestion;
using Festispec.View.Windows.survey.question.QuestionTypes.OpenQuestion;
using Festispec.ViewModel.survey.question.questionTypes;
using Festispec.ViewModel.survey.question.QuestionTypes;

namespace Festispec.Factory
{
    class QuestionFactory
    {
        private readonly Dictionary<string, Window> _questionTypes;

        public QuestionFactory()
        {
            _questionTypes = new Dictionary<string, Window>
            {
                ["Add Open vraag"] = new AddOpenQuestionWindow(),
                ["Edit Open vraag"] = new EditOpenQuestionWindow(),
                ["Add Gesloten vraag"] = new AddClosedQuestionWindow(),
                ["Edit Gesloten vraag"] = new EditClosedQuestionWindow()
            };
        }

        public Window GetQuestionType(string name)
        {
            return _questionTypes[name];
        }

        public IQuestion CreateQuestionType(Question question)
        {
            switch (question.Type)
            {
                case "Open vraag":
                    return new OpenQuestionVM(question);
                case "Gesloten vraag":
                    return new ClosedQuestionVM(question);
                default:
                    return new OpenQuestionVM(question);
            }
        }
    }
}
