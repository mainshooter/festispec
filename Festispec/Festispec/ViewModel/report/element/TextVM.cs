using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        private object _data;
        private Boolean _readOnly;
        private int _elementId;

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

        public TextVM(ReportElementVM element)
        {
            //Data = element.Data;
            _elementId = element.Id;
            Title = element.Title;
            Content = element.Content;
            ReadOnly = true;
        }
        private void ApplyChanges()
        {
            Text = (string)Dictionary["text"];
        }
    }
}
