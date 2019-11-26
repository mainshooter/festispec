using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Interface
{
    public interface IDataParser
    {
        List<string> SupportedQuestions { get; }
        List<string> SupportedVisuals { get; }
        string ParserType { get; }
        IQuestion Question { get; set; }
        List<List<string>> ParseData();
    }
}
