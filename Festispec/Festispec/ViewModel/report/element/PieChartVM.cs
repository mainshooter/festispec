using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using System;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM : ReportElementVM
    {
        private Object _data;

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
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTextPage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this,
            });
        }

        private void ApplyChanges()
        {
            SeriesCollection = (SeriesCollection)Data;
        }
    }
}
