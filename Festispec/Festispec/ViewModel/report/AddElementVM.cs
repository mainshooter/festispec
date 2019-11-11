using Festispec.View.Report.Element;
using Festispec.ViewModel.rapport;
using Festispec.ViewModel.rapport.element;
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
            string elementType = ElementTypes[SelectedElementIndex];
            if (elementType.Equals("table"))
            {
                TableUserControl tableUserControl = new TableUserControl();
                var element = new ReportElementVM()
                {
                    Title = "Leuke titel",
                    Content = "Hier maak ik titels van",
                    Type = "table",
                    Data = new Dictionary<string, List<string>>()
                    {
                        ["id"] = new List<string>() { "1", "2" }
                    },
                };
                TableVM tableVM = new TableVM(element);
                tableUserControl.DataContext = tableVM;
                Report.ReportElementUserControlls.Add(tableUserControl);
            }
            else if (elementType.Equals("linechart"))
            {
                LineChartUserControl lineChartUserControl = new LineChartUserControl();
                var element = new ReportElementVM()
                {
                    Title = "Line chart",
                    Content = "Wij linecharten",
                    Type = "linechart",
                    Data = new Dictionary<string, Object>()
                    {
                        ["xaxisName"] = "Test xas",
                        ["yaxisName"] = "Test yas",
                        ["seriescollection"] = new SeriesCollection
                        {
                            new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
                        }
                    }
                };
                LineChartVM lineChartVM = new LineChartVM(element);
                lineChartUserControl.DataContext = lineChartVM;
                Report.ReportElementUserControlls.Add(lineChartUserControl);
            }
            else if (elementType.Equals("piechart"))
            {
                PieChartUserControl pieChartUserControl = new PieChartUserControl();
                var element = new ReportElementVM()
                {
                    Title = "Leuke piechart",
                    Content = "Taartje beschrijving",
                    Type = "piechart",
                    Data = new SeriesCollection
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
                    }
                };
                PieChartVM pieChartVM = new PieChartVM(element);
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
