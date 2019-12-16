using Festispec.Factory;
using Festispec.Interface;
using Festispec.Message;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Festispec.ViewModel.report.element
{
    public class BaseElementAdd: ViewModelBase
    {
        private DataParserFactory _dataParserFactory;
        private ReportElementVM _reportElementVM;

        public ReportElementVM ReportElementVM 
        {
            get {
                return _reportElementVM;
            }
            set {
                _reportElementVM = value;
            }
        }

        public List<IDataParser> DataParsers { get; private set; }

        public ObservableCollection<IQuestion> SurveyQuestions { get; set; }

        public BaseElementAdd()
        {
            _dataParserFactory = new DataParserFactory();
            SurveyQuestions = new ObservableCollection<IQuestion>();
            DataParsers = new List<IDataParser>();
            DataParsers = _dataParserFactory.DataParsers;
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                var report = message.NextReportVM;
                var questions = message.NextReportVM.Order.Survey.Questions;
                SurveyQuestions.Clear();
                foreach (var item in questions)
                {
                    SurveyQuestions.Add(item);
                }

            });
        }
        public void SaveElement()
        {
            ToastVM toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            ReportElementVM.DataParser.Question = ReportElementVM.SelectedSurveyQuestion;
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
