using Festispec.Factory;
using Festispec.Interface;
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

        private ToastVM _toastVM;

        private string _selectedElementType;
        private IQuestion _selectedQuestion;
        private IDataParser _selectedDataParser;


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

        public ReportVM Report { get; set; }
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
            _reportElementFactory = new ReportElementFactory();
            _toastVM = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                Report = message.SelectedReport;
                _orderVM = Report.Order;
                _survey = _orderVM.Survey;
                PosibleSurveyQuestions.Clear();
                SurveyQuestions.Clear();
                foreach (var item in _survey.Questions)
                {
                    SurveyQuestions.Add(item);
                    PosibleSurveyQuestions.Add(item);
                }
                ReportElement = new ReportElementVM();
            });


            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            SurveyQuestions = new List<IQuestion>();
            DataParsers = _dataParserFactory.DataParsers;
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport);

            PosibleSurveyQuestions = new ObservableCollection<IQuestion>();
            PosibleDataParsers = new ObservableCollection<IDataParser>(DataParsers);
            PosibleElementTypes = new ObservableCollection<string>(ElementTypes);
        }

        private void AddElementToReport()
        {
            if (CheckIfElementCanBeAdded())
            {
                ReportElement.Title = "Leuke titel";
                ReportElement.Content = "Test description";
                ReportElement.Type = SelectedElementType;
                IDataParser dataVM = _dataParserFactory.GetDataParser(SelectedDataParser.ParserType);
                dataVM.Question = SelectedQuestion;
                ReportElement.Data = dataVM.ParseData();
                var userControl = _reportElementFactory.CreateElement(ReportElement);
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
            if (!_selectedDataParser.SupportedVisuals.Contains(SelectedElementType))
            {
                return false;
            }
            return true;
        }
    }
}
