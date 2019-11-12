using Festispec.ViewModel.rapport.element;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element
{
    public class BarChartVM : ReportElementVM
    {
        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { set; get; }

        public List<string> Labels { set; get; }

        public Func<double,string> Formatter { set; get;}

    }
}
