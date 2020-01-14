using Festispec.Interface;
using Festispec.Lib.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.data
{
    public class SurveyImagesDataParserVM : DataVM, IDataParser
    {
        public override string Type => DataParserType.IMAGE;
        public List<string> SupportedQuestions => new List<string>() {
            Lib.Enums.QuestionType.ImageGaleryQuestion,
        };

        public List<string> SupportedVisuals => new List<string>() {
            ReportElementType.SurveyImages
        };

        public bool QuestionTypeIsSupported {
            get {
                var questionCheckResult = SupportedQuestions.Where(s => s == Question.QuestionType);
                if (questionCheckResult != null && questionCheckResult.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public List<List<string>> ParseData()
        {
            if (!CanRun())
            {
                return new List<List<string>>();
            }
            List<List<string>> result = new List<List<string>>();
            var answers = GetQuestionAnswers();

            foreach (var answer in answers)
            {
                List<byte[]> images = JsonConvert.DeserializeObject<List<Byte[]>>(answer.Answer);
                foreach (var image in images)
                {
                    result.Add(new List<string>() { 
                        Encoding.UTF8.GetString(image, 0, image.Length)
                    });
                }
            }

            return result;
        }
    }
}
