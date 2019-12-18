using Festispec.Interface;
using Festispec.Lib.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class MaxDataParserVM : DataVM, IDataParser
    {
        public override string Type => DataParserType.MAX;
        public List<string> SupportedQuestions => new List<string>() {
            Lib.Enums.QuestionType.OpenQuestion,
            Lib.Enums.QuestionType.MultipleChoiseQuestion,
            Lib.Enums.QuestionType.TableQuestion,
            Lib.Enums.QuestionType.SliderQuestion,
            Lib.Enums.QuestionType.ClosedQuestion
        };
        public List<string> SupportedVisuals => new List<string>() { ReportElementType.Table };

        public bool QuestionTypeIsSupported {
            get {
                var questionCheckResult = SupportedQuestions.Where(s => s == Question.QuestionType);
                if (questionCheckResult != null && questionCheckResult.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public List<List<string>> ParseData()
        {
            if (!CanRun())
            {
                return new List<List<string>>();
            }
            string type = Question.QuestionType;
            if (type.Equals(Lib.Enums.QuestionType.MultipleChoiseQuestion))
            {
                return ParseDataMultipleChoise();
            }
            if (type.Equals(Lib.Enums.QuestionType.ClosedQuestion))
            {
                return ParseDataClosedQuestion();
            }
            return ParseDataDefault();
        }

        private List<List<string>> ParseDataClosedQuestion()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();
            var headerRow = new List<string>() { Question.Question };
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
            if (totalYes > totalNo)
            {
                result.Add(new List<string>() { totalYes.ToString() });
            }
            else if (totalNo > totalYes)
            {
                result.Add(new List<string>() { totalNo.ToString() });
            }
            else
            {
                result.Add(new List<string>() { "0" });
            }


            return result;
        }

        private List<List<string>> ParseDataMultipleChoise()
        {
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
                if (currentTotal > totalChoises[lowestAmountOfSelectedAnswer])
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

            var answer = answers.Max(a => a.Answer);

            result.Add(new List<string>() { Question.QuestionDetails.Question });
            result.Add(new List<string>() { answer });

            return result;
        }
    }
}
