using Festispec.Interface;
using Festispec.Lib.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class SelectDataParserVM : DataVM, IDataParser
    {
        public override string Type => DataParserType.SELECT;

        public List<string> SupportedQuestions => new List<string>() 
        {
            Lib.Enums.QuestionType.OpenQuestion,
            Lib.Enums.QuestionType.MultipleChoiseQuestion,
            Lib.Enums.QuestionType.TableQuestion,
            Lib.Enums.QuestionType.SliderQuestion,
            Lib.Enums.QuestionType.ClosedQuestion
        };

        public List<string> SupportedVisuals => new List<string>() { ReportElementType.Table };

        public bool QuestionTypeIsSupported 
        {
            get 
            {
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
            if (questionType.Equals(Lib.Enums.QuestionType.OpenQuestion))
            {
                return ParseDataOpenQuestion();
            }
            if (questionType.Equals(Lib.Enums.QuestionType.MultipleChoiseQuestion))
            {
                return ParseDataMultipleChoise();
            }
            if (questionType.Equals(Lib.Enums.QuestionType.TableQuestion))
            {
                return ParseDataTableQuestion();
            }
            if (questionType.Equals(Lib.Enums.QuestionType.SliderQuestion))
            {
                return ParseSliderQuestion();
            }
            if (questionType.Equals(Lib.Enums.QuestionType.ClosedQuestion))
            {
                return ParseClosedQuestion();
            }
            return new List<List<string>>();
        }

        private List<List<string>> ParseClosedQuestion()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();
            var headerRow = new List<string>() { "Ja", "Nee" };
            result.Add(headerRow);
            foreach (var answer in answers)
            {
                var givenAnswer = answer.Answer;
                List<string> givenAnswerRow = new List<string>();
                if (givenAnswer.Equals("Ja"))
                {
                    givenAnswerRow = new List<string>() { "Ja", "" };
                }
                else if (givenAnswer.Equals("Nee"))
                {
                    givenAnswerRow = new List<string>() { "", "Nee" };
                }
                result.Add(givenAnswerRow);
            }
            return result;
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

            foreach (var answer in answers)
            {
                startIndex = int.Parse(Question.QuestionDetails.Choices.Cols[0]);
                endIndex = int.Parse(Question.QuestionDetails.Choices.Cols[1]);
                var resultRow = new List<string>();
                while (startIndex < endIndex)
                {
                    if (startIndex == int.Parse(answer.Answer))
                    {
                        resultRow.Add("1");
                    }
                    else
                    {
                        resultRow.Add("");
                    }
                    startIndex++;
                }
                result.Add(resultRow);
            }

            return result;
        }

        private List<List<string>> ParseDataTableQuestion()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();
            List<string> headerRow = new List<string>(Question.QuestionDetails.Choices.Cols);
            result.Add(headerRow);

            int selectedColIndex = 0;
            string selectedColumnName = Question.QuestionDetails.Choices.SelectedCol;
            foreach (var item in Question.QuestionDetails.Choices.Cols)
            {
                if (selectedColumnName.Equals(item))
                {
                    break;
                }
                selectedColIndex++;
            }

            foreach (var answer in answers)
            {
                var parsedAnswer = JsonConvert.DeserializeObject<List<List<string>>>(answer.Answer);
                foreach (var item in parsedAnswer)
                {
                    for (int i = 0; i < item.Count; i++)
                    {
                        if (i == selectedColIndex)
                        {
                            var selectedOptionAnswer = int.Parse(item[i]);
                            item[i] = Question.QuestionDetails.Choices.Options[selectedOptionAnswer];
                        }
                    }
                    result.Add(item);
                }
            }
            return result;
        }

        private List<List<string>> ParseDataMultipleChoise()
        {
            var result = new List<List<string>>();

            var answers = GetQuestionAnswers();
            List<string> headerRow = new List<string>(Question.QuestionDetails.Choices.Cols);
            result.Add(headerRow);
            foreach (var item in answers)
            {
                var listRow = new List<string>();
                int answerIndex = int.Parse(item.Answer);
                for (int i = 0; i < headerRow.Count; i++)
                {
                    if (answerIndex == i)
                    {
                        listRow.Add("1");
                    }
                    else
                    {
                        listRow.Add("");
                    }
                }
                result.Add(listRow);
            }
            return result;
        }

        private List<List<string>> ParseDataOpenQuestion()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();
            result.Add(new List<string>() { Question.QuestionDetails.Question});

            foreach (var item in answers)
            {
                result.Add(new List<string>() { item.Answer});
            }
            return result;
        }
    }
}
