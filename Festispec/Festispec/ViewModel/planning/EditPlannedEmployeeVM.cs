using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.employee.availabilty;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.planning
{
    public class EditPlannedEmployeeVM : ViewModelBase
    {
        private PlannedEmployeeVM _plannedEmployeeVM;
        private EventVM _eventVM;
        private DateTime _plannedStartTime;
        private DateTime _plannedEndTime;

        public ICommand BackCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
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

        public EditPlannedEmployeeVM()
        {
            AvailabilityList = new List<AvailabiltyVM>();
            this.MessengerInstance.Register<ChangeSelectedPlannedEmployeeMessage>(this, message =>
            {
                PlannedEmployeeVM = message.PlannedEmployee;
                EventVM = message.EventVM;
                GetAvailability();
                PlannedEmployeeStartTime = PlannedEmployeeVM.PlannedStartTime;
                PlannedEmployeeEndTime = PlannedEmployeeVM.PlannedEndTime;
                _plannedStartTime = PlannedEmployeeVM.PlannedStartTime;
                _plannedEndTime = PlannedEmployeeVM.PlannedEndTime;
            });
            BackCommand = new RelayCommand(Back);
            SaveChangesCommand = new RelayCommand(EditPlannedEmployee, CanSave);
        }

        private bool CanSave()
        {
            if (PlannedEmployeeVM == null || PlannedEmployeeVM.PlannedStartTime >= PlannedEmployeeVM.PlannedEndTime)
            {
                return false;
            }
            return true;
        }

        private void EditPlannedEmployee()
        {
            PlannedEmployeeVM.WorkStartTime = PlannedEmployeeVM.PlannedStartTime;
            PlannedEmployeeVM.WorkEndTime = PlannedEmployeeVM.PlannedEndTime;
            using (var context = new Entities())
            {
                context.InspectorPlannings.Attach(PlannedEmployeeVM.ToModel());
                context.Entry(PlannedEmployeeVM.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        private void Back()
        {
            PlannedEmployeeVM.PlannedStartTime = _plannedStartTime;
            PlannedEmployeeVM.PlannedEndTime = _plannedEndTime;
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        public void GetAvailability()
        {
            using (var context = new Entities())
            {
                AvailabilityList = new List<AvailabiltyVM>(context.AvailabilityInspectors.ToList().Select(availableInspector => new AvailabiltyVM(availableInspector)).Where(availableInspector => availableInspector.AvailabiltyStart.Value.Date == PlannedEmployeeStartTime.Date && availableInspector.AvailabiltyEnd.Value >= PlannedEmployeeStartTime));
            }
        }
    }
}
