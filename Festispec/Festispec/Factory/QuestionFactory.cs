using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.question.QuestionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Factory
{
    public class QuestionFactory
    {
        public static IQuestion CreateQuestion(Question question)
        {
            switch (question.Type)
            {
                case "Open vraag":
                    return new OpenQuestionVM(question);
                case "Gesloten vraag":
                    return new ClosedQuestionVM(question);
                case "Schuifbalk vraag":
                    return new SliderQuestionVM(question);
                case "Opmerking vraag":
                    return new CommentFieldVM(question);
                case "Afbeelding galerij vraag":
                    return new ImageGalleryQuestionVM(question);
                case "Teken vraag":
                    return new DrawQuestionVM(question);
                case "Meerkeuze vraag":
                    return new MultipleChoiceQuestionVM(question);
                case "Tabel vraag":
                    return new TableQuestionVM(question);
                default:
                    throw new NotSupportedException("Question type not supported.");
            }
        }
    }
}
