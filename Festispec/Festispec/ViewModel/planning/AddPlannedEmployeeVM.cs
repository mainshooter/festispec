using Festispec.Domain;
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
using System.Threading.Tasks;
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
        private DateTime _selectedAvailabilityStartDate;
        private DateTime _selectedAvailabilityEndDate;

        public ICommand BackCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand SelectInspectorCommand { get; set; }
        public ICommand ClearInspectorCommand { get; set; }
        public ObservableCollection<EmployeeVM> InspectorList { get; set; }
        public ObservableCollection<DayVM> EventDays { get; set; }

        public List<AvailabiltyVM> AvailabilityList;

        public DateTime SelectedAvailabilityStartDate
        {
            get => _selectedAvailabilityStartDate;
            set
            {
               _selectedAvailabilityStartDate = value;
                RaisePropertyChanged(() => SelectedAvailabilityStartDate);
            }
        }

        public DateTime SelectedAvailabilityEndDate
        {
            get => _selectedAvailabilityEndDate;
            set
            {
                _selectedAvailabilityEndDate = value;
                RaisePropertyChanged(() => SelectedAvailabilityEndDate);
            }
        }

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
                if (value.Date < EventVM.BeginDate)
                {
                    PlannedEmployeeVM.PlannedStartTime = EventVM.BeginDate;
                }
                RaisePropertyChanged(() => PlannedEmployeeStartTime);
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
                if (value.Date > EventVM.EndDate)
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

        public ObservableCollection<EmployeeVM> SortedAvailableInspectorList {
            get
            {
                if (_availableInspectorList == null) return null;
                return new ObservableCollection<EmployeeVM>(_availableInspectorList.OrderBy(e => e.DistanceFromEvent));
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
                GetInspectors();
                EventVM = message.EventVM;
                PlannedEmployeeVM = new PlannedEmployeeVM(EventVM.OrderVM.Days.Select(day => day).Where(day => day.BeginTime.Date == message.EventVM.BeginDate.Date).FirstOrDefault());
                SelectedBeginDate = message.EventVM.BeginDate;
                PlannedEmployeeVM.PlannedDate = message.EventVM.BeginDate;
                PlannedEmployeeVM.PlannedEndTime = message.EventVM.BeginDate;
                PlannedEmployeeVM.Order = message.EventVM.OrderVM;
                EventDays = message.EventVM.OrderVM.Days;
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
            SelectedAvailabilityStartDate = (DateTime)AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyStart;
            SelectedAvailabilityEndDate = (DateTime)AvailabilityList.Select(availability => availability).Where(availability => availability.EmployeeId == PlannedEmployeeVM.Employee.Id).FirstOrDefault().AvailabiltyEnd;
            PlannedEmployeeStartTime = SelectedAvailabilityStartDate;
            PlannedEmployeeEndTime = SelectedAvailabilityEndDate;
            RaisePropertyChanged(() => PlannedEmployeeStartTime);
            RaisePropertyChanged(() => PlannedEmployeeEndTime);
            RaisePropertyChanged(() => VisibilityClearButton);
        }

        private void AddPlannedEmployee()
        {
            if (!PlannedEmployeeVM.EditMessageIfNotWithinWeek(EventVM)) return;

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
                || PlannedEmployeeVM.PlannedStartTime.Date < EventVM.BeginDate || PlannedEmployeeVM.PlannedEndTime.Date > EventVM.EndDate)
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

        public async Task GetAvailability()
        {
            var PlannedEmployeeList = new List<PlannedEmployeeVM>();
            _availableInspectorList = new ObservableCollection<EmployeeVM>();

            using (var context = new Entities())
            {
                AvailabilityList = new List<AvailabiltyVM>(context.AvailabilityInspectors.ToList().Select(availableInspector => new AvailabiltyVM(availableInspector)).Where(availableInspector => availableInspector.AvailabiltyStart.Value.Date == SelectedBeginDate.Date && availableInspector.AvailabiltyEnd.Value >= SelectedBeginDate));
                PlannedEmployeeList = new List<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(plannedEmployee => new PlannedEmployeeVM(plannedEmployee)).Where(plannedEmployee => plannedEmployee.PlannedStartTime.Date == SelectedBeginDate.Date));
            }

            foreach (var employee in InspectorList)
            {
                var availabilityVM = AvailabilityList.FirstOrDefault(availability => availability.EmployeeId == employee.Id);
                var plannedEmployeeVM = PlannedEmployeeList.FirstOrDefault(plannedEmployee => plannedEmployee.Employee.Id == employee.Id);

                if (availabilityVM != null && plannedEmployeeVM == null)
                {
                    employee.DistanceFromEvent = await DistanceFromEventAsync(employee, EventVM);
                    _availableInspectorList.Add(employee);
                }
            }

            RaisePropertyChanged(() => SortedAvailableInspectorList);
        }

        public async Task<int> DistanceFromEventAsync(EmployeeVM employeeVM, EventVM eventVM)
        {
            var distanceCalculator = new DistanceCalculator.DistanceCalculator();
            var employeeHouseNumberAddition = "";
            var eventHouseNumberAddition = "";

            if (string.IsNullOrEmpty(employeeVM.HouseNumberAddition)) employeeHouseNumberAddition = " " + employeeVM.HouseNumberAddition;

            if (string.IsNullOrEmpty(eventVM.HouseNumberAddition)) eventHouseNumberAddition = " " + eventVM.HouseNumberAddition;

            var employeeddressToCoor = await distanceCalculator.AdressToCoor(employeeVM.Street, employeeVM.HouseNumber + employeeHouseNumberAddition, employeeVM.City, employeeVM.PostalCode);
            var eventAddressToCoor = await distanceCalculator.AdressToCoor(eventVM.Street, eventVM.HouseNumber + eventHouseNumberAddition, eventVM.City, eventVM.PostalCode);

            if (employeeddressToCoor == null || eventAddressToCoor == null) return 0;

            var distance = await distanceCalculator.TravelDistance(employeeddressToCoor, eventAddressToCoor);
            return Convert.ToInt32(Math.Round(distance));
        }
    }
}
