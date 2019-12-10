using Festispec.Message;
using Festispec.View.Pages.Report.element;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class TextVM : ReportElementVM
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

        public Dictionary<string, Object> Dictionary { get; set; }

        public TextVM()
        {
            Type = "text";
        }
        public TextVM(ReportElementVM element)
        {
            EditElement = new RelayCommand(() => Edit());
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
                ReportElementVM = this
            });
        }
    }
}
