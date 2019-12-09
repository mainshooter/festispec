using Festispec.Message;
using Festispec.View.Pages.Report.element;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using System;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM : ReportElementVM
    {
        private Object _data;

        public ReportElementVM ReportElementVM { get; set; }

        public override Object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                ApplyChanges();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public PieChartVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            //Data = element.Data;
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
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = ReportElementVM
            });
        }

        private void ApplyChanges()
        {
            SeriesCollection = (SeriesCollection)Data;
        }
    }
}
