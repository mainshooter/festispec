﻿using Festispec.Domain;
using GalaSoft.MvvmLight;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.rapport.element
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

        public virtual Object Data { get; set; }

        public ReportElementVM(ReportElement element)
        {
            _reportElement = element;
        }

        public ReportElementVM()
        {
            _reportElement = new ReportElement();
        }
    }
}
