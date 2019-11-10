using Festispec.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.Factory;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.survey.question.questionTypes;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM
    {
        private ObservableCollection<CaseVM> _cases;
        private ObservableCollection<IQuestion> _questions;
        private Survey _survey;
        private EventVM _event;
        private QuestionFactory _questionFactory;

        public int Id {
            get => _survey.Id;
            private set => _survey.Id = value;
        }

        public string Description {
            get => _survey.Description;
            set => _survey.Description = value;
        }

        public string Status {
            get => _survey.Status;
            set => _survey.Status = value;
        }

        public ObservableCollection<CaseVM> Cases {
            get => _cases;
            set => _cases = value;
        }
        
        public ObservableCollection<IQuestion> Questions {
            get => _questions;
            set => _questions = value;
        }

        public ObservableCollection<string> QuestionTypes { get; set; }
        public string EventName => _event.Name;
        public string SelectedQuestionType { get; set; }
        public IQuestion SelectedQuestion { get; set; }
        public ICommand AddQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }

        public SurveyVM(EventVM selectedEvent, Survey survey)
        {
            _survey = survey;
            _event = selectedEvent;
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            EditQuestionCommand = new RelayCommand(OpenEditCommand);
            _questionFactory = new QuestionFactory();
            Cases = new ObservableCollection<CaseVM>(survey.Cases.ToList().Select(c => new CaseVM(c)));
            Questions = new ObservableCollection<IQuestion>(survey.Questions.ToList().Select(q => _questionFactory.CreateQuestionType(q)));
            GetQuestionTypes();
        }

        public SurveyVM(EventVM selectedEvent)
        {
            _survey = new Survey();
            _event = selectedEvent;
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            Cases = new ObservableCollection<CaseVM>();
            Questions = new ObservableCollection<IQuestion>();
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

                SelectedQuestionType = QuestionTypes[0];
            }
        }

        private void OpenAddQuestion()
        {
            var questionTypeWindow = _questionFactory.GetQuestionType("Add " + SelectedQuestionType);
            questionTypeWindow.ShowDialog();
        }

        private void OpenEditCommand()
        {
            var questionTypeWindow = _questionFactory.GetQuestionType("Edit " + SelectedQuestion.QuestionType);
            questionTypeWindow.DataContext = SelectedQuestion;
            questionTypeWindow.ShowDialog();
        }
    }
}
