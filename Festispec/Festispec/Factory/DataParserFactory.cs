using Festispec.Interface;
using Festispec.ViewModel.report.data;
using System;
using System.Collections.Generic;

namespace Festispec.Factory
{
    public class DataParserFactory
    {
        public List<string> DataTypes { get; set; }
        public List<IDataParser> DataParsers { get; private set; }

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
            DataParsers = new List<IDataParser>();
            DataParsers.Add(new SelectDataParserVM());
            DataParsers.Add(new CountDataParser());
            DataParsers.Add(new CountOptionsDataParser());
            DataParsers.Add(new MinDataParserVM());
            DataParsers.Add(new MaxDataParserVM());
            DataParsers.Add(new AvgDataParser());
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
            if (type.Equals("AVG"))
            {
                return new AvgDataParser();
            }
            return null;
        }
    }
}
