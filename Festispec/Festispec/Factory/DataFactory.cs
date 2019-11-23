using Festispec.Interface;
using Festispec.ViewModel.report.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Factory
{
    public class DataFactory
    {
        public static IDataParser CreateDataElement(string type)
        {
            switch (type)
            {
                case "SELECT":
                    return new SelectDataParserVM();
                case "MIN":
                    return new MinDataParserVM();
                default:
                    return new SelectDataParserVM();
            }
        }
    }
}
