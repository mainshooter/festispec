using Festispec.Domain;
using Festispec.Interface;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class ReportElementVM: ViewModelBase
    {
        private ReportElement _reportElement;
        private IDataParser _dataParser;
        private List<List<string>> _data;

        public int Id {
            get {
                return _reportElement.Id;
            }
            private set {
                _reportElement.Id = value;
            }
        }

        public string Type {
            get {
                return _reportElement.ElementType;
            }
            set {
                _reportElement.ElementType = value;
            }
        }

        public string Title {
            get {
                return _reportElement.Title;
            }
            set {
                _reportElement.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Content {
            get {
                return _reportElement.Content;
            }
            set {
                _reportElement.Content = value;
            }
        }

        public int Order {
            get {
                return _reportElement.Order;
            }
            set {
                _reportElement.Order = value;
            }
        }

        public List<List<string>> Data {
            get {
                return _data;
            }
            set {
                _data = value;
                RaisePropertyChanged("Data");
            }
        }

        public IDataParser DataParser { 
            get {
                return _dataParser;
            }
            set {
                _dataParser = value;
                if (_dataParser != null)
                {
                    _data = _dataParser.ParseData();
                    ApplyChanges();
                    RaisePropertyChanged("DataParser");
                }
                else
                {
                    _data = new List<List<string>>();
                }
            }
        }

        public string JsonData {
            get {
                return _reportElement.Data;
            }
            set {
                _reportElement.Data = value;
            }
        }

        public ICommand EditElementCommand { get; set; }

        public ReportElementVM(ReportElement element)
        {
            _reportElement = element;
        }

        public ReportElementVM()
        {
            _reportElement = new ReportElement();
        }

        protected virtual void ApplyChanges()
        {

        }

        protected void GoToEdit()
        {

        }
    }
}
