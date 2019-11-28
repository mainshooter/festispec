using System.Collections.Generic;
using Festispec.Lib.Survey.Question;
using Festispec.Web.Controllers.Questions.Types;

namespace Festispec.Web.Controllers.Questions
{
    public class QuestionTypeRepository
    {
        private readonly Dictionary<string, BaseQuestionType> _questionTypes;

        public QuestionTypeRepository(QuestionDetails question)
        {
            _questionTypes = new Dictionary<string, BaseQuestionType>
            {
                ["Afbeelding galerij vraag"] = new GalleryQuestionType(question),
                ["Gesloten vraag"] = new ClosedQuestionType(question),
                ["Meerkeuze vraag"] = new MultipleChoiseQuestionType(question),
                ["Open vraag"] = new OpenQuestionType(question),
                ["Schuifbalk vraag"] = new SliderQuestionType(question),
                ["Tabel vraag"] = new TableQuestionType(question),
                ["Teken vraag"] = new DrawingQuestionType(question),
                ["Upload vraag"] = new UploadQuestionType(question)
            };
        }

        public BaseQuestionType GetQuestionType(string questionType)
        {
            return _questionTypes[questionType];
        }
    }
}