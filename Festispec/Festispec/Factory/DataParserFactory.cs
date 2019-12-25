using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Enums;
using Festispec.ViewModel.report.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
                DataParserType.SELECT,
                DataParserType.MIN,
                DataParserType.MAX,
                DataParserType.AVG,
                DataParserType.COUNT,
                DataParserType.COUNTOPTIONS,
                DataParserType.DRAW,
            };
            DataParsers = new List<IDataParser>();
            DataParsers.Add(new SelectDataParserVM());
            DataParsers.Add(new CountDataParser());
            DataParsers.Add(new CountOptionsDataParser());
            DataParsers.Add(new MinDataParserVM());
            DataParsers.Add(new MaxDataParserVM());
            DataParsers.Add(new AvgDataParser());
            DataParsers.Add(new DrawDataParser());
        }

        public static IDataParser GetDataParserByJson(string json)
        {
            if (json == null)
            {
                return null;
            }

            var factory = new DataParserFactory();
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            IDataParser parser = factory.GetDataParser(result["Type"]);
            using(var context = new Domain.Entities())
            {
                int questionId = int.Parse(result["QuestionId"]);
                Question question = context.Questions.Where(q => q.Id == questionId).First();
                parser.Question = QuestionFactory.CreateQuestion(question);
            }
                
            return parser;
        }

        public IDataParser GetDataParser(string type)
        {
            if (!DataTypes.Contains(type))
            {
                throw new Exception("Type not found");
            }
            if (type.Equals(DataParserType.SELECT))
            {
                return new SelectDataParserVM();
            }
            if (type.Equals(DataParserType.COUNT))
            {
                return new CountDataParser();
            }
            if (type.Equals(DataParserType.COUNTOPTIONS))
            {
                return new CountOptionsDataParser();
            }
            if (type.Equals(DataParserType.MIN))
            {
                return new MinDataParserVM();
            }
            if (type.Equals(DataParserType.MAX))
            {
                return new MaxDataParserVM();
            }
            if (type.Equals(DataParserType.AVG))
            {
                return new AvgDataParser();
            }
            if (type.Equals(DataParserType.DRAW))
            {
                return new DrawDataParser();
            }
            return null;
        }
    }
}
