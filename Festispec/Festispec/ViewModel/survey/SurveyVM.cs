using Festispec.Domain;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.survey.question;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;
using Festispec.Factory;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM
    {
        private ObservableCollection<CaseVM> _cases;
        private ObservableCollection<SurveyQuestionVM> _questions;
        private Survey _survey;
        private QuestionFactory _questionFactory;

        public int Id {
            get {
                return _survey.Id;
            }
            private set {
                _survey.Id = value;
            }
        }

        public string Description {
            get {
                return _survey.Description;
            }
            set {
                _survey.Description = value;
            }
        }

        public OrderVM Order { get; set; }

        public string Status {
            get {
                return _survey.Status;
            }
            set {
                _survey.Status = value;
            }
        }

        public ObservableCollection<CaseVM> Cases {
            get {
                return _cases;
            }
            set {
                _cases = value;
            }
        }
        
        public ObservableCollection<SurveyQuestionVM> Questions {
            get {
                return _questions;
            }
            set {
                _questions = value;
            }
        }

        public ObservableCollection<string> QuestionTypes { get; set; }
        public string SelectedQuestionType { get; set; }
        public ICommand AddQuestionCommand { get; set; }

        public SurveyVM(Survey survey)
        {
            _survey = survey;
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            Order = new OrderVM(survey.Order);
            Cases = new ObservableCollection<CaseVM>(survey.Cases.ToList().Select(c => new CaseVM(c)));
            Questions = new ObservableCollection<SurveyQuestionVM>(survey.Questions.ToList().Select(q => new SurveyQuestionVM(q)));
            _questionFactory = new QuestionFactory();
            GetQuestionTypes();
        }

        public SurveyVM()
        {
            _survey = new Survey();
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            Cases = new ObservableCollection<CaseVM>();
            Questions = new ObservableCollection<SurveyQuestionVM>();
            _questionFactory = new QuestionFactory();
            GetQuestionTypes();
        }

        private void GetQuestionTypes()
        {
            QuestionTypes = new ObservableCollection<string>();

            using (var context = new Entities())
            {
                var questionTypes = context.QuestionTypes.ToList();

                foreach (var questionType in questionTypes)
                {
                    QuestionTypes.Add(questionType.Type);
                }
            }
        }

        private void OpenAddQuestion()
        {
            var questionTypeWindow = _questionFactory.GetQuestionType(SelectedQuestionType);
            questionTypeWindow.ShowDialog();
        }
    }
}
