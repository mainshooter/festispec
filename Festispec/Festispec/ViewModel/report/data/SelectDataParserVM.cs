using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.answer;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class SelectDataParserVM : DataVM, IDataParser
    {
        public string ParserType => "SELECT";
        public override string Type => ParserType;

        //INSERT INTO QuestionType VALUES('Afbeelding galerij vraag')
        //INSERT INTO QuestionType VALUES('Gesloten vraag')
        //INSERT INTO QuestionType VALUES('Meerkeuze vraag')
        //INSERT INTO QuestionType VALUES('Open vraag')
        //INSERT INTO QuestionType VALUES('Opmerking vraag')
        //INSERT INTO QuestionType VALUES('Schuifbalk vraag')
        //INSERT INTO QuestionType VALUES('Tabel vraag')
        //INSERT INTO QuestionType VALUES('Teken vraag')
        public List<List<string>> ParseData()
        {
            string questionType = Question.QuestionType;
            if (questionType.Equals("Open vraag"))
            {
                return ParseDataOpenQuestion();
            }
            if (questionType.Equals("Meerkeuze vraag"))
            {
                return ParseDataMultipleChoise();
            }
            if (questionType.Equals("Tabel vraag"))
            {
                return ParseDataTableQuestion();
            }
            return new List<List<string>>();
        }

        private List<List<string>> ParseDataTableQuestion()
        {
            var result = new List<List<string>>();
            var answers = new List<SurveyAnswerVM>();
            using (var context = new Entities())
            {
                var dbResult = context.Answers.Where(answer => answer.QuestionId.Equals(Question.Id)).ToList();
                answers = new List<SurveyAnswerVM>(dbResult.Select(answer => new SurveyAnswerVM(answer)).ToList());
            }
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

            var answers = new List<SurveyAnswerVM>();
            using (var context = new Entities())
            {
                var dbResult = context.Answers.Where(answer => answer.QuestionId.Equals(Question.Id)).ToList();
                answers = new List<SurveyAnswerVM>(dbResult.Select(answer => new SurveyAnswerVM(answer)).ToList());
            }
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
            var answers = new List<SurveyAnswerVM>();
            using (var context = new Entities())
            {
                var dbResult = context.Answers.Where(answer => answer.QuestionId.Equals(Question.Id)).ToList();
                answers = new List<SurveyAnswerVM>(dbResult.Select(answer => new SurveyAnswerVM(answer)).ToList());
            }
            result.Add(new List<string>() { Question.QuestionDetails.Question});

            foreach (var item in answers)
            {
                result.Add(new List<string>() { item.Answer});
            }

            return result;
        }


    }
}
