using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.planning
{
    public class AddPlannedEmployeeVM : ViewModelBase
    {
        private PlannedEmployeeVM _plannedEmployeeVM;
        private ObservableCollection<EmployeeVM> _availableInspectorList;
        private EventVM _eventVM;

        public ICommand BackCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand SelectInspectorCommand { get; set; }

        public ObservableCollection<EmployeeVM> AvailableInspectorList
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

        public ObservableCollection<EmployeeVM> FilteredAvailableInspectorList
        {
            get
            {
                var filteredList = new ObservableCollection<EmployeeVM>();

                foreach (EmployeeVM employee in AvailableInspectorList)
                {
                    var availabilityVM = employee.AvailabiltyVMs.Select(availibility => availibility).Where(availability => availability.AvailabiltyStart.Value.Date == PlannedEmployeeVM.PlannedStartTime.Date).FirstOrDefault();
                    var plannedEmployeeVM = employee.plannedEmployeeVMs.Select(plannedEmployee => plannedEmployee).Where(plannedEmployee => plannedEmployee.PlannedStartTime.Date == PlannedEmployeeVM.PlannedStartTime.Date).FirstOrDefault();
                    if (availabilityVM != null && plannedEmployeeVM == null)
                    {
                        filteredList.Add(employee);
                    }
                }
                return filteredList;
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
            _plannedEmployeeVM = new PlannedEmployeeVM();
            AvailableInspectorList = new ObservableCollection<EmployeeVM>();
            this.MessengerInstance.Register<ChangeSelectedPlannedEmployeeEventMessage>(this, message =>
            {
                EventVM = message.EventVM;
                PlannedEmployeeVM.PlannedStartTime = message.EventVM.BeginDate;
                PlannedEmployeeVM.PlannedEndTime = message.EventVM.BeginDate;
                PlannedEmployeeVM.OrderVM = message.EventVM.OrderVM;
                GetAvailableInspectors();
                RaisePropertyChanged(() => FilteredAvailableInspectorList);
            });
            BackCommand = new RelayCommand(Back);
            SaveChangesCommand = new RelayCommand(AddPlannedEmployee,CanSave);
            SelectInspectorCommand = new RelayCommand<EmployeeVM>(SelectEmployee);
        }

        private void SelectEmployee(EmployeeVM source)
        {
            _plannedEmployeeVM.Employee = source;
            RaisePropertyChanged(() => PlannedEmployeeVM);
        }

        private void AddPlannedEmployee()
        {
            PlannedEmployeeVM.Day = EventVM.OrderVM.Days.Select(day => day).Where(day => day.BeginTime.Date == PlannedEmployeeVM.PlannedStartTime.Date).FirstOrDefault();
            PlannedEmployeeVM.WorkStartTime = PlannedEmployeeVM.PlannedStartTime;
            PlannedEmployeeVM.WorkEndTime = PlannedEmployeeVM.PlannedEndTime;
            PlannedEmployeeVM.Day.InspectorPlannings.Add(PlannedEmployeeVM);
            using (var context = new Entities())
            {
                context.InspectorPlannings.Add(PlannedEmployeeVM.ToModel());
                context.SaveChanges();
            }
            Back();
        }

        private bool CanSave()
        {
            if (PlannedEmployeeVM == null || PlannedEmployeeVM.PlannedStartTime >= PlannedEmployeeVM.PlannedEndTime)
            {
                return false;
            }
            return true;
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        public void GetAvailableInspectors()
        {
            using (var context = new Entities())
            {
                AvailableInspectorList = new ObservableCollection<EmployeeVM>(context.Employees.ToList().Select(employee => new EmployeeVM(employee)).Where(employee => employee.Department.Name == "Inspectie"));
            }
        }
    }
}
