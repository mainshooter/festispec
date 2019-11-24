using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.data
{
    public class CountOptionsDataParser : DataVM, IDataParser
    {
        public string ParserType => "COUNT_OPTIONS";

        public List<List<string>> ParseData()
        {
            string questionType = Question.QuestionType;
            if (questionType.Equals("Gesloten vraag"))
            {
                return ParseDataClosedQuestion();
            }
            if (questionType.Equals("Meerkeuze vraag"))
            {
                return ParseDataMultipleChoise();
            }
            return new List<List<string>>();
        }

        private List<List<string>> ParseDataMultipleChoise()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();

            List<string> headerRow = new List<string>(Question.QuestionDetails.Choices.Cols);
            result.Add(headerRow);
            List<int> totalChoises = new List<int>();
            for (int i = 0; i < headerRow.Count; i++)
            {
                totalChoises.Add(0);
            }
            foreach (var item in answers)
            {
                int answerIndex = int.Parse(item.Answer);
                for (int i = 0; i < headerRow.Count; i++)
                {
                    if (answerIndex == i)
                    {
                        totalChoises[i]++;
                        break;
                    }
                }
            }
            var resultRow = new List<string>();
            for (int i = 0; i < totalChoises.Count; i++)
            {
                resultRow.Add(totalChoises[i].ToString());
            }
            result.Add(resultRow);
            return result;
        }

        private List<List<string>> ParseDataClosedQuestion()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();
            var headerRow = new List<string>() { "Ja", "Nee" };
            result.Add(headerRow);
            int totalYes = 0;
            int totalNo = 0;
            foreach (var answer in answers)
            {
                var givenAnswer = answer.Answer;
                if (givenAnswer.Equals("Ja"))
                {
                    totalYes++;
                }
                else if (givenAnswer.Equals("Nee"))
                {
                    totalNo++;
                }
            }
            result.Add(new List<string>() { totalYes.ToString(), totalNo.ToString()});

            return result;
        }
    }
}
