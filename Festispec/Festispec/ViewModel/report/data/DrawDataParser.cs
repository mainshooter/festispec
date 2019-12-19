
using System.Collections.Generic;
using System.Linq;
using Festispec.Interface;
using Festispec.Lib.Enums;

namespace Festispec.ViewModel.report.data
{
    public class DrawDataParser : DataVM, IDataParser
    {
        public override string Type => DataParserType.DRAW;

        public List<string> SupportedQuestions => new List<string>() {
            Lib.Enums.QuestionType.DrawQuestion
        };

        public List<string> SupportedVisuals => new List<string>() {
            ReportElementType.Draw
        };

        public bool QuestionTypeIsSupported {
            get {
                return true;
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
            var result = new List<List<string>>();
            var answers = GetQuestionAnswers();

            foreach (var item in answers)
            {
                result.Add(new List<string>() { item.Answer });
            }

            return result;
        }
    }
}
