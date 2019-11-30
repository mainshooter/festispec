using Festispec.Message;
using Festispec.View.Pages.Report.element;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        private Boolean _readOnly;

        public Boolean ReadOnly {
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

        public ICommand ChangeToReadOnly { get; set; }

        public ICommand ChangeToInput { get; set; }
        public TextVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            Data = element.Data;
            ReportVM = report;
            ReportElementVM = element;
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            ReadOnly = true;
            ChangeToReadOnly = new RelayCommand(()=> ReadOnly = true);
            ChangeToInput = new RelayCommand(() => ReadOnly = false);
            Order = element.Order;
            ReportId = element.ReportId;
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTextPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = ReportVM,
                ReportElement = ReportElementVM
            });
        }

        public void ApplyChanges()
        {
            Text = (string)Dictionary["text"];
        }
    }
}
