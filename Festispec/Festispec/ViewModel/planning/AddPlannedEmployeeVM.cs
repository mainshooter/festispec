﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.employee.availabilty;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.planning
{
    public class AddPlannedEmployeeVM : ViewModelBase
    {
        private PlannedEmployeeVM _plannedEmployeeVM;
        private ObservableCollection<EmployeeVM> _availableInspectorList;
        private EventVM _eventVM;
        private DateTime _selectedBeginDate;

        public ICommand BackCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand SelectInspectorCommand { get; set; }
        public ICommand ClearInspectorCommand { get; set; }
        public ObservableCollection<EmployeeVM> InspectorList { get; set; }
        public ObservableCollection<DayVM> EventDays { get; set; }

        public List<AvailabiltyVM> AvailabilityList;
        public DateTime PlannedEmployeeStartTime
        {
            get
            {
                if (PlannedEmployeeVM == null)
                {
                    return DateTime.Today;
                }
                return PlannedEmployeeVM.PlannedStartTime;
            }
            set
            {
                if (PlannedEmployeeVM.Employee != null)
                {
                    if (value < AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyStart)
                    {
                        var temp = (DateTime)AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyStart;
                        PlannedEmployeeVM.PlannedStartTime = (DateTime)AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyStart;
                    }
                    else
                    {
                        if (value > AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyEnd)
                        {
                            PlannedEmployeeVM.PlannedStartTime = (DateTime)AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyEnd;
                        }
                        else
                        {
                            PlannedEmployeeVM.PlannedStartTime = value;
                        }
                    }
                }
                if (value > EventVM.EndDate)
                {
                    PlannedEmployeeVM.PlannedStartTime = EventVM.EndDate;
                    PlannedEmployeeVM.PlannedEndTime = EventVM.EndDate;
                }
                if (value < EventVM.BeginDate)
                {
                    PlannedEmployeeVM.PlannedStartTime = EventVM.BeginDate;
                }
                RaisePropertyChanged(() => PlannedEmployeeStartTime);
                RaisePropertyChanged(() => PlannedEmployeeEndTime);
            }
        }

        public DateTime PlannedEmployeeEndTime
        {
            get
            {
                if (PlannedEmployeeVM == null)
                {
                    return DateTime.Today;
                }
                return PlannedEmployeeVM.PlannedEndTime;
            }
            set
            {
                if (PlannedEmployeeVM.Employee != null)
                {
                    if (value > AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyEnd)
                    {
                        PlannedEmployeeVM.PlannedEndTime = (DateTime)AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyEnd;
                    }
                    else
                    {
                        PlannedEmployeeVM.PlannedEndTime = value;
                    }
                }
                if (value > EventVM.EndDate)
                {
                    PlannedEmployeeVM.PlannedEndTime = EventVM.EndDate;
                }
                RaisePropertyChanged(() => PlannedEmployeeEndTime);
            }
        }

        public Visibility VisibilityClearButton
        {
            get
            {
                if (PlannedEmployeeVM == null)
                {
                    return Visibility.Collapsed;
                }
                if (PlannedEmployeeVM.Employee == null)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public EventVM EventVM
        {
            get
            {
                return _eventVM;
            }

            set
            {
                _eventVM = value;
                RaisePropertyChanged(() => EventVM);
            }
        }

        public DateTime SelectedBeginDate
        {
            get
            {
                return _selectedBeginDate;
            }
            set
            {
                if (EventDays != null)
                {
                    PlannedEmployeeVM.Day = EventDays.Where(day => day.BeginTime.Date == value.Date).FirstOrDefault();
                }
                PlannedEmployeeVM.PlannedDate = value;
                _selectedBeginDate = value;
                GetAvailability();
                RaisePropertyChanged(() => SelectedBeginDate);
                RaisePropertyChanged(() => VisibilityClearButton);
            }
        }

        public ObservableCollection<EmployeeVM> FilteredAvailableInspectorList
        {
            get
            {
                return _availableInspectorList;
            }
            set
            {
                _availableInspectorList = value;
                RaisePropertyChanged(() => FilteredAvailableInspectorList);
            }
        }

        public PlannedEmployeeVM PlannedEmployeeVM
        {
            get
            {
                return _plannedEmployeeVM;
            }

            set
            {
                _plannedEmployeeVM = value;
                RaisePropertyChanged(() => PlannedEmployeeVM);
            }
        }

        public AddPlannedEmployeeVM()
        {
            AvailabilityList = new List<AvailabiltyVM>();
            InspectorList = new ObservableCollection<EmployeeVM>();
            MessengerInstance.Register<ChangeSelectedPlannedEmployeeEventMessage>(this, message =>
            {
                EventVM = message.EventVM;
                PlannedEmployeeVM = new PlannedEmployeeVM(EventVM.OrderVM.Days.Select(day => day).Where(day => day.BeginTime.Date == message.EventVM.BeginDate.Date).FirstOrDefault());
                SelectedBeginDate = message.EventVM.BeginDate;
                PlannedEmployeeVM.PlannedDate = message.EventVM.BeginDate;
                PlannedEmployeeVM.PlannedEndTime = message.EventVM.BeginDate;
                PlannedEmployeeVM.Order = message.EventVM.OrderVM;
                EventDays = message.EventVM.OrderVM.Days;
                GetInspectors();
                GetAvailability();
                RaisePropertyChanged(() => VisibilityClearButton);
            });
            BackCommand = new RelayCommand(Back);
            ClearInspectorCommand = new RelayCommand(ClearInspector);
            SaveChangesCommand = new RelayCommand(AddPlannedEmployee, CanSave);
            SelectInspectorCommand = new RelayCommand<EmployeeVM>(SelectEmployee);
            RaisePropertyChanged(() => VisibilityClearButton);
        }

        private void ClearInspector()
        {
            PlannedEmployeeVM.Employee = null;
            RaisePropertyChanged(() => VisibilityClearButton);
        }

        private void SelectEmployee(EmployeeVM source)
        {
            PlannedEmployeeVM.Employee = source;
            RaisePropertyChanged(() => VisibilityClearButton);
        }

        private void AddPlannedEmployee()
        {
            PlannedEmployeeVM.Day = EventVM.OrderVM.Days.Select(day => day).Where(day => day.BeginTime.Date == PlannedEmployeeVM.PlannedStartTime.Date).FirstOrDefault();
            PlannedEmployeeVM.WorkStartTime = PlannedEmployeeVM.PlannedStartTime;
            PlannedEmployeeVM.WorkEndTime = PlannedEmployeeVM.PlannedEndTime;
            PlannedEmployeeVM.Day.InspectorPlannings.Add(PlannedEmployeeVM);

            using (var context = new Entities())
            {
                var temp = PlannedEmployeeVM.ToModel();
                temp.Employee = null;
                temp.Day = null;
                context.InspectorPlannings.Add(temp);
                context.SaveChanges();
            }
            Back();
        }

        private bool CanSave()
        {
            if (PlannedEmployeeVM == null || PlannedEmployeeVM.PlannedStartTime >= PlannedEmployeeVM.PlannedEndTime || PlannedEmployeeVM.Employee == null
                || PlannedEmployeeVM.PlannedStartTime < EventVM.BeginDate || PlannedEmployeeVM.PlannedEndTime > EventVM.EndDate)
            {
                return false;
            }
            return true;
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        public void GetInspectors()
        {
            using (var context = new Entities())
            {
                InspectorList = new ObservableCollection<EmployeeVM>(context.Employees.ToList().Select(employee => new EmployeeVM(employee)).Where(employee => employee.Department.Name == "Inspectie"));
            }
        }

        public void GetAvailability()
        {
            List<PlannedEmployeeVM> PlannedEmployeeList = new List<PlannedEmployeeVM>();
            FilteredAvailableInspectorList = new ObservableCollection<EmployeeVM>();
            using (var context = new Entities())
            {
                AvailabilityList = new List<AvailabiltyVM>(context.AvailabilityInspectors.ToList().Select(availableInspector => new AvailabiltyVM(availableInspector)).Where(availableInspector => availableInspector.AvailabiltyStart.Value.Date == SelectedBeginDate.Date && availableInspector.AvailabiltyEnd.Value >= SelectedBeginDate));
                PlannedEmployeeList = new List<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(plannedEmployee => new PlannedEmployeeVM(plannedEmployee)).Where(plannedEmployee => plannedEmployee.PlannedStartTime.Date == SelectedBeginDate.Date));
            }

            foreach (EmployeeVM employee in InspectorList)
            {
                var availabilityVM = AvailabilityList.Select(availibility => availibility).Where(availability => availability.EmployeeId == employee.Id).FirstOrDefault();
                var plannedEmployeeVM = PlannedEmployeeList.Select(plannedEmployee => plannedEmployee).Where(plannedEmployee => plannedEmployee.Employee.Id == employee.Id).FirstOrDefault();
                if (availabilityVM != null && plannedEmployeeVM == null)
                {
                    FilteredAvailableInspectorList.Add(employee);
                }
            }
            RaisePropertyChanged(() => FilteredAvailableInspectorList);
        }
    }
}
