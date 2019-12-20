using Festispec.Domain;
using Festispec.Web.Models.Questions.Types;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Festispec.Web.Models.Questions
{
    public class QuestionCleanerAnswer
    {
        public Answer CleanAnswer(Question question, Answer answer)
        {
            if (question.Type == "Tabel vraag")
            {
                return CleanTableAnswer(question, answer);
            }
            return answer;
        }

        private Answer CleanTableAnswer(Question question, Answer answer)
        {
            TableQuestionType tableQuestionType = new TableQuestionType(question);
            List<List<string>> answersParsed = JsonConvert.DeserializeObject<List<List<string>>>(answer.Answer1);
            if (answersParsed == null)
            {
                return null;
            }
            int selectedColIndex = 0;
            string selectedColumnName = tableQuestionType.Details.Choices.SelectedCol;
            if (selectedColumnName != null && selectedColumnName != "Geen")
            {
                foreach (var item in tableQuestionType.Details.Choices.Cols)
                {
                    if (selectedColumnName.Equals(item))
                    {
                        break;
                    }
                    selectedColIndex++;
                }
            }

            for (int i = answersParsed.Count - 1; i >= 0; i--)
            {
                var answerRow = answersParsed[i];
                foreach (var item in answerRow)
                {
                    if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
                    {
                        answersParsed.RemoveAt(i);
                        break;
                    }
                }
            }

            answer.Answer1 = JsonConvert.SerializeObject(answersParsed);
            return answer;
        }
    }
}