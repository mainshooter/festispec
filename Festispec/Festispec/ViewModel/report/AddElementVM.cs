using Festispec.View.Report.Element;
using Festispec.ViewModel.rapport;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddElementVM: ViewModelBase
    {
        private Page _previousePage;
        private MainViewModel _mainViewModel;
        public List<string> ElementTypes { get; set; }
        public ReportVM Report { get; set; }
        public MainViewModel MainViewModel {
            get {
                return _mainViewModel;
            }
            set {
                _mainViewModel = value;
                _previousePage = _mainViewModel.Page;
            }
        }
        public int SelectedElementIndex { get; set; }

        public ICommand GoBackCommand { get; set; }
        public ICommand AddElementCommand { get; set; }

        public AddElementVM()
        {
            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport);
        }

        private void AddElementToReport()
        {
            if (SelectedElementIndex == null)
            {
                return;
            }
            string elementType = ElementTypes[SelectedElementIndex];
            if (elementType.Equals("table"))
            {
                TableUserControl tableUserControl = new TableUserControl();
                TableVM tableVM = new TableVM();
                tableVM.Title = "Test tabelletje";
                tableVM.Content = "Doei doei doei";
                tableVM.Dictionary.Add("id", new List<string>() { "1", "2" });
                tableVM.ApplyChanges();
                tableUserControl.DataContext = tableVM;
                Report.ReportElementUserControlls.Add(tableUserControl);
            }
            else if (elementType.Equals("linechart"))
            {
                LineChartUserControl lineChartUserControl = new LineChartUserControl();
                LineChartVM lineChartVM = new LineChartVM();
                lineChartVM.Title = "Doei";
                lineChartVM.Content = "Content doei doei";
                lineChartVM.XaxisName = "Test xas";
                lineChartVM.YaxisName = "Test y";
                lineChartVM.SeriesCollection = new SeriesCollection
                    {
                        new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
                    };
                lineChartVM.SeriesCollection.Add(new LineSeries { Title = "Test 2", Values = new ChartValues<int> { 44, 587, 10, 5, 9, 60 } });
                lineChartUserControl.DataContext = lineChartVM;
                Report.ReportElementUserControlls.Add(lineChartUserControl);
            }
            else if (elementType.Equals("piechart"))
            {
                PieChartUserControl pieChartUserControl = new PieChartUserControl();
                PieChartVM pieChartVM = new PieChartVM();
                pieChartVM.Title = "Test";
                pieChartVM.Content = "Lorem ipsum";
                pieChartVM.SeriesCollection = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Bier",
                            Values = new ChartValues<double> { 20 },
                            DataLabels = true,
                        },
                        new PieSeries
                        {
                            Title = "Frisdrank",
                            Values = new ChartValues<double> { 12 },
                            DataLabels = true,
                        },
                        new PieSeries
                        {
                            Title = "Cocktail",
                            Values = new ChartValues<double> { 8 },
                            DataLabels = true,
                        },
                        new PieSeries
                        {
                            Title = "Wijn",
                            Values = new ChartValues<double> { 2 },
                            DataLabels = true,
                        }
                    };
                pieChartUserControl.DataContext = pieChartVM;
                Report.ReportElementUserControlls.Add(pieChartUserControl);
            }
            GoBackToReport();
        }

        private void GoBackToReport()
        {
            MainViewModel.Page = _previousePage;
        }
    }
}
