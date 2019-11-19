using LiveCharts;
using System;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM: ReportElementVM
    {
        private Object _data;

        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
                ApplyChanges();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public PieChartVM(ReportElementVM element)
        {
            Title = element.Title;
            Content = element.Content;
            Data = element.Data;
            EditElementCommand = new RelayCommand(GoToEdit);
        }

        private void ApplyChanges()
        {
            SeriesCollection = (SeriesCollection)Data;
        }
    }
}
