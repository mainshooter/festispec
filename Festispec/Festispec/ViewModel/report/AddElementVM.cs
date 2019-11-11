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
            ElementTypes = new List<string>() { "table", "linechart", "piechart", "barchart"};
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
                Table tableUserControl = new Table();
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
                LineChart lineChartUserControl = new LineChart();
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
                View.Report.Element.PieChart pieChartUserControl = new View.Report.Element.PieChart();
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
            else if (elementType.Equals("barchart"))
            {
                BarChart barChartUserControl = new BarChart();
                BarChartVM barChartVM = new BarChartVM();
                barChartVM.Title = "PleaseWork";
                barChartVM.Content = "Random stuff";
                barChartVM.SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "test",
                        Values = new ChartValues<double>{20,40,50,78}
                    }
                };
                barChartVM.SeriesCollection.Add(new ColumnSeries
                {
                    Title = "test2",
                    Values = new ChartValues<double> {10,20,25,39}
                });
                barChartVM.Labels.Add("testname");
                barChartVM.Labels.Add("testname1");
                barChartVM.Labels.Add("testname2");
                barChartVM.Labels.Add("testname3");
                barChartVM.Formatter = value => value.ToString("N");
                barChartUserControl.DataContext = barChartVM;
                Report.ReportElementUserControlls.Add(barChartUserControl);
            }
            GoBackToReport();
        }

        private void GoBackToReport()
        {
            MainViewModel.Page = _previousePage;
        }
    }
}
