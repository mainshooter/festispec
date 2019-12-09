using Festispec.Domain;
using GalaSoft.MvvmLight;
using System;
using System.Windows;

namespace Festispec.ViewModel.report.element
{
    public class ReportElementVM : ViewModelBase
    {
        private ReportElement _reportElement;
        private Visibility _visibilityButtons;

        public int Id
        {
            get
            {
                return _reportElement.Id;
            }
            private set
            {
                _reportElement.Id = value;
            }
        }

        public string Type
        {
            get
            {
                return _reportElement.ElementType;
            }
            set
            {
                _reportElement.ElementType = value;
            }
        }

        public string Title
        {
            get
            {
                return _reportElement.Title;
            }
            set
            {
                _reportElement.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Content
        {
            get
            {
                return _reportElement.Content;
            }
            set
            {
                _reportElement.Content = value;
            }
        }

        public int Order
        {
            get
            {
                return _reportElement.Order;
            }
            set
            {
                _reportElement.Order = value;
            }
        }

        public virtual Object Data { get; set; }

        public ReportElementVM(ReportElement element)
        {
            _reportElement = element;
        }

        public ReportElementVM()
        {
            _reportElement = new ReportElement();
        }

        public Visibility VisibilityButtons
        {
            get
            {
                return _visibilityButtons;
            }
            set
            {
                _visibilityButtons = value;
                RaisePropertyChanged();
            }
        }


    }
}
