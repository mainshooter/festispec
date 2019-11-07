using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee
{
    public class BetterReportVM
    {
        private EmployeeVM _employee;
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

        private BetterReportInspector _betterReportInspector;
        public BetterReportVM(BetterReportInspector betterReport)
        {
            _betterReportInspector = betterReport;
            _employee = new EmployeeVM(betterReport.Employee);
        }
    }
}
