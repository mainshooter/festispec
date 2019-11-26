using Festispec.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class AvgDataParser : DataVM, IDataParser
    {
        public string ParserType => "AVG";
        public List<string> SupportedQuestions => new List<string>() { "Schuifbalk vraag" };
        public List<string> SupportedVisuals => new List<string>() { "Table" };

        public List<List<string>> ParseData()
        {
            return ParseDefaultData();
        }

        private List<List<string>> ParseDefaultData()
        {
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
