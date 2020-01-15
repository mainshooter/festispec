using System;
using System.Collections.ObjectModel;
using System.Linq;
using Festispec.Domain;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.employee.Inspector
{
    public class InspectorInfoVM : ViewModelBase
    {
        private DateTime _beginDateDateUniversal;
        private DateTime _endDateDateUniversal;

        public ObservableCollection<EmployeeVM> Inspectors { get; set; }

        public DateTime SelectedBeginDate
        {
            get => _beginDateDateUniversal;
            set
            {
                _beginDateDateUniversal = value;
                FillInspectorPlanning();
                FillInspectorSick();
                FillNumberWorkedHoures();
            }
        }

        public DateTime SelectedEndDate
        {
            get => _endDateDateUniversal;
            set
            {
                _endDateDateUniversal = value;
                FillInspectorPlanning();
                FillInspectorSick();
                FillNumberWorkedHoures();
            }
        }

        public InspectorInfoVM()
        {
            FillInspectors();
            FillInspectorPlanning();
            FillInspectorSick();
            SelectedBeginDate = DateTime.Today;
            SelectedEndDate = DateTime.Today;
        }

        private void FillInspectors()
        {
            using (var context = new Entities())
            {
                Inspectors = new ObservableCollection<EmployeeVM>(context.Employees.Where(i => i.Department == "Inspectie").ToList().Select(i => new EmployeeVM(i)));
            }
        }

        private void FillInspectorPlanning()
        {
            foreach (var inspector in Inspectors)
            {
                using (var context = new Entities())
                {
                    inspector.AmountPlanned = context.InspectorPlannings.Count(i => i.EmployeeId == inspector.Id && i.PlannedFrom >= SelectedBeginDate && i.WorkedTill <= SelectedEndDate);
                }
            }
        }

        private void FillInspectorSick()
        {
            foreach (var inspector in Inspectors)
            {
                using (var context = new Entities())
                {
                    inspector.AmountSick = context.SickReportInspectors.Count(i => i.EmployeeId == inspector.Id && i.InspectorPlanning.PlannedFrom >= SelectedBeginDate && i.InspectorPlanning.WorkedTill <= SelectedEndDate);
                }
            }
        }

        private void FillNumberWorkedHoures()
        {
            foreach (var inspector in Inspectors)
            {
                using (var context = new Entities())
                {
                    inspector.WorkedHoures = 0;
                    var inspectorPlanning = context.InspectorPlannings.Where(i => i.EmployeeId == inspector.Id && i.WorkedFrom >= SelectedBeginDate && i.WorkedTill <= SelectedEndDate).ToList();

                    foreach (var planning in inspectorPlanning)
                    {
                        if (planning.WorkedTill != null && planning.WorkedFrom != null)
                        {
                            var temp = planning.WorkedTill.Value - planning.WorkedFrom.Value;
                            inspector.WorkedHoures += (int)temp.TotalHours;
                            RaisePropertyChanged(() => Inspectors);
                        }
                    }
                }
            }
        }
    }
}
