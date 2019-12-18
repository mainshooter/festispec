using Festispec.Domain;
using Festispec.Factory;
using Festispec.Interface;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Edit
{
    public class BaseElementEdit: ViewModelBase
    {
        private DataParserFactory _dataParserFactory;
        private ReportElementVM _reportElementVM;
        private ObservableCollection<IQuestion> _surveyQuestions;
        private List<IDataParser> _dataParsers;
        private int _selectedSurveyQuestionIndex;
        private int _selectedDataParserIndex;

        public ICommand SaveElementCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public ReportElementVM ReportElementVM 
        {
            get 
            {
                return _reportElementVM;
            }
            set 
            {
                _reportElementVM = value;
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
                        }
                        index++;
                    }

                    if (_reportElementVM != null && _reportElementVM.DataParser != null)
                    {
                        int secondIndex = 0;
                        foreach (var item in DataParsers)
                        {
                            if (item.Type == _reportElementVM.DataParser.Type)
                            {
                                SelectedDataParserIndex = secondIndex;
                                break;
                            }
                            secondIndex++;
                        }
                    }
                }
                RaisePropertyChanged("ReportElementVM");
            }
        }

        public int SelectedDataParserIndex 
        {
            get 
            {
                return _selectedDataParserIndex;
            }
            set 
            {
                _selectedDataParserIndex = value;
                
                ReportElementVM.DataParser = DataParsers[_selectedDataParserIndex];
                RaisePropertyChanged("SelectedDataParserIndex");
            }
        }

        public int SelectedSurveyQuestionIndex 
        {
            get 
            {
                return _selectedSurveyQuestionIndex;
            }
            set 
            {
                _selectedSurveyQuestionIndex = value;
                ReportElementVM.SelectedSurveyQuestion = SurveyQuestions[_selectedSurveyQuestionIndex];
                RaisePropertyChanged("SelectedSurveyQuestionIndex");
            }
        }

        public List<IDataParser> DataParsers 
        { 
            get 
            {
                return _dataParsers;
            }
            set 
            {
                _dataParsers = value;
                RaisePropertyChanged("DataParsers");
            }
        }

        public ObservableCollection<IQuestion> SurveyQuestions 
        { 
            get 
            {
                return _surveyQuestions;
            }
            set 
            {
                _surveyQuestions = value;
                RaisePropertyChanged("SurveyQuestions");
            }
        }

        public BaseElementEdit()
        {
            SaveElementCommand = new RelayCommand(EditElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseEditElement);
            _dataParserFactory = new DataParserFactory();
            SurveyQuestions = new ObservableCollection<IQuestion>();
            DataParsers = new List<IDataParser>();
            DataParsers = _dataParserFactory.DataParsers;
        }

        public void EditElement()
        {
            ReportElementVM.DataParser.Question = ReportElementVM.SelectedSurveyQuestion;
            ToastVM toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            if (CanUseOptions())
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Attach(this.ReportElementVM.ToModel());
                    context.Entry(ReportElementVM.ToModel()).State = EntityState.Modified;
                    context.SaveChanges();
                }
                toast.ShowInformation("Rapportelement bijgewerkt.");
                CloseEditElement();
            }
            else
            {
                toast.ShowError("Deze query kan niet met dit element en/of vraag word niet ondersteunt door deze query");
            }
        }

        public void CloseEditElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }

        private bool CanUseOptions()
        {
            if (ReportElementVM.DataParser != null && ReportElementVM.DataParser.QuestionTypeIsSupported)
            {
                var dataParser = ReportElementVM.DataParser;
                bool containsSupportedType = dataParser.SupportedVisuals.Contains(ReportElementVM.Type);
                if (containsSupportedType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
