using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class CommentFieldVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _questionType;

        private QuestionDetails _questionDetails;
        public QuestionDetails QuestionDetails {
            get {
                return _questionDetails;
            }
            set {
                _questionDetails = value;
                RaisePropertyChanged("QuestionDetails");
            }
        }
        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        [PreferredConstructor]
        public CommentFieldVM()
        {
            _questionType = "Opmerking vraag";
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message => {
                _surveyVm = message.SurveyVM;
                _surveyQuestion = message.NextQuestion;
                QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
                _question = QuestionDetails.Question;
            });
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => {
                _surveyVm = message.NextSurvey;
                _surveyQuestion = new Question();
                QuestionDetails = new QuestionDetails();
            });
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public CommentFieldVM(SurveyVM surveyVm, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    context.Questions.Add(_surveyQuestion);
                    _surveyQuestion.Variables = "test";
                    _surveyQuestion.Type = _questionType;
                    _surveyQuestion.SurveyId = _surveyVm.ToModel().Id;
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

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }

        public void GoBack()
        {
            QuestionDetails.Question = _question;
            RaisePropertyChanged("QuestionDetails");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }

        public bool ValidateQuestionDetails()
        {
            if (QuestionDetails.Question == "" || QuestionDetails.Question.Length > 255)
            {
                MessageBox.Show("De vraag mag niet leeg zijn of langer zijn dan 255 karakters.");
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
