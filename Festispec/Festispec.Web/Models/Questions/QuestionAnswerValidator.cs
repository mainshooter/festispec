﻿using Festispec.Domain;
using Festispec.Web.Models.Questions.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            if (answer.Answer1 == null)
            {
                return false;
            }
            return true;
        }

        private bool ValidateTableQuestion(Domain.Question question, Answer answer)
        {
            try
            {
                TableQuestionType tableQuestionType = new TableQuestionType(question);
                List<List<string>> answersParsed = JsonConvert.DeserializeObject<List<List<string>>>(answer.Answer1);

                int selectedColIndex = 0;
                string selectedColumnName = tableQuestionType.Details.Choices.SelectedCol;
                if (selectedColumnName != null)
                {
                    foreach (var item in tableQuestionType.Details.Choices.Cols)
                    {
                        if (selectedColumnName.Equals(item))
                        {
                            break;
                        }
                        selectedColIndex++;
                    }
                    int maximumOptionCount = tableQuestionType.Details.Choices.Options.Count;


                    foreach (var parsedRow in answersParsed)
                    {
                        var answerString = parsedRow[selectedColIndex];
                        int parsedAnswer = int.Parse(answerString);
                        if (parsedAnswer < 0 && parsedAnswer > maximumOptionCount)
                        {
                            return false;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
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
            if (answer.Answer1 == null)
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
                int range = multipleChoiseQuestionType.Details.Choices.Cols.Count;
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
            if (answer.Answer1 != null)
            {
                return false;
            }
            return true;
        }
    }
}
