using Festispec.Domain;
using Festispec.Interface;
using GalaSoft.MvvmLight;
using System;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class ReportElementVM: ViewModelBase
    {
        private ReportElement _reportElement;

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

        public IDataParser DataParser { get; set; }

        public string JsonData {
            get {
                return _reportElement.Data;
            }
            set {
                _reportElement.Data = value;
            }
        }

        public virtual Object Data { get; set; }
        public ICommand EditElementCommand { get; set; }

        public ReportElementVM(ReportElement element)
        {
            _reportElement = element;
        }

        public ReportElementVM()
        {
            _reportElement = new ReportElement();
        }

        protected void GoToEdit()
        {

        }
    }
}
