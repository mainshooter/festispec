using Festispec.Domain;
using Festispec.Message;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Festispec.ViewModel.Customer.order;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.toast;

namespace Festispec.ViewModel.planning
{
    public class PlanningOverviewVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filteritems;
        private ObservableCollection<PlannedEmployeeVM> _filteredPlannedEmployeeList;
        private EventVM _selectedEventVM;
        private ToastVM _toastVM;

        public ObservableCollection<PlannedEmployeeVM> FilteredPlannedEmployeeList
        {
            get
            {
                var temp = new ObservableCollection<PlannedEmployeeVM>();

                if (Filter != null)
                {
                    switch (SelectedFilter)
                    {
                        case "Geen filter":
                            temp = _filteredPlannedEmployeeList;
                            break;
                        case "Volledige naam":
                            temp = new ObservableCollection<PlannedEmployeeVM>(_filteredPlannedEmployeeList.Select(i => i).Where(i => i.Employee.Fullname.ToLower().Contains(Filter.ToLower())));
                            break;
                        case "Evenement":
                            temp = new ObservableCollection<PlannedEmployeeVM>(_filteredPlannedEmployeeList.Select(i => i).Where(i => i.Day.Order.Event.Name.ToLower().Contains(Filter.ToLower())));
                            break;
                        case "Status":
                            temp = new ObservableCollection<PlannedEmployeeVM>(_filteredPlannedEmployeeList.Select(i => i).Where(i => i.Status.ToLower().Contains(Filter.ToLower())));
                            break;
                    }
                    if (_showOnlyFuture)
                    {
                        temp = new ObservableCollection<PlannedEmployeeVM>(temp.ToList().Where(i => i.PlannedEndTime >= DateTime.Today).ToList());
                    }
                }
                return temp;
            }
            set
            {
                _filteredPlannedEmployeeList = value;
                RaisePropertyChanged("FilteredPlannedEmployeeList");
            }
        }

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
                RaisePropertyChanged("FilteredPlannedEmployeeList");
            }
        }

        public PlanningOverviewVM()
        {
            _toastVM = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            MessengerInstance.Register<ChangeSelectedEventVM>(this, message => {
                _selectedEventVM = message.NextEvent;
                GetInitialPlannedEmployeeList();
            });

            MessengerInstance.Register<ChangePageMessage>(this, message => {
                Type pageType = message.NextPageType;
                if (pageType == typeof(PlanningOverviewPage))
                {
                    _selectedEventVM = null;
                    GetInitialPlannedEmployeeList();
                }
            });

            FilterItems = new List<string>();
            SelectedFilter = FilterItems.First();
            Filter = "";
        }

        private void GetInitialPlannedEmployeeList() 
        {
            using (var context = new Entities())
            {
                _filteredPlannedEmployeeList = new ObservableCollection<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(i => new PlannedEmployeeVM(i)));
                if (_selectedEventVM != null) 
                {
                    OrderVM orderVM = _selectedEventVM.OrderVM;
                    var result = _filteredPlannedEmployeeList.Where(e => e.OrderId.Equals(orderVM.Id));
                    _filteredPlannedEmployeeList = new ObservableCollection<PlannedEmployeeVM>(result);
                }
                _toastVM.ShowSuccess(_filteredPlannedEmployeeList.Count + " resultaten gefilterd!");
                FilteredPlannedEmployeeList = _filteredPlannedEmployeeList;
            }
        }
    }
}
