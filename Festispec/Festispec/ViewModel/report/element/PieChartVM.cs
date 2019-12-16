using LiveCharts;
using LiveCharts.Wpf;
﻿using Festispec.Message;
﻿using Festispec.Lib.Enums;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM : ReportElementVM
    {
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection 
        {
            get {
                return _seriesCollection;
            }
            set 
            {
                _seriesCollection = value;
                RaisePropertyChanged("SeriesCollection");
            }
        }

        public PieChartVM()
        {
            Type = ReportElementType.Piechart;
            EditElement = new RelayCommand(() => Edit());
        }
        public PieChartVM(ReportElementVM element)
        {
            EditElement = new RelayCommand(() => Edit());
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            ReportId = element.ReportId;
            Order = element.Order;
            if (element.DataParser != null)
            {
                DataParser = element.DataParser;
                Data = DataParser.ParseData();
            }
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditPieChartPage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this,
            });
        }

        public void ApplyChanges()
        {
            try
            {
                List<List<string>> dataList = Data;

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
        }
    }
}
