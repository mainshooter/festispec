using System.Collections.Generic;
using Festispec.Domain;
using Festispec.Web.Controllers.Questions.Types;

namespace Festispec.Web.Controllers.Questions
{
    public class QuestionTypeRepository
    {
        private readonly Dictionary<string, IQuestion> _questionTypes;

        public QuestionTypeRepository(Question question)
        {
            _questionTypes = new Dictionary<string, IQuestion>
            {
                ["Afbeelding galerij vraag"] = new GalleryQuestionType(question),
                ["Gesloten vraag"] = new ClosedQuestionType(question),
                ["Meerkeuze vraag"] = new MultipleChoiseQuestionType(question),
                ["Open vraag"] = new OpenQuestionType(question),
                ["Schuifbalk vraag"] = new SliderQuestionType(question),
                ["Tabel vraag"] = new TableQuestionType(question),
                ["Teken vraag"] = new DrawingQuestionType(question)
            };
        }

        public IQuestion GetQuestionType(string questionType)
        {
            return _questionTypes[questionType];
        }
    }
}