using Festispec.Interface;
using Festispec.Lib.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.data
{
    public class SelectDataParserVM : DataVM, IDataParser
    {
        public override string Type => DataParserType.SELECT;

        public List<string> SupportedQuestions => new List<string>() {
            Lib.Survey.Question.QuestionType.OpenQuestion,
            Lib.Survey.Question.QuestionType.MultipleChoiseQuestion,
            Lib.Survey.Question.QuestionType.TableQuestion,
            Lib.Survey.Question.QuestionType.SliderQuestion,
            Lib.Survey.Question.QuestionType.ClosedQuestion
        };
        public List<string> SupportedVisuals => new List<string>() { ReportElementType.Table };
        public List<List<string>> ParseData()
        {
            string questionType = Question.QuestionType;
            if (questionType.Equals(Lib.Survey.Question.QuestionType.OpenQuestion))
            {
                return ParseDataOpenQuestion();
            }
            if (questionType.Equals(Lib.Survey.Question.QuestionType.MultipleChoiseQuestion))
            {
                return ParseDataMultipleChoise();
            }
            if (questionType.Equals(Lib.Survey.Question.QuestionType.TableQuestion))
            {
                return ParseDataTableQuestion();
            }
            if (questionType.Equals(Lib.Survey.Question.QuestionType.SliderQuestion))
            {
                return ParseSliderQuestion();
            }
            if (questionType.Equals(Lib.Survey.Question.QuestionType.ClosedQuestion))
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
                var parsedAnswer = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(answer.Answer);
                var answerList = parsedAnswer["Answers"];
                for (int i = 0; i < answerList.Count; i++)
                {
                    if (i == selectedColIndex)
                    {
                        var selectedOptionAnswer = int.Parse(answerList[i]);
                        answerList[i] = Question.QuestionDetails.Choices.Options[selectedOptionAnswer];
                    }
                }
                result.Add(answerList);
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
