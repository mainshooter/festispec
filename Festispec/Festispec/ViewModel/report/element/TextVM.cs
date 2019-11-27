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

        public string Text { get; set; }

        public Dictionary<string, Object> Dictionary { get; set; }

        public ICommand ChangeToReadOnly { get; set; }

        public ICommand ChangeToInput { get; set; }

        public TextVM(ReportElementVM element)
        {
            Data = element.Data;
            Title = element.Title;
            Content = element.Content;
            ReadOnly = true;
            ChangeToReadOnly = new RelayCommand(()=> ReadOnly = true);
            ChangeToInput = new RelayCommand(() => ReadOnly = false);
            EditElementCommand = new RelayCommand(GoToEdit);
        }

        private void ApplyChanges()
        {
            Text = (string)Dictionary["text"];
        }
    }
}
