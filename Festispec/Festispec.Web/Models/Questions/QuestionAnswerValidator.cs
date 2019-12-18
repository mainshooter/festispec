using Festispec.Domain;
using Festispec.Web.Models.Questions.Types;

namespace Festispec.Lib.Survey.Question.Validator
{
    public class QuestionAnswerValidator
    {
        public bool IsAnswerValid(Domain.Question question, Answer answer)
        {
            if (question == null)
            {
                return false;
            }
            switch (question.Type)
            {
                case "Afbeelding galerij vraag":
                    return ValidateGalleryQuestion(question, answer);
                case "Gesloten vraag":
                    return ValidateClosedQuestion(question, answer);
                case "Meerkeuze vraag":
                    return ValidateMultipleChoiseQuestion(question, answer);
                case "Open vraag":
                    return ValidateOpenQuestion(question, answer);
                case "Schuifbalk vraag":
                    return ValidateSliderQuestion(question, answer);
                case "Tabel vraag":
                    return ValidateTableQuestion(question, answer);
                case "Teken vraag":
                    return ValidateDrawQuestion(question, answer);
                default:
                    return false;
            }
        }

        private bool ValidateDrawQuestion(Domain.Question question, Answer answer)
        {
            if (string.IsNullOrEmpty(answer.Answer1) || string.IsNullOrWhiteSpace(answer.Answer1))
            {
                return false;
            }
            return true;
        }

        private bool ValidateTableQuestion(Domain.Question question, Answer answer)
        {
            try
            {

            }
            catch (System.Exception)
            {

                throw;
            }
            return false;
        }

        private bool ValidateSliderQuestion(Domain.Question question, Answer answer)
        {
            try
            {
                MultipleChoiseQuestionType multipleChoiseQuestionType = new MultipleChoiseQuestionType(question);
                int resultParse = int.Parse(answer.Answer1);
                int min = int.Parse(multipleChoiseQuestionType.Details.Choices.Cols[0]);
                int max = int.Parse(multipleChoiseQuestionType.Details.Choices.Cols[1]);
                if (resultParse >= min && resultParse <= max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
            }
            return false;
        }

        private bool ValidateOpenQuestion(Domain.Question question, Answer answer)
        {
            if (string.IsNullOrEmpty(answer.Answer1) || string.IsNullOrWhiteSpace(answer.Answer1))
            {
                return false;
            }
            return true;
        }

        private bool ValidateMultipleChoiseQuestion(Domain.Question question, Answer answer)
        {
            try
            {
                MultipleChoiseQuestionType multipleChoiseQuestionType = new MultipleChoiseQuestionType(question);
                int resultParse = int.Parse(answer.Answer1);
                int range = multipleChoiseQuestionType.Details.Choices.Options.Count;
                if (resultParse <= range && resultParse > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
            }
            return false;
        }

        private bool ValidateClosedQuestion(Domain.Question question, Answer answer)
        {
            if (answer.Answer1 == "Ja" || answer.Answer1 == "Nee")
            {
                return true;
            }
            return false;
        }

        private bool ValidateGalleryQuestion(Domain.Question question, Answer answer)
        {
            if (string.IsNullOrEmpty(answer.Answer1))
            {
                return false;
            }
            return true;
        }
    }
}
