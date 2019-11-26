using Festispec.Domain;
using Festispec.Factory;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddElementVM : ViewModelBase
    {
        private ReportElementFactory _reportElementFactory;
        private string _axes;
        private int _selectedElementIndex;

        public List<string> ElementTypes { get; set; }

        public ReportElementVM ReportElement { get; set; }

        public string Axes
        {
            get
            {
                return _axes;
            }
            set
            {
                _axes = value;
                RaisePropertyChanged("Axes");
            }
        }


        public ReportVM Report { get; set; }

        public int SelectedElementIndex
        {
            get
            {
                return _selectedElementIndex;
            }
            set
            {
                _selectedElementIndex = value;
                RaisePropertyChanged("SelectedElementIndex");
                ChangeInput();
            }
        }

        public ICommand GoBackCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public AddElementVM()
        {
            Axes = "Hidden";
            ReportElement = new ReportElementVM();
            _reportElementFactory = new ReportElementFactory();
            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport);
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                Report = message.NextReportVM;
                ReportElement.ReportId = Report.Id;
                ReportElement.Order = Report.ReportElements.Count + 1;
            });

            
        }
        public void ChangeInput()
        {
            switch (ElementTypes[SelectedElementIndex])
            {
                case "image":
                    var fd = new OpenFileDialog { Filter = "All Image Files | *.*", Multiselect = false };
                    if (fd.ShowDialog() != true) return;
                    using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        var test = new byte[fs.Length];
                        fs.Read(test, 0, Convert.ToInt32(fs.Length));
                    }
                    Axes = "Hidden";
                    break;
                case "barchart":
                    Axes = "Visable";
                    break;
                case "linechart":
                    Axes = "Visable";
                    break;
                default:
                    Axes = "Hidden";
                    break;
            }
        }
        private void AddElementToReport()
        {
            ReportElement.Type = ElementTypes[SelectedElementIndex];
           
            using (var context = new Entities())
            {
                _reportElementFactory.CreateElement(ReportElement, Report);
                context.ReportElements.Add(ReportElement.ToModel());
                context.SaveChanges();
            }
            var userControl = _reportElementFactory.CreateElement(ReportElement, Report);
            Report.ReportElementUserControlls.Add(userControl);
            GoBackToReport();


            //else
            //{

            //    string elementType = ElementTypes[SelectedElementIndex];
            //    var element = new ReportElementVM();
            //    element.Title = "Leuke titel";
            //    element.Content = "Test description";
            //    element.Type = elementType;

            //    if (elementType.Equals("table"))
            //    {
            //        element.Data = new Dictionary<string, List<string>>()
            //        {
            //            ["id"] = new List<string>() { "1", "2" }
            //        };
            //    }
            //    else if (elementType.Equals("linechart"))
            //    {
            //        element.Data = new Dictionary<string, Object>()
            //        {
            //            ["xaxisName"] = "Test xas",
            //            ["yaxisName"] = "Test yas",
            //            ["seriescollection"] = new SeriesCollection
            //        {
            //            new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
            //        }
            //        };
            //    }
            //    else if (elementType.Equals("piechart"))
            //    {
            //        element.Data = new SeriesCollection
            //    {
            //        new PieSeries
            //        {
            //            Title = "Bier",
            //            Values = new ChartValues<double> { 20 },
            //            DataLabels = true,
            //        },
            //        new PieSeries
            //        {
            //            Title = "Frisdrank",
            //            Values = new ChartValues<double> { 12 },
            //            DataLabels = true,
            //        },
            //        new PieSeries
            //        {
            //            Title = "Cocktail",
            //            Values = new ChartValues<double> { 8 },
            //            DataLabels = true,
            //        },
            //        new PieSeries
            //        {
            //            Title = "Wijn",
            //            Values = new ChartValues<double> { 2 },
            //            DataLabels = true,
            //        }
            //    };
            //    }
            //    else if (elementType.Equals("barchart"))
            //    {
            //        element.Data = new Dictionary<string, Object>()
            //        {
            //            ["xaxisName"] = "Place",
            //            ["yaxisName"] = "Amount",
            //            ["labels"] = new List<string> { "test1", "test2", "test3", "test4", "test5" },
            //            ["seriescollection"] = new SeriesCollection
            //        {
            //            new ColumnSeries {Title = "testdata" , Values = new ChartValues<int>{10,20,30,40,50} },

            //            new ColumnSeries {Title = "testdata2" , Values = new ChartValues<int>{15,25,35,45,55} }
            //        }
            //        };
            //    }
            //    else if (elementType.Equals("text"))
            //    {
            //        element.Data = new Dictionary<string, Object>()
            //        {
            //            ["text"] = "test text smiley"
            //        };

            //    }
            //    else if (elementType.Equals("image"))
            //    {
            //        element.Data = new Dictionary<string, Object>()
            //        {
            //            ["image"] = new byte[0]
            //        };
            //    }
            //}

        }

        private void GoBackToReport()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }
    }
}
