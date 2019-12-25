using Festispec.Domain;
using Festispec.Message;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.planning.plannedEmployee;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.planning
{
    public class WorkedPlannedEmployeeVM : ViewModelBase
    {
        private ObservableCollection<PlannedEmployeeVM> _workedList;
        private PlannedEmployeeVM _selectedPlannedEmployeeVM;
        private DateTime _selectedStartTime;
        private DateTime _selectedEndTime;
        private List<string> _filterItems;
        private EmployeeVM _employeeVM;
        private string _selectedFilter;

        public ICommand EditCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeselectCommand { get; set; }

        public EmployeeVM EmployeeVM
        {
            get => _employeeVM;
            set
            {
                _employeeVM = value;
                RaisePropertyChanged(() => EmployeeVM);
            }
        }

        public List<string> FilterItems
        {
            get => _filterItems;
            set
            {
                _filterItems = new List<string>();
                _filterItems.Add("Afgelopen week");
                _filterItems.Add("Afgelopen maand");
                _filterItems.Add("Afgelopen jaar");
            }
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                RaisePropertyChanged(() => WorkedList);
            }
        }

        public DateTime SelectedStartTime
        {
            get => _selectedStartTime;
            set
            {
                if (value > SelectedEndTime)
                {
                    SelectedStartTime = SelectedEndTime;
                    return;
                }
                _selectedStartTime = value;
                RaisePropertyChanged(() => SelectedStartTime);
            }
        }

        public DateTime SelectedEndTime
        {
            get => _selectedEndTime;
            set
            {
                if (value < SelectedStartTime)
                {
                    SelectedEndTime = SelectedStartTime;
                    return;
                }
                _selectedEndTime = value;
                RaisePropertyChanged(() => SelectedEndTime);
            }
        }

        public Visibility ShowSelectedDay
        {
            get
            {
                if (SelectedPlannedEmployeeVM == null)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public ObservableCollection<PlannedEmployeeVM> WorkedList
        {
            get
            {
                if (_workedList != null)
                {
                    var temp = new ObservableCollection<PlannedEmployeeVM>();

                    switch (SelectedFilter)
                    {
                        case "Afgelopen week":
                            temp = new ObservableCollection<PlannedEmployeeVM>(_workedList.Select(i => i).Where(i => i.WorkStartTime > DateTime.Now.Date.AddDays(-7)));
                            break;
                        case "Afgelopen maand":
                            temp = new ObservableCollection<PlannedEmployeeVM>(_workedList.Select(i => i).Where(i => i.WorkStartTime > DateTime.Now.Date.AddDays(-31)));
                            break;
                        case "Afgelopen jaar":
                            temp = new ObservableCollection<PlannedEmployeeVM>(_workedList.Select(i => i).Where(i => i.WorkStartTime > DateTime.Now.Date.AddDays(-365)));
                            break;
                    }
                    return temp;
                }
                return _workedList;
            }
            set
            {
                _workedList = value;
                RaisePropertyChanged(() => WorkedList);
            }
        }

        public PlannedEmployeeVM SelectedPlannedEmployeeVM
        {
            get => _selectedPlannedEmployeeVM;
            set
            {
                _selectedPlannedEmployeeVM = value;
                RaisePropertyChanged(() => SelectedPlannedEmployeeVM);
            }
        }

        public WorkedPlannedEmployeeVM()
        {
            this.MessengerInstance.Register<ChangeSelectedEmployeeMessage>(this, message =>
            {
                EmployeeVM = message.Employee;
                FillWorkedList();
            });
            EditCommand = new RelayCommand<PlannedEmployeeVM>(EditWorkedTime, CanEdit);
            SaveCommand = new RelayCommand(SaveWorkedTime, CanSave);
            DeselectCommand = new RelayCommand(Deselect);
            FilterItems = new List<string>();
            SelectedFilter = FilterItems.First();
        }

        private void Deselect()
        {
            SelectedPlannedEmployeeVM = null;
            RaisePropertyChanged(()=> ShowSelectedDay);
        }

        private void SaveWorkedTime()
        {
            SelectedPlannedEmployeeVM.WorkStartTime = SelectedStartTime;
            SelectedPlannedEmployeeVM.WorkEndTime = SelectedEndTime;
            using (var context = new Entities())
            {
                context.InspectorPlannings.Attach(SelectedPlannedEmployeeVM.ToModel());
                context.Entry(SelectedPlannedEmployeeVM.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            SelectedPlannedEmployeeVM = null;
            RaisePropertyChanged(() => ShowSelectedDay);
            RaisePropertyChanged(() => WorkedList);
        }

        private void EditWorkedTime(PlannedEmployeeVM source)
        {
            SelectedStartTime = DateTime.Now;
            SelectedEndTime = DateTime.Now;
            SelectedPlannedEmployeeVM = source;
            SelectedStartTime = source.WorkStartTime;
            SelectedEndTime = source.WorkEndTime;
            RaisePropertyChanged(() => ShowSelectedDay);
        }

        public void FillWorkedList()
        {
            using (var context = new Entities())
            {
                WorkedList = new ObservableCollection<PlannedEmployeeVM>(context.InspectorPlannings.ToList().Select(workedHours => new PlannedEmployeeVM(workedHours)).Where(workedHours => workedHours.Employee.Id == EmployeeVM.Id && workedHours.WorkStartTime < DateTime.Now));
            }
        }

        private bool CanSave()
        {
            if (SelectedPlannedEmployeeVM == null || SelectedStartTime >= SelectedEndTime)
            {
                return false;
            }
            return true;
        }

        private bool CanEdit(PlannedEmployeeVM source)
        {
            if (source == null || (DateTime.Now - source.PlannedStartTime).TotalDays > 7)
            {
                return false;
            }
            return true;
        }
    }
}
