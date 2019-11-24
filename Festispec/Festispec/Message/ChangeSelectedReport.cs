using Festispec.ViewModel.report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Message
{
    public class ChangeSelectedReport
    {
        public ReportVM NextReportVM { get; set; }

        public int OrderNumber { get; set; }
    }
}
