using Festispec.Message;
using Festispec.View.Pages.Report.element;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class LineChartVM: ReportElementVM
    {
        private Object _data;

        public Dictionary<string, Object> Dictionary { get; set; }

        public string XaxisName { get; set; }

        public string YaxisName { get; set; }

        public SeriesCollection SeriesCollection { get; set; }

        public Func<string, string> YaxisLabelFormat { get; set; }

        public List<string> Labels { get; set; }

        public ReportElementVM ReportElementVM { get; set; }


        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
                Dictionary = (Dictionary<string, Object>) Data;
                ApplyChanges();
            }
        }

        public LineChartVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            Labels = new List<string>();
            //Data = element.Data;
            ReportVM = report;
            ReportElementVM = element;
            Id = element.Id;
            ReportId = element.ReportId;
            Type = element.Type;
            Title = element.Title;
            Order = element.Order;
            Content = element.Content;
            X_as = element.X_as;
            Y_as = element.Y_as;

            Order = element.Order;
        }
        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditLineChartPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = ReportVM,
                ReportElement = ReportElementVM
            });
        }

        private void ApplyChanges()
        {
            XaxisName = (string)Dictionary["xaxisName"];
            YaxisName = (string)Dictionary["yaxisName"];
            SeriesCollection = (SeriesCollection)Dictionary["seriescollection"];
        }
    }
}
