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
                if (_reportElementVM != null && _reportElementVM.ReportVM != null)
                {
                    var questions = _reportElementVM.ReportVM.Order.Survey.Questions;
                    SurveyQuestions.Clear();
                    int index = 0;
                    foreach (var item in questions)
                    {
                        SurveyQuestions.Add(item);
                        if (_reportElementVM.DataParser != null && _reportElementVM.DataParser.Question != null && item.Id == _reportElementVM.DataParser.Question.Id)
                        {
                            SelectedSurveyQuestionIndex = index;
                            break;
                        }
                    }

                    if (_reportElementVM != null && _reportElementVM.DataParser != null)
                    {
                        index = 0;
                        foreach (var item in DataParsers)
                        {
                            if (item.Type == _reportElementVM.DataParser.Type)
                            {
                                SelectedDataParserIndex = index;
                                break;
                            }
                            index++;
                        }
                    }
                }
            }
        }

        public int SelectedDataParserIndex {
            get;set;
        }

        public int SelectedSurveyQuestionIndex {
            get;set;
        }

        public List<IDataParser> DataParsers { 
            get {
                return _dataParsers;
            }
            set {
                _dataParsers = value;

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
