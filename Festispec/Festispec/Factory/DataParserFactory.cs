using Festispec.Interface;
using Festispec.ViewModel.report.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Factory
{
    public class DataParserFactory
    {
        public List<string> DataTypes { get; set; }

        public DataParserFactory()
        {
            DataTypes = new List<string>()
            {
                "SELECT",
                "MIN",
                "HIGH",
                "AVG",
                "COUNT",
                "COUNT_OPTIONS"
            };
        }

        public IDataParser GetDataParser(string type)
        {
            if (!DataTypes.Contains(type))
            {
                throw new Exception("Type not found");
            }
            if (type.Equals("SELECT"))
            {
                return new SelectDataParserVM();
            }
            if (type.Equals("COUNT"))
            {
                return new CountDataParser();
            }
            if (type.Equals("COUNT_OPTIONS"))
            {
                return new CountOptionsDataParser();
            }
            if (type.Equals("MIN"))
            {
                return new MinDataParserVM();
            }
            if (type.Equals("MAX"))
            {
                return new MaxDataParserVM();
            }
            return null;
        }
    }
}
