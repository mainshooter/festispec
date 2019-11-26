using System.Collections.Generic;

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
