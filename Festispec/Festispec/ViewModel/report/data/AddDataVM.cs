using Festispec.Message;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.report;
using Festispec.ViewModel.report.element;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.View.Usercontrols.Report.Data
{
    public class AddDataVM: ViewModelBase
    {
        private ObservableCollection<SurveyQuestionVM> _questions;
        public ReportElementVM ReportElement { get; set; }
        public ReportVM ReportVM { get; set; }

        public OrderVM OrderVM { get; set; }

        public SurveyVM Survey { get; set; }

        public ObservableCollection<SurveyQuestionVM> Questions {
            get {
                return _questions;
            }
            set {
                _questions = value;
                RaisePropertyChanged();
            }
        }

        [PreferredConstructor]
        public AddDataVM()
        {
            MessengerInstance.Register<ChangeSelectedOrderMessage>(this, message => {
                OrderVM = message.SelectedOrder;
                Survey = OrderVM.Survey;
                Questions = Survey.Questions;
            });
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportVM = message.SelectedReport;
            });

            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => {
                ReportElement = message.ReportElementVM;
            });
        }
    }
}
