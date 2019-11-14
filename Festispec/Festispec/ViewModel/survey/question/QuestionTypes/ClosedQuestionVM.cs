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
    public class ClosedQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _description;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public ClosedQuestionVM(SurveyVM surveyVm, MainViewModel mainViewModel, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            MainViewModel = mainViewModel;
            QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
            _description = QuestionDetails.Description;
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    _description = QuestionDetails.Description;
                    QuestionDetails.Choices.Cols.Add("Ja");
                    QuestionDetails.Choices.Cols.Add("Nee");
                    _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);
                    context.Questions.Add(_surveyQuestion);
                    _surveyVm.Questions.Add(this);
                    context.SaveChanges();
                }
                else
                {
                    _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);
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

            if (QuestionDetails.Description == "" || QuestionDetails.Description.Length > 500)
            {
                MessageBox.Show("De omschrijving mag niet leeg zijn of langer zijn dan 500 karakters.");
                return false;
            }

            return true;
        }

        public Question ToModel()
        {
            return _surveyQuestion;
        }
    }
}
