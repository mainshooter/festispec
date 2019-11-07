using Festispec.Domain;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.rapport.element;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.rapport
{
    public class ReportVM
    {
        private Report _report;

        public int Id {
            get {
                return _report.Id;
            }
            private set {
                _report.Id = value;
            }
        }

        public OrderVM Order { get; set; }

        public string Title {
            get {
                return _report.Title;
            }
            set {
                _report.Title = value;
            }
        }

        public string Status {
            get {
                return _report.Status;
            }
            set {
                _report.Status = value;
            }
        }

        public ObservableCollection<ReportElementVM> ReportElements { get; set; }

        public ReportVM(Report report)
        {
            _report = report;
            ReportElements = new ObservableCollection<ReportElementVM>(report.ReportElements.ToList().Select(e => new ReportElementVM(e)));
        }

        public ReportVM()
        {
            _report = new Report();
        }
    }
}
