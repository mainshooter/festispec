using Festispec.Interface;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.data
{
    public class DataVM
    {
        public virtual string Type { get; set; }

        public string Where { get; set; }

        public string GroupBy { get; set; }
        public List<IQuestion> Questions { get; set; }

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
            return JsonConvert.SerializeObject(dic);
        }
    }
}
