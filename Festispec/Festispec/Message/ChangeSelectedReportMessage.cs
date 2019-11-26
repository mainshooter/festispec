using Festispec.ViewModel.report;
using Festispec.ViewModel.report.element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Message
{
    public class ChangeSelectedReportMessage
    {
        public ReportVM NextReportVM { get; set; }
        public ReportElementVM ReportElement { get; set; }

    }
}
