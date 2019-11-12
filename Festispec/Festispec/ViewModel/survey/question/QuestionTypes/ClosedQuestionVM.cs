using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Survey.Question;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class ClosedQuestionVM : IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public ClosedQuestionVM(SurveyVM surveyVm, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                if (_surveyQuestion.Id == 0)
                {
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

            GoBack();
        }

        public void GoBack()
        {
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
