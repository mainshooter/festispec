using Festispec.Domain;
using Festispec.ViewModel.report;
using Festispec.ViewModel.report.element;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.Repository
{
    public class ReportRepository
    {
        public ObservableCollection<ReportElementVM> GetReportElements(ReportVM report)
        {
            using (var context = new Entities())
            {
                return new ObservableCollection<ReportElementVM>(context.ReportElements.ToList().Where(r => r.ReportId == report.Id).OrderBy(reportElement => reportElement.Order).Select(reportElement => new ReportElementVM(reportElement, report)));
            }
        }
    }
}
