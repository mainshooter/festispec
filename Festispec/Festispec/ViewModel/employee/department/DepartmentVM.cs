using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee.department
{
    public class DepartmentVM
    {
        public string Name {
            get {
                return _department.Name;
            }
            set {
                _department.Name = value;
            }
        }
        public string Description { 
            get {
                return _department.Description;
            }
            set {
                _department.Description = value;
            }
        }
        private Department _department;
        public DepartmentVM(Department department)
        {
            _department = department;
        }
    }
}
