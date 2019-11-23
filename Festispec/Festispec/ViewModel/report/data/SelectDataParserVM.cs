using Festispec.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.data
{
    public class SelectDataParserVM : DataVM, IDataParser
    {
        public string ParserType => "SELECT";
        public override string Type => ParserType;

        public List<List<string>> ParseData()
        {
            throw new NotImplementedException();
        }
    }
}
