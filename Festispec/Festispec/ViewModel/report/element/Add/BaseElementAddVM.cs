using Festispec.Domain;
using Festispec.Factory;
using Festispec.Interface;
using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Add
{
    public class BaseElementAdd: ViewModelBase
    {
        private DataParserFactory _dataParserFactory;
        private ReportElementVM _reportElementVM;
        private List<IDataParser> _dataParsers;
        private ObservableCollection<IQuestion> _surveyQuestions;
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
                RaisePropertyChanged("ReportElementVM");
            }
        }

        public List<IDataParser> DataParsers 
        { 
            get 
            {
                return _dataParsers;
            }
            private set 
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

        public BaseElementAdd()
        {
            SaveElementCommand = new RelayCommand(SaveElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseSaveElement);
            _dataParserFactory = new DataParserFactory();
            SurveyQuestions = new ObservableCollection<IQuestion>();
            DataParsers = new List<IDataParser>();
            DataParsers = _dataParserFactory.DataParsers;
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => 
            {
                var questions = message.NextReportVM.Order.Survey.Questions;
                SurveyQuestions.Clear();
                foreach (var item in questions)
                {
                    SurveyQuestions.Add(item);
                }
                ReportElementVM.SelectedSurveyQuestion = SurveyQuestions.First();
            });
        }
        public void SaveElement()
        {
            ToastVM toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            if (ReportElementVM.Type != ReportElementType.Image && ReportElementVM.Type != ReportElementType.Text)
            {
                ReportElementVM.DataParser.Question = ReportElementVM.SelectedSurveyQuestion;
            }
            
            if (CanUseOptions())
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Add(ReportElementVM.ToModel());
                    context.SaveChanges();
                }
                toast.ShowInformation("Rapportelement is toegevoegd.");
                CloseSaveElement();
            }
            else
            {
                toast.ShowError("Deze query kan niet met dit element en/of vraag word niet ondersteunt door deze query");
            }
        }

        public void CloseSaveElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }

        public bool CanUseOptions()
        {
            if (ReportElementVM.Type == ReportElementType.Image || ReportElementVM.Type == ReportElementType.Text)
            {
                return true;
            }
            if (ReportElementVM.DataParser != null && ReportElementVM.DataParser.Question != null && ReportElementVM.DataParser.QuestionTypeIsSupported)
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
