﻿using Festispec.Factory;
using Festispec.Message;
using Festispec.Repository;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.report.data;
using Festispec.ViewModel.report.element;
using Festispec.ViewModel.survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddElementVM : ViewModelBase
    {
        private ReportElementFactory _reportElementFactory;
        private DataTypesRepository _dataTypesRepository;
        private DataVM _dataVM;
        private OrderVM _orderVM;
        private SurveyVM _survey;
        private ReportElementVM _reportElementVM;

        public List<string> ElementTypes { get; set; }

        public ReportElementVM ReportElement {
            get {
                return _reportElementVM;
            }
            set {
                _reportElementVM = value;
                RaisePropertyChanged("ReportElement");
            }
        }

        public ReportVM Report { get; set; }
        public ICommand GoBackCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public List<string> QueryTypes { get; set; }

        public List<string> SurveyQuestions { get; set; }

        public DataVM DataVM { 
            get {
                return _dataVM;
            }
            set {
                _dataVM = value;
            }
        }

        public AddElementVM()
        {
            ReportElement = new ReportElementVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                Report = message.SelectedReport;
                _orderVM = Report.Order;
                _survey = _orderVM.Survey;
                foreach (var item in _survey.Questions)
                {
                    SurveyQuestions.Add(item.Question);
                }
                ReportElement = new ReportElementVM();
            });
            _dataTypesRepository = CommonServiceLocator.ServiceLocator.Current.GetInstance<DataTypesRepository>();
            _reportElementFactory = new ReportElementFactory();
            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            SurveyQuestions = new List<string>();
            QueryTypes = _dataTypesRepository.DataTypes;
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport);
        }

        private void AddElementToReport()
        {
            ReportElement.Title = "Leuke titel";
            ReportElement.Content = "Test description";
            
            if (ReportElement.Equals("table"))
            {
                ReportElement.Data = new Dictionary<string, List<string>>()
                {
                    ["id"] = new List<string>() { "1", "2" }
                };
            }
            else if (ReportElement.Type.Equals("linechart"))
            {
                ReportElement.Data = new Dictionary<string, Object>()
                {
                    ["xaxisName"] = "Test xas",
                    ["yaxisName"] = "Test yas",
                    ["seriescollection"] = new SeriesCollection
                    {
                        new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
                    }
                };
            }
            else if (ReportElement.Type.Equals("piechart"))
            {
                ReportElement.Data = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Bier",
                        Values = new ChartValues<double> { 20 },
                        DataLabels = true,
                    },
                    new PieSeries
                    {
                        Title = "Frisdrank",
                        Values = new ChartValues<double> { 12 },
                        DataLabels = true,
                    },
                    new PieSeries
                    {
                        Title = "Cocktail",
                        Values = new ChartValues<double> { 8 },
                        DataLabels = true,
                    },
                    new PieSeries
                    {
                        Title = "Wijn",
                        Values = new ChartValues<double> { 2 },
                        DataLabels = true,
                    }
                };
            }
            else if (ReportElement.Type.Equals("barchart"))
            {
                ReportElement.Data = new Dictionary<string, Object>()
                {
                    ["xaxisName"] = "Place",
                    ["yaxisName"] = "Amount",
                    ["labels"] = new List<string> { "test1", "test2", "test3", "test4", "test5" },
                    ["seriescollection"] = new SeriesCollection
                    {
                        new ColumnSeries {Title = "testdata" , Values = new ChartValues<int>{10,20,30,40,50} },

                        new ColumnSeries {Title = "testdata2" , Values = new ChartValues<int>{15,25,35,45,55} }
                    }
                };
            }        
            else if (ReportElement.Type.Equals("text"))
            {
                ReportElement.Data = new Dictionary<string, Object>()
                {
                    ["text"] = "test text smiley"
                };

            }
            else if (ReportElement.Type.Equals("image"))
            {
                ReportElement.Data = new Dictionary<string, Object>()
                {
                    ["image"] = new byte[0]
                };
            }
            var userControl = _reportElementFactory.CreateElement(ReportElement);
            Report.ReportElementUserControlls.Add(userControl);
            GoBackToReport();
        }

        private void GoBackToReport()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }
    }
}
