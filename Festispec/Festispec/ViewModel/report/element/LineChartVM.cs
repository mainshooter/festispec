using Festispec.ViewModel.rapport.element;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element
{
    public class LineChartVM: ReportElementVM
    {
        private Object _data;

        public Dictionary<string, Object> Dictionary { get; set; }

        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<string, string> YaxisLabelFormat { get; set; }

        public List<string> Labels { get; set; }

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

        public LineChartVM(ReportElementVM element)
        {
            Labels = new List<string>();
            Data = element.Data;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
        }

        private void ApplyChanges()
        {
            XaxisName = (string)Dictionary["xaxisName"];
            YaxisName = (string)Dictionary["yaxisName"];
            SeriesCollection = (SeriesCollection)Dictionary["seriescollection"];
        }

    }
}
