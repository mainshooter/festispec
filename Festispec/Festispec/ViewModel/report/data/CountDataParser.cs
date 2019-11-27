using Festispec.Interface;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.data
{
    public class CountDataParser : DataVM, IDataParser
    {
        public override string Type => "COUNT";
        public List<string> SupportedQuestions => new List<string>() {
            Lib.Survey.Question.QuestionType.OpenQuestion,
            Lib.Survey.Question.QuestionType.MultipleChoiseQuestion,
            Lib.Survey.Question.QuestionType.TableQuestion,
            Lib.Survey.Question.QuestionType.SliderQuestion,
            Lib.Survey.Question.QuestionType.ClosedQuestion,
        };
        public List<string> SupportedVisuals => new List<string>() { "Table", "Barchart" };

        public List<List<string>> ParseData()
        {
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();
            List<string> headerRow = new List<string>();
            headerRow.Add("Aantal ingevulde cases");
            result.Add(headerRow);
            result.Add(new List<string>() { answers.Count.ToString() });
            return result;
        }
    }
}
