using Festispec.Factory;
using Festispec.Interface;
using Festispec.Message;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element
{
    public class BaseElementEdit: ViewModelBase
    {
        private IQuestion _selectedQuestion;
        private IDataParser _selectedDataParser;
        private DataParserFactory _dataParserFactory;
        private ReportElementVM _reportElementVM;

        public ReportElementVM ReportElementVM {
            get {
                return _reportElementVM;
            }
            set {
                _reportElementVM = value;
                if (_reportElementVM != null)
                {
                    var questions = _reportElementVM.ReportVM.Order.Survey.Questions;
                    SurveyQuestions.Clear();
                    foreach (var item in questions)
                    {
                        SurveyQuestions.Add(item);
                    }
                }
                RaisePropertyChanged("ReportElementVM");
            }
        }

        public List<IDataParser> DataParsers { get; private set; }

        public ObservableCollection<IQuestion> SurveyQuestions { get; set; }
        public IQuestion SelectedQuestion {
            get {
                return _selectedQuestion;
            }
            set {
                _selectedQuestion = value;
            }
        }
        public IDataParser SelectedDataParser {
            get {
                return _selectedDataParser;
            }
            set {
                _selectedDataParser = value;
            }
        }

        public BaseElementEdit()
        {
            _dataParserFactory = new DataParserFactory();
            SurveyQuestions = new ObservableCollection<IQuestion>();
            DataParsers = new List<IDataParser>();
            DataParsers = _dataParserFactory.DataParsers;
        }
    }
}
