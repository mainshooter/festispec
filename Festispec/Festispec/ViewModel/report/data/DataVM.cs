﻿using Festispec.Domain;
using Festispec.Interface;
using Festispec.ViewModel.survey.answer;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.data
{
    public class DataVM
    {
        public virtual string Type { get; set; }

        public string Where { get; set; }

        public string GroupBy { get; set; }
        public IQuestion Question { get; set; }

        [PreferredConstructor]
        public DataVM()
        {
        }

        public DataVM(string json)
        {
            Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            Type = dic["Type"];
            Where = dic["Where"];
            GroupBy = dic["GroupBy"];
        }

        public string ToJson()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Type", Type);
            dic.Add("Where", Where);
            dic.Add("GroupBy", GroupBy);
            dic.Add("QuestionId", Question.Id.ToString());
            return JsonConvert.SerializeObject(dic);
        }

        protected List<SurveyAnswerVM> GetQuestionAnswers()
        {
            var answers = new List<SurveyAnswerVM>();
            using (var context = new Entities())
            {
                var dbResult = context.Answers.Where(answer => answer.QuestionId.Equals(Question.Id)).ToList();
                answers = new List<SurveyAnswerVM>(dbResult.Select(answer => new SurveyAnswerVM(answer)).ToList());
            }
            return answers;
        }
    }
}