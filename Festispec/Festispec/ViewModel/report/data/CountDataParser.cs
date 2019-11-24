using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.data
{
    public class CountDataParser : DataVM, IDataParser
    {
        public string ParserType => "COUNT";

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
