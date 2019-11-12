using System;
using System.Windows;
using System.Windows.Input;
using Festispec.Domain;
using Festispec.Survey.Question;
using Festispec.ViewModel.survey.question.questionTypes;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    class SliderQuestionVM : IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public int Id => _surveyQuestion.Id;

        public string QuestionType
        {
            get => _surveyQuestion.Type;
            set => _surveyQuestion.Type = value;
        }

        public string Question
        {
            get => _surveyQuestion.Question1;
            set => _surveyQuestion.Question1 = value;
        }

        public int Order
        {
            get => _surveyQuestion.Order;
            set => _surveyQuestion.Order = value;
        }

        public int LowestNumber { get; set; }
        public int HighestNumber { get; set; }

        public SliderQuestionVM(SurveyVM surveyVm, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            QuestionType = _surveyQuestion.Type;

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
