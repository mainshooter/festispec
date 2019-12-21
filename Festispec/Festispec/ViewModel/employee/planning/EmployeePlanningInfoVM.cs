using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee.Planning;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.employee.planning
{
    public class EmployeePlanningInfoVM : ViewModelBase
    {
        private EmployeeVM _loggedinEmployee;
        private ObservableCollection<PlannedEmployeeVM> _allInspectorPlannings;
        private ObservableCollection<PlannedEmployeeVM> _employeePlanning;
        private bool _showOnlyFuture;

        public bool ShowOnlyFuture 
        {
            get 
            {
                return _showOnlyFuture;
            }
            set 
            {
                _showOnlyFuture = value;
                RaisePropertyChanged("EmployeePlanning");
            }
        }

        public ObservableCollection<PlannedEmployeeVM> EmployeePlanning 
        {
            get 
            {
                if (_employeePlanning == null)
                {
                    return _employeePlanning;
                }


                if (_showOnlyFuture)
                {
                    _employeePlanning = new ObservableCollection<PlannedEmployeeVM>(_allInspectorPlannings.ToList().Where(i => i.PlannedEndTime >= DateTime.Today).OrderBy(e => e.PlannedStartTime).ToList());
                }
                else
                {
                    _employeePlanning = _allInspectorPlannings;
                }

                return _employeePlanning;
            }
            set 
            {
                _employeePlanning = value;
                RaisePropertyChanged("EmployeePlanning");
            }
        }

        public EmployeePlanningInfoVM()
        {
            _loggedinEmployee = UserSessionVM.Current.Employee;
            MessengerInstance.Register<ChangePageMessage>(this, message => 
            { 
                if (message.NextPageType == typeof(EmployeePlanningPage))
                {
                    _loggedinEmployee = UserSessionVM.Current.Employee;
                    ShowOnlyFuture = true;
                    FillData();
                }
            });
            ShowOnlyFuture = true;
            FillData();
        }

        private void FillData()
        {
            if (_loggedinEmployee == null)
            {
                return;
            }
            using (var context = new Entities())
            {
                var inspectorPlanningList = context.InspectorPlannings.ToList().Select(p => new PlannedEmployeeVM(p)).Where(p => p.Employee.Id == _loggedinEmployee.Id);
                _allInspectorPlannings = new ObservableCollection<PlannedEmployeeVM>(inspectorPlanningList);
                EmployeePlanning = _allInspectorPlannings;
            }
        }
    }
}
