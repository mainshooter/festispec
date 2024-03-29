﻿using Festispec.Domain;

namespace Festispec.ViewModel.employee.department
{
    public class DepartmentVM
    {
        private Department _department;

        public string Name
        {
            get
            {
                return _department.Name;
            }
            set
            {
                _department.Name = value;
            }
        }

        public string Description
        {
            get
            {
                return _department.Description;
            }
            set
            {
                _department.Description = value;
            }
        }

        public DepartmentVM(Department department)
        {
            _department = department;
        }

        public DepartmentVM()
        {
            _department = new Department();
        }

        public Department ToModel()
        {
            return _department;
        }
    }
}
