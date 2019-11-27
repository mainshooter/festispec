using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        
        private object _data;
        private Boolean _readOnly;

        public Boolean ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                RaisePropertyChanged("ReadOnly");
            }
        }

        public ReportElementVM ReportElementVM { get; set; }
        public string Text { get; set; }

        public Dictionary<string, Object> Dictionary { get; set; }

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

        public TextVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            //Data = element.Data;
            Report = report;
            ReportElementVM = element;
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;

            Order = element.Order;
        }
        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTextPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = Report,
                ReportElement = ReportElementVM
            });
        }
    
        private void ApplyChanges()
        {
            Text = (string)Dictionary["text"];
        }
    }
}
