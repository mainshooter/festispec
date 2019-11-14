using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Survey.Question;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class MultipleChoiceQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _description;
        private string _optionName;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public ICommand AddOptionCommand { get; set; }
        public ICommand DeleteOptionCommand { get; set; }

        public string OptionName
        {
            get => _optionName;
            set
            {
                _optionName = value;
                RaisePropertyChanged();
            }
        }

        public string SelectedOptionName { get; set; }
        public ObservableCollection<string> Options { get; set; }

        public MultipleChoiceQuestionVM(SurveyVM surveyVm, MainViewModel mainViewModel, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            MainViewModel = mainViewModel;
            Options = new ObservableCollection<string>();

            if (_surveyQuestion.Question1 != null)
            {
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);

                foreach (var choice in QuestionDetails.Choices.Cols)
                {
                    Options.Add(choice);
                }
            }
            else
            {
                QuestionDetails = new QuestionDetails();
            }

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
            AddOptionCommand = new RelayCommand(AddOption);
            DeleteOptionCommand = new RelayCommand(DeleteOption);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
            _description = QuestionDetails.Description;
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                QuestionDetails.Choices.Cols.Clear();

                foreach (var option in Options)
                {
                    QuestionDetails.Choices.Cols.Add(option);
                }

                _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    _description = QuestionDetails.Description;
                    context.Questions.Add(_surveyQuestion);
                    _surveyVm.Questions.Add(this);
                    context.SaveChanges();
                }
                else
                {
                    context.Questions.Attach(_surveyQuestion);
                    context.Entry(_surveyQuestion).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }

            MainViewModel.Page.NavigationService?.GoBack();
        }

        public void GoBack()
        {
            QuestionDetails.Question = _question;
            QuestionDetails.Description = _description;
            Options.Clear();

            foreach (var options in QuestionDetails.Choices.Cols)
            {
                Options.Add(options);
            }

            RaisePropertyChanged("QuestionDetails");
            MainViewModel.Page.NavigationService?.GoBack();
        }

        public bool ValidateQuestionDetails()
        {
            if (QuestionDetails.Question == "" || QuestionDetails.Question.Length > 255)
            {
                MessageBox.Show("De vraag mag niet leeg zijn of langer zijn dan 255 karakters.");
                return false;
            }

            if (QuestionDetails.Description.Length > 500)
            {
                MessageBox.Show("De omschrijving mag niet langer zijn dan 500 karakters.");
                return false;
            }

            if (Options.Count < 2)
            {
                MessageBox.Show("Deze vraag moet minimaal 2 opties hebben.");
                return false;
            }

            return true;
        }

        public Question ToModel()
        {
            return _surveyQuestion;
        }

        private void AddOption()
        {
            if (OptionName == "" || OptionName.Length > 50)
            {
                MessageBox.Show("De optie mag niet leeg zijn of langer zijn dan 50 karakters.");
                return;
            }

            if (Options.Contains(OptionName))
            {
                MessageBox.Show("Deze optie bestaat al.");
                return;
            }

            if (Options.Count >= 10)
            {
                MessageBox.Show("Het maximum aantal opties is 10.");
                return;
            }

            Options.Add(OptionName);
            OptionName = "";
        }

        private void DeleteOption()
        {
            Options.Remove(SelectedOptionName);
        }
    }
}
