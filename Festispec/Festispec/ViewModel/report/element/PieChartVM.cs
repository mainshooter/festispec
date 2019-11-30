using LiveCharts;
using LiveCharts.Wpf;
﻿using Festispec.Message;
using Festispec.View.Pages.Report.element;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM : ReportElementVM
    {

        public ReportElementVM ReportElementVM { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public PieChartVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(() => Edit());
            Data = element.Data;
            ReportVM = report;
            ReportElementVM = element;
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            ReportId = element.ReportId;
            Order = element.Order;
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTextPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = ReportVM,
                ReportElement = ReportElementVM,
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
