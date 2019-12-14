using Festispec.Message;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using Festispec.Domain;

namespace Festispec.ViewModel.planning
{
    public class PlanningOverviewVM : ViewModelBase
    {
        private string _filter;
        private List<string> _filteritems;
        private ObservableCollection<PlannedEmployeeVM> _filteredPlannedEmployeeList;
        private bool _showOnlyFuture;
        private EventVM _eventVM;

        public string SelectedFilter { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand EditInspectorCommand { get; set; }
        public ICommand DeleteInspectorCommand { get; set; }
        public string EventName => EventVM?.Name;

        public EventVM EventVM
        {
            get => _eventVM;
            set
            {
                _eventVM = value;
                RaisePropertyChanged(() => EventName);
            }
        }

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
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged("FilteredPlannedEmployeeList");
            }
        }

        public List<string> FilterItems
        {
            get => _filteritems;
            set
            {
                _filteritems = new List<string>();
                _filteritems.Add("Geen filter");
                _filteritems.Add("Volledige naam");
                _filteritems.Add("Evenement");
                _filteritems.Add("Status");
            }
        }

        public bool ShowOnlyFuture
        {
            get => _showOnlyFuture;
            set
            {
                _showOnlyFuture = value;
                RaisePropertyChanged("FilteredPlannedEmployeeList");
            }
        }

        public PlanningOverviewVM()
        {
            MessengerInstance.Register<ChangeSelectedEventMessage>(this, message => {
                EventVM = message.Event;
                GetInitialPlannedEmployeeList();
            });

            MessengerInstance.Register<ChangePageMessage>(this, message => {
                if (message.NextPageType == typeof(PlanningOverviewPage))
                {
                    GetInitialPlannedEmployeeList();
                }
            });
            EditInspectorCommand = new RelayCommand<PlannedEmployeeVM>(EditInspector);
            DeleteInspectorCommand = new RelayCommand<PlannedEmployeeVM>(DeleteInspector);
            BackCommand = new RelayCommand(Back);
            FilterItems = new List<string>();
            SelectedFilter = FilterItems.First();
            Filter = "";
        }

        private void EditInspector(PlannedEmployeeVM source)
        {
            EventVM.RaisePropertyChangedUniversalDate();
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditPlannedEmployeePage) });
            MessengerInstance.Send<ChangeSelectedPlannedEmployeeMessage>(new ChangeSelectedPlannedEmployeeMessage()
            {
                PlannedEmployee = source,
                PlannedEmployeesList = _filteredPlannedEmployeeList,
                EventVM = EventVM               
            }) ;
        }

        private void DeleteInspector(PlannedEmployeeVM source)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze inspecteur wilt verwijderen?", "Inspecteur Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    var temp = source.ToModel();
                    context.InspectorPlannings.Remove(context.InspectorPlannings.Select(ins => ins).Where(ins => ins.EmployeeId == temp.EmployeeId)
                                                                                                   .Where(ins => ins.DayId == temp.DayId)
                                                                                                   .Where(ins => ins.OrderId == temp.OrderId)
                                                                                                   .First());
                    context.SaveChanges();
                }
                _filteredPlannedEmployeeList.Remove(source);
                RaisePropertyChanged("EventListFiltered");
            }
        }

        private void Back()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        private void GetInitialPlannedEmployeeList() 
        {
            if (EventVM != null)
            {
                _filteredPlannedEmployeeList = new ObservableCollection<PlannedEmployeeVM>();

                foreach (var day in EventVM.OrderVM.Days)
                {
                    foreach (var inspectorPlanning in day.InspectorPlannings)
                    {
                        _filteredPlannedEmployeeList.Add(inspectorPlanning);
                    }
                }

                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess(_filteredPlannedEmployeeList.Count + " resultaten gefilterd!");
            }

            FilteredPlannedEmployeeList = _filteredPlannedEmployeeList;
        }
    }
}
