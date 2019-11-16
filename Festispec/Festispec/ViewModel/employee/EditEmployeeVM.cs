﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee.department;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;

using System.Windows.Input;

namespace Festispec.ViewModel.employee
{
    public class EditEmployeeVM : ViewModelBase
    {
        private EmployeeVM _employeeVM;
        private int _departmentIndex;
        public EmployeeListVM EmployeeList { get; set; }
        public EmployeeVM Employee
        {
            get
            {
                return _employeeVM;
            }
            set
            {
                _employeeVM = value;
                RaisePropertyChanged("Employee");
            }
        }
        public ObservableCollection<DepartmentVM> Departments { get; set; }
        public ObservableCollection<string> Statuses { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand CloseEditEmployeeCommand { get; set; }

        public int DepartmentIndex
        {
            get
            {
                return _departmentIndex;
            }
            set
            {
                _departmentIndex = value;
                RaisePropertyChanged("DepartmentIndex");
            }
        }
        public EditEmployeeVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEmployeeMessage>(this, message =>
            {
                Employee = message.Employee;
                DepartmentIndex = Departments.IndexOf(Departments.Select(department => department).Where(department => department.Name == Employee.Department.Name).FirstOrDefault());
            });
            EditEmployeeCommand = new RelayCommand(EditEmployee, CanEditEmployee);
            CloseEditEmployeeCommand = new RelayCommand(CloseEditEmployee);
            using (var context = new Entities())
            {
                Departments = new ObservableCollection<DepartmentVM>(context.Departments.ToList().Select(department => new DepartmentVM(department)));
                Statuses = new ObservableCollection<string>(context.EmployeeStatus.ToList().Select(status => status.Status));
            }
        }

        public void EditEmployee()
        {
            using (var context = new Entities())
            {
                context.Entry(Employee.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            EmployeeList.RaisePropertyChanged("EmployeeListFiltered");
            CloseEditEmployee();
        }

        private void CloseEditEmployee()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }

        public bool CanEditEmployee()
        {
            if (Employee == null)
            {
                return false;
            }
            if (Employee.Firstname == null || Employee.Lastname == null || Employee.City == null || Employee.Department == null || Employee.Email == null || Employee.HouseNumber <= 0 || Employee.Phone == null || Employee.PostalCode == null || Employee.Status == null || Employee.Street == null || Employee.Birthday.ToString().Equals("") || Employee.Birthday == null || Employee.Iban == null)
            {
                return false;
            }
            string regexEmail = "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            string regexPhone = "^\\s*(?:\\+?(\\d{1,3}))?([-. (]*(\\d{3})[-. )]*)?((\\d{3})[-. ]*(\\d{2,4})(?:[-.x ]*(\\d+))?)\\s*$";
            string regexIban = "[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}";
            if (!Regex.IsMatch(Employee.Email, regexEmail) || !Regex.IsMatch(Employee.Phone, regexPhone) || !Regex.IsMatch(Employee.Iban, regexIban) || Employee.Birthday.Year < 1900 || Employee.HouseNumberAddition.Length > 5 || Employee.Iban.Length > 27 || Employee.Password.Length > 255 || Employee.Email.Length > 100 || Employee.Phone.Length > 15 || Employee.City.Length > 45 || Employee.PostalCode.Length > 6 || Employee.HouseNumber > 9999 || Employee.Street.Length > 100 || Employee.Lastname.Length > 45 || Employee.Prefix.Length > 45 || Employee.Firstname.Length > 45)
            {
                return false;
            }
            return true;
        }
    }
}