﻿using Festispec.Domain;
using Festispec.ViewModel.employee;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.Repository
{
    public class EmployeeRepository
    {
        public List<EmployeeVM> GetEmployees()
        {
            List<EmployeeVM> result = new List<EmployeeVM>();
            using (var context = new Entities())
            {
                result = new List<EmployeeVM>(context.Employees.ToList().Select(employee => new EmployeeVM(employee)));
            }
            return result;
        }
    }
}
