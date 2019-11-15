using Festispec.Domain;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.planning
{
    public class PlanningOverviewVM : ViewModelBase
    {
        public ObservableCollection<PlannedEmployeeVM> PlannedEmployeeList { get; set; }

        public PlanningOverviewVM()
        {

            using (var context = new Entities())
            {
                //PlannedEmployeeList = new ObservableCollection<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(i => new PlannedEmployeeVM(i)).Where(i => i.PlannedEndTime.Date >= DateTime.Now).ToList());
                PlannedEmployeeList = new ObservableCollection<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(i => new PlannedEmployeeVM(i)));
                FilterItems = new List<string>();
                SelectedFilter = FilterItems.First();
                Filter = "";

                //new ObservableCollection<EmployeeVM>(EmployeeList.Select(employee => employee).Where(employee => employee.Firstname.ToLower().Contains(Filter.ToLower())).ToList());
            }
        }

        public ObservableCollection<PlannedEmployeeVM> FilteredPlannedEmployeeList
        {
            get
            {
                if(Filter != null)
                {
                    switch (SelectedFilter)
                    {
                        case "Geen filter":
                            return PlannedEmployeeList;
                        case "Volledige naam":
                            return new ObservableCollection<PlannedEmployeeVM>(PlannedEmployeeList.Select(i => i).Where(i => i.Employee.Fullname.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Evenement":
                            return new ObservableCollection<PlannedEmployeeVM>(PlannedEmployeeList.Select(i => i).Where(i => i.Day.Order.Event.Name.ToLower().Contains(Filter.ToLower())).ToList());
                        case "Status":
                            return new ObservableCollection<PlannedEmployeeVM>(PlannedEmployeeList.Select(i => i).Where(i => i.Status.ToLower().Contains(Filter.ToLower())).ToList());

                    }
                }
                return PlannedEmployeeList;
            }
        }

        private string _filter;

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                RaisePropertyChanged("FilteredPlannedEmployeeList");
            }
        }

        public string SelectedFilter { get; set; }

        private List<string> _filteritems;

        public List<string> FilterItems
        {
            get
            {
                return _filteritems;
            }
            set
            {
                _filteritems = new List<string>();
                _filteritems.Add("Geen filter");
                _filteritems.Add("Volledige naam");
                _filteritems.Add("Evenement");
                _filteritems.Add("Status");
            }
        }
    }
}
