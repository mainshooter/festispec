﻿using Festispec.Domain;
using Festispec.Factory;
using Festispec.Interface;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class ReportElementVM : ViewModelBase
    {
        private ReportElement _reportElement;
        private IDataParser _dataParser;
        private List<List<string>> _data;

        public ReportVM ReportVM { get; set; }

        public ICommand EditElement { get; set; }

        public ICommand DeleteElement { get; set; }

        public ICommand ElementUpCommand { get; set; }

        public ICommand ElementDownCommand { get; set; }

        public int Id
        {
            get
            {
                return _reportElement.Id;
            }
            set
            {
                _reportElement.Id = value;
            }
        }

        public int ReportId
        {
            get
            {
                return _reportElement.ReportId;
            }
            set
            {
                _reportElement.ReportId = value;
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
                RaisePropertyChanged("Content");
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
                RaisePropertyChanged("Order");
            }
        }

        public string X_as
        {
            get
            {
                return _reportElement.X_as;
            }
            set
            {
                _reportElement.X_as = value;
            }
        }

        public string Y_as
        {
            get
            {
                return _reportElement.Y_as;
            }
            set
            {
                _reportElement.Y_as = value;
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

        public virtual IDataParser DataParser { 
            get {
                return _dataParser;
            }
            set {
                _dataParser = value;
                if (_dataParser != null)
                {
                    _data = _dataParser.ParseData();
                    _reportElement.Data = DataParser.ToJson();
                    RaisePropertyChanged("DataParser");
                }
                else
                {
                    _data = new List<List<string>>();
                    _reportElement.Data = null;
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

        public ReportElementVM(ReportElement element, ReportVM report)
        {
            _reportElement = element;
            ReportVM = report;
            DataParser = DataParserFactory.GetDataParserByJson(element.Data);
            DeleteElement = new RelayCommand(() => Delete());
            ElementUpCommand = new RelayCommand(() => ReportVM.MoveElement(this, -1));
            ElementDownCommand = new RelayCommand(() => ReportVM.MoveElement(this, 1));
        }

        public ReportElementVM()
        {
            DeleteElement = new RelayCommand(() => Delete());
            ElementUpCommand = new RelayCommand(() => ReportVM.MoveElement(this, -1));
            ElementDownCommand = new RelayCommand(() => ReportVM.MoveElement(this, 1));
            _reportElement = new ReportElement();
        }
        public void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze element wilt verwijderen?", "Element Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Remove(context.ReportElements.Where(reportElement => reportElement.Id == _reportElement.Id).First());
                    context.SaveChanges();
                }
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Rapportelement verwijderd.");
            }
            ReportVM.RefreshElements();
        }

        public ReportElement ToModel()
        {
            return _reportElement;
        }
    }
}
