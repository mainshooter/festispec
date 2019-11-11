using Festispec.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.ViewModel.survey.question.questionTypes;
using Festispec.ViewModel.survey.question.QuestionTypes;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM
    {
        private ObservableCollection<CaseVM> _cases;
        private ObservableCollection<IQuestion> _questions;
        private Survey _survey;
        private EventVM _event;

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

        public MainViewModel MainViewModel { get; set; }
        public ObservableCollection<string> QuestionTypes { get; set; }
        public string EventName => _event.Name;
        public string SelectedQuestionType { get; set; }
        public IQuestion SelectedQuestion { get; set; }
        public ICommand AddQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }

        public SurveyVM(MainViewModel mainViewModel, EventVM selectedEvent, Survey survey)
        {
            MainViewModel = mainViewModel;
            _survey = survey;
            _event = selectedEvent;
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            EditQuestionCommand = new RelayCommand(OpenEditCommand);
            Cases = new ObservableCollection<CaseVM>(survey.Cases.ToList().Select(c => new CaseVM(c)));
            Questions = new ObservableCollection<IQuestion>(survey.Questions.ToList().Select(CreateQuestionType));
            GetQuestionTypes();
        }

        public SurveyVM(EventVM selectedEvent)
        {
            _survey = new Survey();
            _event = selectedEvent;
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            Cases = new ObservableCollection<CaseVM>();
            Questions = new ObservableCollection<IQuestion>();
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

        private IQuestion CreateQuestionType(Question question)
        {
            switch (question.Type)
            {
                case "Open vraag":
                    var openQuestionVm = new OpenQuestionVM(question) {MainViewModel = MainViewModel};
                    return openQuestionVm;
                case "Gesloten vraag":
                    return new ClosedQuestionVM(question) { MainViewModel = MainViewModel };
                default:
                    return new OpenQuestionVM(question) { MainViewModel = MainViewModel };
            }
        }

        private void OpenAddQuestion()
        {
            var questionTypePage = MainViewModel.PageSingleton.GetPage("Add " + SelectedQuestionType);
            MainViewModel.Page = questionTypePage;
        }

        private void OpenEditCommand()
        {
            var questionTypePage = MainViewModel.PageSingleton.GetPage("Edit " + SelectedQuestion.QuestionType);
            questionTypePage.DataContext = SelectedQuestion;
            MainViewModel.Page = questionTypePage;
        }
    }
}
