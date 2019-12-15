using Festispec.Factory;
using Festispec.Interface;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Festispec.ViewModel.report.element
{
    public class BaseElementEdit: ViewModelBase
    {
        private DataParserFactory _dataParserFactory;
        private ReportElementVM _reportElementVM;
        private ObservableCollection<IQuestion> _surveyQuestions;
        private List<IDataParser> _dataParsers;

        public ReportElementVM ReportElementVM {
            get {
                return _reportElementVM;
            }
            set {
                _reportElementVM = value;
                RaisePropertyChanged("ReportElementVM");
                if (_reportElementVM != null && _reportElementVM.Order != null)
                {
                    var questions = _reportElementVM.ReportVM.Order.Survey.Questions;
                    SurveyQuestions.Clear();
                    foreach (var item in questions)
                    {
                        SurveyQuestions.Add(item);
                        if (ReportElementVM.DataParser.Question.Id == item.Id)
                        {
                            ReportElementVM.DataParser.Question = item;
                        }
                    }
                }
            }
        }

        public List<IDataParser> DataParsers { 
            get {
                return _dataParsers;
            }
            set {
                _dataParsers = value;
                if (_reportElementVM != null && _reportElementVM.DataParser != null)
                {
               
                }
                RaisePropertyChanged("DataParsers");
            }
        }

        public ObservableCollection<IQuestion> SurveyQuestions { 
            get {
                return _surveyQuestions;
            }
            set {
                _surveyQuestions = value;
                RaisePropertyChanged("SurveyQuestions");
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
