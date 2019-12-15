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
    public class BaseElementAdd: ViewModelBase
    {
        private DataParserFactory _dataParserFactory;

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
    }
}
