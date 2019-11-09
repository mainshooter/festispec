using Festispec.Domain;
using Festispec.View.Report.Element;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.rapport.element;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.rapport
{
    public class ReportVM
    {
        private Report _report;

        public int Id {
            get {
                return _report.Id;
            }
            private set {
                _report.Id = value;
            }
        }

        public OrderVM Order { get; set; }

        public string Title {
            get {
                return _report.Title;
            }
            set {
                _report.Title = value;
            }
        }

        public string Status {
            get {
                return _report.Status;
            }
            set {
                _report.Status = value;
            }
        }

        public ObservableCollection<ReportElementVM> ReportElements { get; set; }
        private ObservableCollection<UserControl> _reportElementUserControlls;
        public ObservableCollection<UserControl> ReportElementUserControlls {
            get {
                return _reportElementUserControlls;
            }
            set {
                _reportElementUserControlls = value;
            }
        }

        public ICommand SaveReportCommand { get; set; }

        public ReportVM(Report report)
        {
            _report = report;
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            ReportElements = new ObservableCollection<ReportElementVM>(report.ReportElements.ToList().Select(e => new ReportElementVM(e)));
            ReportElements.CollectionChanged += RenderReportElements;
            SaveReportCommand = new RelayCommand(Save);
        }

        public ReportVM()
        {
            _report = new Report();
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            ReportElements = new ObservableCollection<ReportElementVM>();
            ReportElements.CollectionChanged += RenderReportElements;
            SaveReportCommand = new RelayCommand(Save);
        }

        public Report ToModel()
        {
            return _report;
        }

        public void Save()
        {
            if (Id == 0)
            {
                Insert();
                return;
            }
            using (var context = new Entities())
            {
                context.Reports.Attach(_report);
                context.Entry(_report).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        private void Insert()
        {
            using (var context = new Entities())
            {
                context.Reports.Add(_report);
                context.SaveChanges();
            }
        }

        public void RenderReportElements(object sender, NotifyCollectionChangedEventArgs e)
        {
            ReportElementUserControlls.Clear();
            foreach (var element in ReportElements)
            {
                if (element.Type.Equals("table"))
                {
                    Table tableUserControl = new Table();
                    TableVM tableVM = new TableVM();
                    tableVM.Title = element.Title;
                    tableVM.Content = element.Content;
                    tableVM.Dictionary.Add("id", new List<string>() { "1", "2" });
                    tableVM.ApplyChanges();
                    tableUserControl.DataContext = tableVM;
                    ReportElementUserControlls.Add(tableUserControl);
                }
                else if (element.Type.Equals("linechart"))
                {
                    LineChart lineChartUserControl = new LineChart();
                    LineChartVM lineChartVM = new LineChartVM();
                    lineChartVM.Title = element.Title;
                    lineChartVM.Content = element.Content;
                    lineChartVM.XaxisName = "Test xas";
                    lineChartVM.YaxisName = "Test y";
                    lineChartVM.SeriesCollection = new SeriesCollection
                    {
                        new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
                    };
                    lineChartVM.SeriesCollection.Add(new LineSeries { Title = "Test 2", Values = new ChartValues<int> { 44, 587, 10, 5, 9, 60 } });
                    lineChartUserControl.DataContext = lineChartVM;
                    ReportElementUserControlls.Add(lineChartUserControl);
                }
                else if (element.Type.Equals("piechart"))
                {
                    View.Report.Element.PieChart pieChartUserControl = new View.Report.Element.PieChart();
                    PieChartVM pieChartVM = new PieChartVM();
                    pieChartVM.Title = element.Title;
                    pieChartVM.Content = element.Content;
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
                    ReportElementUserControlls.Add(pieChartUserControl);
                }
            }
        }
    }
}
