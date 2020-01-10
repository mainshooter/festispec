using Festispec.Web.Models.Questions.Types;

namespace Festispec.Web.Models.Questions
{
    public class QuestionTypeFactory
    {
        public IQuestion GetQuestionType(string c)
        {
            switch (c)
            {
                case Lib.Enums.QuestionType.ImageGaleryQuestion:
                    return new GalleryQuestionType();
                case Lib.Enums.QuestionType.ClosedQuestion:
                    return new ClosedQuestionType();
                case Lib.Enums.QuestionType.MultipleChoiseQuestion:
                    return new MultipleChoiseQuestionType();
                case Lib.Enums.QuestionType.OpenQuestion:
                    return new OpenQuestionType();
                case Lib.Enums.QuestionType.SliderQuestion:
                    return new SliderQuestionType();
                case Lib.Enums.QuestionType.TableQuestion:
                    return new TableQuestionType();
                case Lib.Enums.QuestionType.DrawQuestion:
                    return new DrawingQuestionType();
                case Lib.Enums.QuestionType.NoteQuestion:
                    return new CommentQuestionType();
            }
            return new OpenQuestionType();
        }
    }
}