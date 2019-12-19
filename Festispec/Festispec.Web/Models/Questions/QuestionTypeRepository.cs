using System.Collections.Generic;
using Festispec.Web.Models.Questions.Types;

namespace Festispec.Web.Models.Questions
{
    public class QuestionTypeRepository
    {
        private readonly Dictionary<string, IQuestion> _questionTypes;

        public QuestionTypeRepository()
        {
            _questionTypes = new Dictionary<string, IQuestion>
            {
                ["Afbeelding galerij vraag"] = new GalleryQuestionType(),
                ["Gesloten vraag"] = new ClosedQuestionType(),
                ["Meerkeuze vraag"] = new MultipleChoiseQuestionType(),
                ["Open vraag"] = new OpenQuestionType(),
                ["Schuifbalk vraag"] = new SliderQuestionType(),
                ["Tabel vraag"] = new TableQuestionType(),
                ["Teken vraag"] = new DrawingQuestionType(),
                ["Opmerking vraag"] = new CommentQuestionType(),
            };
        }

        public IQuestion GetQuestionType(string questionType)
        {
            return _questionTypes[questionType];
        }
    }
}