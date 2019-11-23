using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Interface
{
    public interface IDataParser
    {
        string ParserType { get; }
        List<List<string>> ParseData();
    }
}
