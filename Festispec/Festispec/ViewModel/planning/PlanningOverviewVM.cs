using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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
        public ICommand AddInspectorCommand { get; set; }
        public string EventName => EventVM?.Name;

        public EventVM EventVM
        {
            get => _eventVM;
            set
            {
                _eventVM = value;
                RaisePropertyChanged(()=> EventVM);
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
            MessengerInstance.Register<ChangeSelectedEventMessage>(this, message =>
            {
                EventVM = message.Event;
                GetInitialPlannedEmployeeList();
            });

            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(PlanningOverviewPage))
                {
                    GetInitialPlannedEmployeeList();
                }
            });
            AddInspectorCommand = new RelayCommand(AddInspector, CanAdd);
            EditInspectorCommand = new RelayCommand<PlannedEmployeeVM>(EditInspector, CanUse);
            DeleteInspectorCommand = new RelayCommand<PlannedEmployeeVM>(DeleteInspector, CanUse);
            BackCommand = new RelayCommand(Back);
            FilterItems = new List<string>();
            SelectedFilter = FilterItems.First();
            Filter = "";
        }

        private void AddInspector()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddPlannedEmployeePage) });
            MessengerInstance.Send<ChangeSelectedPlannedEmployeeEventMessage>(new ChangeSelectedPlannedEmployeeEventMessage()
            {
                EventVM = EventVM
            });
        }

        private void EditInspector(PlannedEmployeeVM source)
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditPlannedEmployeePage) });
            MessengerInstance.Send<ChangeSelectedPlannedEmployeeMessage>(new ChangeSelectedPlannedEmployeeMessage()
            {
                PlannedEmployee = source,
                EventVM = EventVM
            });
        }

        private void DeleteInspector(PlannedEmployeeVM source)
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze inspecteur wilt verwijderen?", "Inspecteur Verwijderen", MessageBoxButton.YesNo);

            if (result.Equals(MessageBoxResult.Yes) && source.EditMessageIfNotWithinWeek(EventVM))
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
                source.Day.InspectorPlannings.Remove(source);
                GetInitialPlannedEmployeeList();
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
            }
            FilteredPlannedEmployeeList = _filteredPlannedEmployeeList;
        }

        private bool CanUse(PlannedEmployeeVM arg)
        {
            if (arg == null || arg.PlannedStartTime < DateTime.Today)
            {
                return false;
            }
            return true;
        }

        private bool CanAdd()
        {
            if (EventVM == null || EventVM.EndDate < DateTime.Today)
            {
                return false;
            }
            return true;
        }
    }
}
