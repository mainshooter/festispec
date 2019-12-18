using Festispec.Interface;
using Festispec.Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class AvgDataParser : DataVM, IDataParser
    {
        public override string Type => DataParserType.AVG;
        public List<string> SupportedQuestions => new List<string>() { 
            Lib.Enums.QuestionType.SliderQuestion
        };
        public List<string> SupportedVisuals => new List<string>() {
            ReportElementType.Table
        };

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
            return ParseDefaultData();
        }

        private List<List<string>> ParseDefaultData()
        {
            if (!CanRun())
            {
                return new List<List<string>>();
            }
            List<List<string>> result = new List<List<string>>();
            List<int> intAnsers = new List<int>();
            var answers = GetQuestionAnswers();
            string answer = "";

            try
            {
                
                foreach (var item in answers)
                {
                    intAnsers.Add(int.Parse(item.Answer));
                }
                answer = intAnsers.Average(a => a).ToString();
            }
            catch (Exception)
            {
            }


            result.Add(new List<string>() { Question.QuestionDetails.Question });
            result.Add(new List<string>() { answer });

            return result;
        }
    }
}
