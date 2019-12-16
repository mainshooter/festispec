using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Data.Entity;
using System.Windows.Input;

namespace Festispec.ViewModel.planning
{
    public class EditPlannedEmployeeVM : ViewModelBase
    {
        private PlannedEmployeeVM _plannedEmployeeVM;

        public ICommand BackCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }

        private EventVM _eventVM { get; set; }

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
            this.MessengerInstance.Register<ChangeSelectedPlannedEmployeeMessage>(this, message =>
            {
                PlannedEmployeeVM = message.PlannedEmployee;
                EventVM = message.EventVM;
            });
            BackCommand = new RelayCommand(Back);
            SaveChangesCommand = new RelayCommand(EditPlannedEmployee);
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
            Back();
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }
    }
}
