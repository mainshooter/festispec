
using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Employee.Planning;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.employee.planning
{
    public class EmployeePlanningInfoVM : ViewModelBase
    {
        private EmployeeVM _loggedinEmployee;
        private List<PlannedEmployeeVM> _allInspectorPlannings;
        private ObservableCollection<PlannedEmployeeVM> _employeePlanning;

        public ObservableCollection<PlannedEmployeeVM> EmployeePlanning 
        {
            get 
            {
                return _employeePlanning;
            }
            set {
                _employeePlanning = value;
                RaisePropertyChanged("EmployeePlanning");
            }
        }

        public EmployeePlanningInfoVM()
        {
            _loggedinEmployee = UserSessionVM.Current.Employee;
            MessengerInstance.Register<ChangePageMessage>(this, message => { 
                if (message.NextPageType == typeof(EmployeePlanningPage))
                {
                    _loggedinEmployee = UserSessionVM.Current.Employee;
                    FillData();
                }
            });
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
                _allInspectorPlannings = context.InspectorPlannings.Where(p => p.EmployeeId == _loggedinEmployee.Id).Select(p => new PlannedEmployeeVM(p)).ToList();
            }
        }
    }
}
