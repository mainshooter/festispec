using System;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Slugify;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class SliderQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _description;
        private int _lowestNumber;
        private int _highestNumber;
        private string _questionType;
        private QuestionDetails _questionDetails;

        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public int LowestNumber { get; set; }
        public int HighestNumber { get; set; }

        public QuestionDetails QuestionDetails
        {
            get => _questionDetails;
            set {
                _questionDetails = value;
                RaisePropertyChanged();
            }
        }

        [PreferredConstructor]
        public SliderQuestionVM()
        {
            _questionType = "Schuifbalk vraag";
            MessengerInstance.Register<ChangeSelectedSurveyQuestionMessage>(this, message => {
                _surveyVm = message.SurveyVM;
                //_surveyQuestion = message.NextQuestion;
                QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
                try
                {
                    LowestNumber = Convert.ToInt32(QuestionDetails.Choices.Cols[0]);
                    HighestNumber = Convert.ToInt32(QuestionDetails.Choices.Cols[1]);

                    _question = QuestionDetails.Question;
                    _description = QuestionDetails.Description;
                    _lowestNumber = LowestNumber;
                    _highestNumber = HighestNumber;
                    RaisePropertyChanged("LowestNumber");
                    RaisePropertyChanged("HighestNumber");
                }
                catch (Exception)
                {
                }

            });
            MessengerInstance.Register<ChangeSelectedSurveyMessage>(this, message => {
                _surveyVm = message.NextSurvey;
                _surveyQuestion = new Question();
                QuestionDetails = new QuestionDetails();
            });
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public SliderQuestionVM(SurveyVM surveyVm, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;

            if (_surveyQuestion.Question1 != null)
            {
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
                LowestNumber = Convert.ToInt32(QuestionDetails.Choices.Cols[0]);
                HighestNumber = Convert.ToInt32(QuestionDetails.Choices.Cols[1]);
            }
            else
            {
                QuestionDetails = new QuestionDetails();
            }

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
            _description = QuestionDetails.Description;
            _lowestNumber = LowestNumber;
            _highestNumber = HighestNumber;
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                QuestionDetails.Choices.Cols.Clear();
                QuestionDetails.Choices.Cols.Add(LowestNumber.ToString());
                QuestionDetails.Choices.Cols.Add(HighestNumber.ToString());
                _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    _description = QuestionDetails.Description;
                    _lowestNumber = LowestNumber;
                    _highestNumber = HighestNumber;
                    _surveyQuestion.SurveyId = _surveyVm.ToModel().Id;
                    _surveyQuestion.Variables = StringToSlug.Slugify(QuestionDetails.Question);
                    _surveyQuestion.Type = _questionType;
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

            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
        }

        public void GoBack()
        {
            QuestionDetails.Question = _question;
            QuestionDetails.Description = _description;
            LowestNumber = _lowestNumber;
            HighestNumber = _highestNumber;
            RaisePropertyChanged("QuestionDetails");
            RaisePropertyChanged("LowestNumber");
            RaisePropertyChanged("HighestNumber");
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
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

            if (LowestNumber <= 0)
            {
                MessageBox.Show("De cijfers moeten positief zijn.");
                return false;
            }

            if (HighestNumber > 1000)
            {
                MessageBox.Show("De cijfers mogen niet hoger dan 1000 zijn.");
                return false;
            }

            if (LowestNumber >= HighestNumber)
            {
                MessageBox.Show("Het laagste cijfer mag niet gelijk of groter zijn dan het hoogste nummer en het moeten cijfers zijn.");
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
