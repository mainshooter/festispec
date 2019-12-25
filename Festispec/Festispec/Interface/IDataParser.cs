using System.Collections.Generic;

namespace Festispec.Interface
{
    public interface IDataParser
    {
        List<string> SupportedQuestions { get; }
        List<string> SupportedVisuals { get; }
        string Type { get; }
        IQuestion Question { get; set; }
        List<List<string>> ParseData();
        bool QuestionTypeIsSupported { get; }
        string ToJson();
    }
}
