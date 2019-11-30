using Festispec.Factory;
using Festispec.Interface;
﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.report.data;
using Festispec.ViewModel.report.element;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddElementVM : ViewModelBase
    {
        private ReportElementFactory _reportElementFactory;

        private DataParserFactory _dataParserFactory;
        private DataVM _dataVM;
        private OrderVM _orderVM;
        private SurveyVM _survey;
        private ReportElementVM _reportElementVM;

        private string _axes;
        private int _selectedElementIndex;


        private ToastVM _toastVM;


        private string _selectedElementType;
        private IQuestion _selectedQuestion;
        private IDataParser _selectedDataParser;


        public string Axes
        {
            get
            {
                return _axes;
            }
            set
            {
                _axes = value;
                RaisePropertyChanged("Axes");
            }
        }

        public ReportVM Report { get; set; }

        public int SelectedElementIndex
        {
            get
            {
                return _selectedElementIndex;
            }
            set
            {
                _selectedElementIndex = value;
                RaisePropertyChanged("SelectedElementIndex");
                ChangeInput();
            }
        }

        public string SelectedElementType {
            get {
                return _selectedElementType;
            }
            set {
                _selectedElementType = value;
            }
        }
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
        public ICommand GoBackCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public IDataParser SelectedDataParser {
            get {
                return _selectedDataParser;
            }
            set {
                _selectedDataParser = value;
            }
        }
        public List<IDataParser> DataParsers { get; private set; }
        public ObservableCollection<IDataParser> PosibleDataParsers { get; set; }


        public List<IQuestion> SurveyQuestions { get; set; }
        public IQuestion SelectedQuestion {
            get {
                return _selectedQuestion;
            }
            set {
                _selectedQuestion = value;
            }
        }
        public ObservableCollection<IQuestion> PosibleSurveyQuestions { get; set; }
        public ObservableCollection<string> PosibleElementTypes { get; set; }

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
            _dataParserFactory = new DataParserFactory();
            Axes = "Hidden";
            _reportElementFactory = new ReportElementFactory();
            _toastVM = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();


            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            SurveyQuestions = new List<IQuestion>();
            DataParsers = _dataParserFactory.DataParsers;
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport);

            PosibleSurveyQuestions = new ObservableCollection<IQuestion>();
            PosibleDataParsers = new ObservableCollection<IDataParser>(DataParsers);
            PosibleElementTypes = new ObservableCollection<string>(ElementTypes);

            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                Report = message.NextReportVM;
                ReportElement = new ReportElementVM();
                ReportElement.ReportId = Report.Id;
                ReportElement.Order = Report.ReportElements.Count + 1;
                _orderVM = Report.Order;
                _survey = _orderVM.Survey;
                PosibleSurveyQuestions.Clear();
                SurveyQuestions.Clear();
                foreach (var item in _survey.Questions)
                {
                    SurveyQuestions.Add(item);
                    PosibleSurveyQuestions.Add(item);
                }
            });
        }
        public void ChangeInput()
        {
            switch (ElementTypes[SelectedElementIndex])
            {
                case "image":
                    var fd = new OpenFileDialog { Filter = "All Image Files | *.*", Multiselect = false };
                    if (fd.ShowDialog() != true) return;
                    using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        var test = new byte[fs.Length];
                        fs.Read(test, 0, Convert.ToInt32(fs.Length));
                    }
                    Axes = "Hidden";
                    break;
                case "barchart":
                    Axes = "Visable";
                    break;
                case "linechart":
                    Axes = "Visable";
                    break;
                default:
                    Axes = "Hidden";
                    break;
            }
        }
        private void AddElementToReport()
        {
            if (CheckIfElementCanBeAdded())
            {
                IDataParser dataVM = _dataParserFactory.GetDataParser(SelectedDataParser.Type);
                dataVM.Question = SelectedQuestion;
                ReportElement.DataParser = dataVM;
                using (var context = new Entities())
                {
                    _reportElementFactory.CreateElement(ReportElement, Report);
                    context.ReportElements.Add(ReportElement.ToModel());
                    context.SaveChanges();
                }
                var userControl = _reportElementFactory.CreateElement(ReportElement, Report);
                Report.ReportElementUserControlls.Add(userControl);
                GoBackToReport();
            }
            else
            {
                _toastVM.ShowError("Deze combinatie is niet mogelijk");
            }
        }

        private void GoBackToReport()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        private bool CheckIfElementCanBeAdded()
        {
            if (!_selectedDataParser.SupportedQuestions.Contains(_selectedQuestion.QuestionType))
            {
                return false;
            }
            if (!_selectedDataParser.SupportedVisuals.Contains(ReportElement.Type))
            {
                return false;
            }
            return true;
        }
    }
}
