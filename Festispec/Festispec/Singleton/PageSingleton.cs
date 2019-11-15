using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Employee;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee.Availability;
using LiveCharts.Wpf;
using LiveCharts;
using Festispec.ViewModel.report.element;
using System;
using Festispec.ViewModel.report;
using Festispec.View.Report;
using Festispec.ViewModel;

namespace Festispec.Singleton
{
    public class PageSingleton
    {
        private Dictionary<string, Page> _pages;

        public PageSingleton()
        {          
            _pages = new Dictionary<string, Page>();
            _pages.Add("dashboard", new DashboardPage());
            _pages.Add("employee", new EmployeePage());
            _pages.Add("customer", new CustomerPage());
            _pages.Add("availability", new AvailablePage());
            _pages.Add("event", new EventPage());
            _pages.Add("sick", new SickPage());
            //AddReportPage(mainViewModel);
        }

        public IEnumerable<ReportElementVM> CurrentReportElements
        {
            get {
                //var report = new ReportPage();
                //ReportVM reportVM = new ReportVM();
                var reportElements = new List<ReportElementVM>();
                reportElements.Add(
                        new ReportElementVM()
                        {
                            Title = "Leuke titel",
                            Content = "Hier maak ik titels van",
                            Type = "table",
                            Data = new Dictionary<string, List<string>>()
                            {
                                ["id"] = new List<string>() { "1", "2" },
                                ["namen"] = new List<string>() { "Peter", "Mike", "Wout" }
                            },
                        }
                    );
                reportElements.Add(
                    new ReportElementVM()
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
                    }
                );
                reportElements.Add(
                    new ReportElementVM()
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
                    }
                );
                reportElements.Add(
                   new ReportElementVM()
                   {
                       Title = "test",
                       Content = "ttest",
                       Type = "barchart",
                       Data = new Dictionary<string, Object>()
                       {
                           ["xaxisName"] = "Place",
                           ["yaxisName"] = "Amount",
                           ["labels"] = new List<string> { "test1", "test2", "test3", "test4", "test5" },
                           ["seriescollection"] = new SeriesCollection
                           {
                            new ColumnSeries {Title = "testdata" , Values = new ChartValues<int>{10,20,30,40,50} },

                            new ColumnSeries {Title = "testdata2" , Values = new ChartValues<int>{15,25,35,45,55} }
                           }
                       }
                   }
                );
                reportElements.Add(
                    new ReportElementVM()
                    {
                        Title = "text",
                        Content = "test text",
                        Type = "text",
                        Data = new Dictionary<string, Object>()
                        {
                            ["text"] = "test text smiley"
                        }
                    }
                );
                reportElements.Add(
                    new ReportElementVM()
                    {
                        Title = "image",
                        Content = "local image",
                        Type = "image",
                        Data = new Dictionary<string, Object>()
                        {
                            ["image"] = new byte[0]
                        }
                    }
                );
                // reportVM.MainViewModel = mainViewModel;
                //report.DataContext = reportVM;

                //_pages.Add("report", report);
                return reportElements;
            }
        }

        public Page GetPage(string pageName)
        {
            var result = _pages.Where(p => p.Key.Equals(pageName));
            return result.FirstOrDefault().Value;
        }
    }
}
