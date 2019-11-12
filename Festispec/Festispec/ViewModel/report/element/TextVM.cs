using Festispec.ViewModel.rapport.element;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        private object _data;
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

        public ICommand ChangeToReadOnly { get; set; }
        public ICommand ChangeToInput { get; set; }

        public override Object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
        public TextVM(ReportElementVM element)
        {
            Title = element.Title;
            Text = element.Content;
            Data = element.Data;
            ReadOnly = true;
            ChangeToReadOnly = new RelayCommand(()=> ReadOnly = true);
            ChangeToInput = new RelayCommand(() => ReadOnly = false);
        }
    }
}
