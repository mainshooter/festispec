using Festispec.ViewModel.rapport.element;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM: ReportElementVM
    {
        private Object _data;
        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
                ApplyChanges();
            }
        }
        public SeriesCollection SeriesCollection { get; set; }

        public PieChartVM(ReportElementVM element)
        {
            Title = "Test";
            Content = "Lorem ipsum";
            Data = element.Data;
        }

        private void ApplyChanges()
        {
            SeriesCollection = (SeriesCollection)Data;
        }
    }
}
