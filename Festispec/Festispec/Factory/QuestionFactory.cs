using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.question.QuestionTypes;
using System;

namespace Festispec.Factory
{
    public class QuestionFactory
    {
        public static IQuestion CreateQuestion(Question question)
        {
            switch (question.Type)
            {
                case Lib.Enums.QuestionType.OpenQuestion:
                    return new OpenQuestionVM(question);
                case Lib.Enums.QuestionType.ClosedQuestion:
                    return new ClosedQuestionVM(question);
                case Lib.Enums.QuestionType.SliderQuestion:
                    return new SliderQuestionVM(question);
                case Lib.Enums.QuestionType.NoteQuestion:
                    return new CommentFieldVM(question);
                case Lib.Enums.QuestionType.ImageGaleryQuestion:
                    return new ImageGalleryQuestionVM(question);
                case Lib.Enums.QuestionType.DrawQuestion:
                    return new DrawQuestionVM(question);
                case Lib.Enums.QuestionType.MultipleChoiseQuestion:
                    return new MultipleChoiceQuestionVM(question);
                case Lib.Enums.QuestionType.TableQuestion:
                    return new TableQuestionVM(question);
                default:
                    throw new NotSupportedException("Question type not supported.");
            }
        }
    }
}
