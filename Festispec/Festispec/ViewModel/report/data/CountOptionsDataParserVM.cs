using Festispec.Interface;
using Festispec.Lib.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class CountOptionsDataParser : DataVM, IDataParser
    {
        public override string Type => DataParserType.COUNTOPTIONS;

        public List<string> SupportedQuestions => new List<string>() {
            Lib.Enums.QuestionType.ClosedQuestion,
            Lib.Enums.QuestionType.MultipleChoiseQuestion,
            Lib.Enums.QuestionType.SliderQuestion
        };

        public List<string> SupportedVisuals => new List<string>() { 
            ReportElementType.Table,
            ReportElementType.Barchart,
            ReportElementType.Linechart,
            ReportElementType.Piechart,
        };

        public bool QuestionTypeIsSupported 
        {
            get 
            {
                if (Question == null)
                {
                    return false;
                }
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
            string questionType = Question.QuestionType;
            if (questionType.Equals(Lib.Enums.QuestionType.ClosedQuestion))
            {
                return ParseDataClosedQuestion();
            }
            if (questionType.Equals(Lib.Enums.QuestionType.MultipleChoiseQuestion))
            {
                return ParseDataMultipleChoise();
            }
            if (questionType.Equals(Lib.Enums.QuestionType.SliderQuestion))
            {
                return ParseSliderQuestion();
            }
            return new List<List<string>>();
        }

        private List<List<string>> ParseSliderQuestion()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();

            List<string> headerRow = new List<string>();
            int startIndex = int.Parse(Question.QuestionDetails.Choices.Cols[0]);
            int endIndex = int.Parse(Question.QuestionDetails.Choices.Cols[1]);
            while (startIndex < endIndex)
            {
                headerRow.Add(startIndex.ToString());
                startIndex++;
            }
            result.Add(headerRow);
            List<int> totalChoises = new List<int>();
            for (int i = 0; i < headerRow.Count; i++)
            {
                totalChoises.Add(0);
            }
            foreach (var answer in answers)
            {
                startIndex = int.Parse(Question.QuestionDetails.Choices.Cols[0]);
                endIndex = int.Parse(Question.QuestionDetails.Choices.Cols[1]);
                var resultRow = new List<string>();
                int internIndex = 0;
                int answerSelectedRange = int.Parse(answer.Answer);
                while (startIndex < endIndex)
                {
                    if (startIndex == answerSelectedRange)
                    {
                        totalChoises[internIndex]++;
                        break;
                    }
                    internIndex++;
                    startIndex++;
                }
            }
            List<string> totalChoisesAsString = new List<string>();
            foreach (var item in totalChoises)
            {
                totalChoisesAsString.Add(item.ToString());
            }
            result.Add(totalChoisesAsString);
            return result;
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
