using Festispec.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.data
{
    public class MinDataParserVM : DataVM, IDataParser
    {
        public string ParserType => "MIN";

        public List<List<string>> ParseData()
        {
            string type = Question.QuestionType;
            if (type.Equals("Meerkeuze vraag"))
            {
                return ParseDataMultipleChoise();
            }
            return ParseDataDefault();
        }

        private List<List<string>> ParseDataMultipleChoise() {
            List<List<string>> result = new List<List<string>>();
            var answers = GetQuestionAnswers();

            List<string> headerRow = new List<string>(Question.QuestionDetails.Choices.Cols);
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

            int lowestAmountOfSelectedAnswer = 0;
            for (int i = 0; i < totalChoises.Count; i++)
            {
                int currentTotal = totalChoises[i];
                if (currentTotal < totalChoises[lowestAmountOfSelectedAnswer])
                {
                    lowestAmountOfSelectedAnswer = i;
                }
            }

            result.Add(new List<string>() { Question.QuestionDetails.Question });
            result.Add(new List<string>() { headerRow[lowestAmountOfSelectedAnswer] });
            return result;
        }

        private List<List<string>> ParseDataDefault()
        {
            List<List<string>> result = new List<List<string>>();
            var answers = GetQuestionAnswers();

            var answer = answers.Min(a => a.Answer);

            result.Add(new List<string>() { Question.QuestionDetails.Question });
            result.Add(new List<string>() { answer });

            return result;
        }
    }
}
