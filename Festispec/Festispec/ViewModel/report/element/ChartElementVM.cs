using Festispec.ViewModel.rapport.element;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element
{
    public class ChartElementVM: ReportElementVM
    {
        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<string, string> YaxisLabelFormat { get; set; }

        public List<string> Labels { get; set; }

    }
}
