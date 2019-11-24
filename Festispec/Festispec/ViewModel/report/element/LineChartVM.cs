using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class LineChartVM : ReportElementVM
    {
        private Object _data;

        public Dictionary<string, Object> Dictionary { get; set; }

        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<string, string> YaxisLabelFormat { get; set; }

        public List<string> Labels { get; set; }

        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
            }
        }

        public LineChartVM(ReportElementVM element)
        {
            Dictionary = new Dictionary<string, Object>();
            Labels = new List<string>();
            Data = element.Data;
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

                var headers = dataList[0];
                foreach (var item in headers)
                {
                    dataCollectionList.Add(new List<string> { item });
                }
                dataList.RemoveAt(0);

                while (dataList.Count > 0)
                {
                    List<string> dataListItem = dataList[0];
                    int index = 0;
                    foreach (var listItem in dataCollectionList)
                    {
                        listItem.Add(dataListItem[index]);
                        index++;
                    }
                    dataList.RemoveAt(0);
                }

                var seriesCollection = new SeriesCollection();
                foreach (var title in headers)
                {
                    seriesCollection.Add(new LineSeries() { Title = title });
                }

                List<List<int>> IntCollection = new List<List<int>>();
                for (int i = 0; i < dataCollectionList.Count; i++)
                {
                    var item = dataCollectionList[i];
                    List<int> intList = new List<int>();
                    for (int j = 0; j < item.Count; j++)
                    {
                        if (j > 0)
                        {
                            intList.Add(int.Parse(item[j]));
                        }
                    }
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
