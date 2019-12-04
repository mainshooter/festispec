using System.Collections.Generic;
using Festispec.Web.Models.Questions.Types;

namespace Festispec.Web.Models.Questions
{
    public static class QuestionTypeFactory
    {
        public static IQuestion CreateQuestionTypeFor(string questionType)
        {
            switch (questionType)
            {
                case "Afbeelding galerij vraag": return new GalleryQuestionType();
                case "Gesloten vraag": return new ClosedQuestionType();
                case "Meerkeuze vraag": return new MultipleChoiseQuestionType();
                case "Open vraag": return new OpenQuestionType();
                case "Schuifbalk vraag": return new SliderQuestionType();
                case "Tabel vraag": return new TableQuestionType();
                case "Teken vraag": return new DrawingQuestionType();
                case "Upload vraag": return new UploadQuestionType();
                default: return new OpenQuestionType();
            }
        }
    }
}