using Festispec.Domain;
using Festispec.ViewModel.report;
using Festispec.ViewModel.report.element;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.Repository
{
    public class ReportRepository
    {
        public ObservableCollection<ReportElementVM> GetReportElements(ReportVM report)
        {
            using (var context = new Entities())
            {
                return new ObservableCollection<ReportElementVM>(context.ReportElements.ToList().Where(r => r.ReportId == report.Id).OrderBy(reportElement => reportElement.Order).Select(reportElement => new ReportElementVM(reportElement, report)));
            }

            var reportElements = new List<ReportElementVM>();
            reportElements.Add(
                new ReportElementVM()
                {
                    Title = "hfiuawfhawiuhfawhufiawuifhawuifhiuwahf",
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

            
        }
    }
}
