using Festispec.View.Report;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using Festispec.Singleton;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Festispec.ViewModel.rapport;
using Festispec.ViewModel.rapport.element;
using System.Collections.Generic;
using System;
using LiveCharts;
using LiveCharts.Wpf;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;
        private PageSingleton _pageSingleton;

        //publics
        public ICommand CloseApplication { get; set; }
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenCustomer { get; set; }
        public ICommand OpenAvailability { get; set; }
        public ICommand OpenEvent { get; set; }
        public ICommand OpenSick { get; set; }

        public Page Page
        {
            get { return _page; }
            set { _page = value; RaisePropertyChanged("Page"); }
        }

        //constructor
        public MainViewModel()
        {
            CloseApplication = new RelayCommand(CloseApp);
            OpenDashboard = new RelayCommand(OpenDashboardTab);
            OpenEmployee = new RelayCommand(OpenEmployeeTab);
            OpenCustomer = new RelayCommand(OpenCustomerTab);
            OpenAvailability = new RelayCommand(OpenAvailabilityTab);
            OpenEvent = new RelayCommand(OpenEventTab);
            OpenSick = new RelayCommand(OpenSickTab);
            _pageSingleton = new PageSingleton();
            var report = new ReportPage();
            ReportVM reportVM = new ReportVM();

            reportVM.ReportElements.Add(
                    new ReportElementVM()
                    {
                        Title = "Leuke titel",
                        Content = "Hier maak ik titels van",
                        Type = "table",
                        Data = new Dictionary<string, List<string>>()
                        {
                            ["id"] = new List<string>() { "1", "2" }
                        },
                    }
                );
            reportVM.ReportElements.Add(
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
            reportVM.ReportElements.Add(
                new ReportElementVM()
                {
                    Title = "Line chart",
                    Content = "Wij linecharten",
                    Type = "linechart",
                    Data = new Dictionary<string, Object>() {
                        ["xaxisName"] = "Test xas",
                        ["yaxisName"] = "Test yas",
                        ["seriescollection"] = new SeriesCollection
                        {
                            new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
                        }
                    }
                }
            );
            reportVM.ReportElements.Add(
               new ReportElementVM()
               {
                   Title = "test",
                   Content = "ttest",
                   Type = "barchart",
                   Data = new Dictionary<string, Object>() {
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
            report.DataContext = reportVM;
            reportVM.MainViewModel = this;
            Page = report;

           
        }


        //methodes
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenDashboardTab()
        {
            Page = _pageSingleton.GetPage("dashboard");
        }

        private void OpenEmployeeTab()
        {
            Page = _pageSingleton.GetPage("employee");
        }

        private void OpenCustomerTab()
        {
            Page = _pageSingleton.GetPage("customer");
        }

        private void OpenAvailabilityTab()
        {
            Page = _pageSingleton.GetPage("availability");
        }

        private void OpenEventTab()
        {
            Page = _pageSingleton.GetPage("event");
        }

        private void OpenSickTab()
        {
            Page = _pageSingleton.GetPage("sick");
        }
    }
}
