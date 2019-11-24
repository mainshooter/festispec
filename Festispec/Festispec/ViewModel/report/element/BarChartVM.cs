using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.ViewModel.report.element
{
    public class BarChartVM : ReportElementVM
    {
        private Object _data;

        public Dictionary<string, Object> Dictionary { get; set; }

        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { set; get; }

        public List<string> Labels { set; get; }

        public Func<double,string> Formatter { set; get;}

        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
            }
        }

        public BarChartVM(ReportElementVM element)
        {
            Data = element.Data;
            Dictionary = new Dictionary<string, Object>();
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            EditElementCommand = new RelayCommand(GoToEdit);
        }

        private void DataToDictonary()
        {
            try
            {
                List<List<string>> dataList = (List<List<string>>)Data;

                var dataCollectionList = new List<List<string>>();
                int columnIndex = 0;
                foreach (var item in dataList)
                {
                    var dataCollection = new List<string>();
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        var row = dataList[i];
                        var column = row[columnIndex];

                        dataCollection.Add(column);
                    }
                    columnIndex++;
                    dataCollectionList.Add(dataCollection);
                }

                var titles = new List<string>();
                foreach (var item in dataCollectionList)
                {
                    titles.Add(item[0]);
                }

                var seriesCollection = new SeriesCollection();
                foreach (var title in titles)
                {
                    seriesCollection.Add(new ColumnSeries() { Title = title});
                }
                foreach (var item in dataCollectionList)
                {
                    item.RemoveAt(0);
                }

                List<List<int>> IntCollection = new List<List<int>>();
                for (int i = 0; i < dataCollectionList.Count; i++)
                {
                    var item = dataCollectionList[i];
                    var intList = item.Select(int.Parse).ToList();
                    IntCollection.Add(intList);
                }
                for (int i = 0; i < seriesCollection.Count; i++)
                {
                    var columnSerie = seriesCollection[i];
                    var chartValues = new ChartValues<int>();
                    foreach (var item in IntCollection[i])
                    {
                        chartValues.Add(item);
                    }
                    columnSerie.Values = chartValues;
                }
                Dictionary["labels"] = new List<string> { "test1", "test2", "test3", "test4", "test5" };
                Dictionary["xaxisName"] = "Place";
                Dictionary["yaxisName"] = "Amount";
                Dictionary["seriescollection"] = seriesCollection;
            }
            catch (Exception)
            {
            }
        }

        public void ApplyChanges()
        {
            DataToDictonary();
            try
            {
                Labels = (List<string>)Dictionary["labels"];
                XaxisName = (string)Dictionary["xaxisName"];
                YaxisName = (string)Dictionary["yaxisName"];
                SeriesCollection = (SeriesCollection)Dictionary["seriescollection"];
            }
            catch (Exception)
            {
            }

        }
    }
}
