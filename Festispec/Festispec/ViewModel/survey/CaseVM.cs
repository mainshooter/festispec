using Festispec.ViewModel.employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey
{
    public class CaseVM
    {
        public int Id { get; set; }
        public SurveyVM Survey { get; set; }
        public EmployeeVM Employee { get; set; }
        public string Status { get; set; }
    }
}
