﻿using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using System;
using System.Collections.Generic;

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

        public Func<double, string> Formatter { set; get; }

        public override Object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                Dictionary = (Dictionary<string, Object>)Data;
                ApplyChanges();
            }
        }

        public BarChartVM()
        {
            Type = ReportElementType.Barchart;
        }

        public BarChartVM(ReportElementVM element)
        {
            EditElement = new RelayCommand(() => Edit());
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            ReportId = element.ReportId;
            X_as = element.X_as;
            Y_as = element.Y_as;
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditBarChartPage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this
            });
        }

        private void ApplyChanges()
        {
            Labels = (List<string>)Dictionary["labels"];
            XaxisName = (string)Dictionary["xaxisName"];
            YaxisName = (string)Dictionary["yaxisName"];
            SeriesCollection = (SeriesCollection)Dictionary["seriescollection"];
        }
    }
}
