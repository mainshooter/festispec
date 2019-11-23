using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.answer;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class SelectDataParserVM : DataVM, IDataParser
    {
        public string ParserType => "SELECT";
        public override string Type => ParserType;

        public List<string> SupportedQuestionTypes { get; private set; }
        public List<string> SupportedElementTypes { get; private set; }

        public SelectDataParserVM()
        {
            SupportedQuestionTypes = new List<string>()
            {
                "Open vraag",
                "Meerkeuze vraag",
            };
            SupportedElementTypes = new List<string>()
            {
                "table",
            };
        }

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
            if (Question.QuestionType.Equals("Open vraag"))
            {
                return ParseDataOpenQuestion();
            }
            if (Question.QuestionType.Equals("Meerkeuze vraag"))
            {
                return ParseDataMultipleChoise();
            }
            return new List<List<string>>();
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
