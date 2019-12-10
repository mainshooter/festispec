using Festispec.Message;
using Festispec.View.Pages.Report.element;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        private object _data;

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
            }
        }

        public ReportElementVM ReportElementVM { get; set; }

        public Dictionary<string, Object> Dictionary { get; set; }

        public TextVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            ReportVM = report;
            ReportElementVM = element;
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            ReportId = element.ReportId;
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTextPage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = ReportElementVM
            });
        }
    }
}
