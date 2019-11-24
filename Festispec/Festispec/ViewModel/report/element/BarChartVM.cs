using LiveCharts;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class BarChartVM : ReportElementVM
    {
        private Object _data;

        public Dictionary<string, Object> Dictionary { get; set; }

        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { set; get; }

        public List<string> Labels { set; get; }

        public Func<double,string> Formatter { set; get;}

        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
                Dictionary = (Dictionary<string, Object>) Data;
                ApplyChanges();
            }
        }

        public BarChartVM(ReportElementVM element)
        {
            //Data = element.Data;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
        }

        private void ApplyChanges()
        {
            Labels = (List<string>)Dictionary["labels"];
            XaxisName = (string)Dictionary["xaxisName"];
            YaxisName = (string)Dictionary["yaxisName"];
            SeriesCollection = (SeriesCollection)Dictionary["seriescollection"];
        }
    }
}
