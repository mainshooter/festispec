using Festispec.ViewModel.report;
using Festispec.ViewModel.report.element;

namespace Festispec.Message
{
    public class ChangeSelectedReportMessage
    {
        public ReportVM NextReportVM { get; set; }
        public ReportElementVM ReportElement { get; set; }
    }
}
