using System.Collections.Generic;
using Festispec.Web.Models.Questions.Types;

namespace Festispec.Web.Models.Questions
{
    public class QuestionTypeFactory
    {
        private readonly Dictionary<string, IQuestion> _questionTypes;

        public QuestionTypeFactory()
        {
            _questionTypes = new Dictionary<string, IQuestion>
            {
                [Lib.Enums.QuestionType.ImageGaleryQuestion] = new GalleryQuestionType(),
                [Lib.Enums.QuestionType.ClosedQuestion] = new ClosedQuestionType(),
                [Lib.Enums.QuestionType.MultipleChoiseQuestion] = new MultipleChoiseQuestionType(),
                [Lib.Enums.QuestionType.OpenQuestion] = new OpenQuestionType(),
                [Lib.Enums.QuestionType.SliderQuestion] = new SliderQuestionType(),
                [Lib.Enums.QuestionType.TableQuestion] = new TableQuestionType(),
                [Lib.Enums.QuestionType.DrawQuestion] = new DrawingQuestionType(),
                [Lib.Enums.QuestionType.NoteQuestion] = new CommentQuestionType(),
            };
        }

        public IQuestion GetQuestionType(string questionType)
        {
            return _questionTypes[questionType];
        }
    }
}