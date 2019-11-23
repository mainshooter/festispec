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

        public List<List<string>> ParseData()
        {
            if (Question.QuestionType.Equals("Open vraag"))
            {
                return ParseDataOpenQuestion();
            }
            return new List<List<string>>();
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
