using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.certificate
{
    public class CertificateVM
    {
        public int Id { get; set; }
        public EmployeeVM Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
