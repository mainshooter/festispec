using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM: ReportElementVM
    {
        private Object _data;

        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public PieChartVM(ReportElementVM element)
        {
            Title = element.Title;
            Content = element.Content;
            Data = element.Data;
            EditElementCommand = new RelayCommand(GoToEdit);
        }

        private void DataToSeriesCollection()
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
                    seriesCollection.Add(new PieSeries() { Title = title, DataLabels = true, });
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
                SeriesCollection = seriesCollection;
            }
            catch (Exception)
            {
            }
            //    ReportElement.Data = new SeriesCollection
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
        }

        public void ApplyChanges()
        {
            DataToSeriesCollection();
            try
            {
                SeriesCollection = (SeriesCollection)Data;
            }
            catch (Exception)
            {
            }
        }
    }
}
