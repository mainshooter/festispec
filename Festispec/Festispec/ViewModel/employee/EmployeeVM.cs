using Festispec.ViewModel.employee.department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.employee
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public DepartmentVM Department { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        private string Password { get; set; }
        private string PasswordResetToken { get; set; }
        private DateTime ResetTokenExpire { get; set; }
        public string Iban { get; set; }
        public string Status { get; set; }
        
    }
}
