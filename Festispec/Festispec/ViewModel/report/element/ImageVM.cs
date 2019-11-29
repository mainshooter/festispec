using Festispec.Message;
using Festispec.View.Pages.Report.element;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    class ImageVM : ReportElementVM
    {
        private object _data;

        public byte[] Photo { get; set; }

        public override Object Data
        {
            get {
                return _data;
            }
            set {
                _data = value;
                Dictionary = (Dictionary<string, Object>)Data;
                ApplyChanges();
            }
        }

        public Dictionary<string, Object> Dictionary { get; set; }
        public ReportElementVM ReportElementVM { get; set; }

        public ImageVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            //Data = element.Data;
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
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditImagePage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = ReportVM,
                ReportElement = ReportElementVM
            });
        }

        private void ApplyChanges()
        {
            Photo = (byte[])Dictionary["image"];
        }
    }
}
