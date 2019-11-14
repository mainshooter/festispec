using Festispec.Domain;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.planning
{
    public class PlanningOverviewVM : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public ObservableCollection<PlannedEmployeeVM> PlannedEmployeeList { get; set; }

        public PlanningOverviewVM(MainViewModel mainViewModel)
        {
            this._mainViewModel = mainViewModel;

            using (var context = new Entities())
            {
                PlannedEmployeeList = new ObservableCollection<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(i => new PlannedEmployeeVM(i)));
            }
        }
    }
}
