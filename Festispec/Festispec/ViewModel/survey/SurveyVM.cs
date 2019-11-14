using Festispec.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.Interface;
using Festispec.ViewModel.survey.question.QuestionTypes;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM
    {
        private Domain.Survey _survey;

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

        public ObservableCollection<CaseVM> Cases { get; set; }
        public ObservableCollection<IQuestion> Questions { get; set; }
        public MainViewModel MainViewModel { get; set; }
        public ObservableCollection<string> QuestionTypes { get; set; }
        public string SelectedQuestionType { get; set; }
        public IQuestion SelectedQuestion { get; set; }
        public ICommand AddQuestionCommand { get; set; }
        public ICommand EditQuestionCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }

        public SurveyVM(MainViewModel mainViewModel, Domain.Survey survey)
        {
            MainViewModel = mainViewModel;
            _survey = survey;
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            EditQuestionCommand = new RelayCommand(OpenEditQuestion);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);
            Cases = new ObservableCollection<CaseVM>(survey.Cases.ToList().Select(c => new CaseVM(c)));
            Questions = new ObservableCollection<IQuestion>(survey.Questions.ToList().Select(CreateQuestionType));
            GetQuestionTypes();
            MainViewModel.PageSingleton.SetSurveyPages();
        }

        public SurveyVM(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            _survey = new Domain.Survey();
            AddQuestionCommand = new RelayCommand(OpenAddQuestion);
            EditQuestionCommand = new RelayCommand(OpenEditQuestion);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);
            Cases = new ObservableCollection<CaseVM>();
            Questions = new ObservableCollection<IQuestion>();
            GetQuestionTypes();
            MainViewModel.PageSingleton.SetSurveyPages();
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
                    return new OpenQuestionVM(this, question) { MainViewModel = MainViewModel };
                case "Gesloten vraag":
                    return new ClosedQuestionVM(this, question) { MainViewModel = MainViewModel };
                case "Schuifbalk vraag":
                    return new SliderQuestionVM(this, question) { MainViewModel = MainViewModel };
                case "Opmerking veld":
                    return new CommentFieldVM(this, question) { MainViewModel = MainViewModel };
                case "Afbeelding galerij vraag":
                    return new ImageGalleryQuestionVM(this, question) { MainViewModel = MainViewModel };
                case "Teken vraag":
                    return new DrawQuestionVM(this, question) { MainViewModel = MainViewModel };
                case "Meerkeuze vraag":
                    return new MultipleChoiceQuestionVM(this, question) { MainViewModel = MainViewModel };
                default:
                    return new OpenQuestionVM(this, question) { MainViewModel = MainViewModel };
            }
        }

        private void OpenAddQuestion()
        {
            var questionTypePage = MainViewModel.PageSingleton.GetPage("Add " + SelectedQuestionType);
            var questionVm = CreateQuestionType(new Question() { Type = SelectedQuestionType, Order = Questions.Count + 1, SurveyId = _survey.Id, Variables = "test" });
            questionVm.MainViewModel = MainViewModel;
            questionTypePage.DataContext = questionVm;
            MainViewModel.Page = questionTypePage;
        }

        private void OpenEditQuestion()
        {
            var questionTypePage = MainViewModel.PageSingleton.GetPage("Edit " + SelectedQuestion.ToModel().Type);
            questionTypePage.DataContext = SelectedQuestion;
            MainViewModel.Page = questionTypePage;
        }

        private void DeleteQuestion()
        {
            using (var context = new Entities())
            {
                context.Questions.Attach(SelectedQuestion.ToModel());
                context.Questions.Remove(SelectedQuestion.ToModel());
                context.SaveChanges();
                Questions.Remove(SelectedQuestion);
            }
        }
    }
}
