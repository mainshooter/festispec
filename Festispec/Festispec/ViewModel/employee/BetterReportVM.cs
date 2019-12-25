using Festispec.Domain;
using System;

namespace Festispec.ViewModel.employee
{
    public class BetterReportVM
    {
        private EmployeeVM _employee;
        private BetterReportInspector _betterReportInspector;

        public EmployeeVM Employee {
            get {
                return _employee;
            }
            set {
                _employee = value;
            }
        }

        public DateTime Date {
            get {
                return _betterReportInspector.DateTime;
            }
            set {
                _betterReportInspector.DateTime = value;
            }
        }

        public BetterReportVM(BetterReportInspector betterReport)
        {
            _betterReportInspector = betterReport;
            _employee = new EmployeeVM(betterReport.Employee);
        }

        public BetterReportVM()
        {
            _betterReportInspector = new BetterReportInspector();
        }
    }
}
