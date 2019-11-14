using System;
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
    public class SliderQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private string _description;
        private int _lowestNumber;
        private int _highestNumber;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public int LowestNumber { get; set; }
        public int HighestNumber { get; set; }

        public SliderQuestionVM(SurveyVM surveyVm, MainViewModel mainViewModel, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            MainViewModel = mainViewModel;

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
            LowestNumber = _lowestNumber;
            HighestNumber = _highestNumber;
            RaisePropertyChanged("QuestionDetails");
            RaisePropertyChanged("LowestNumber");
            RaisePropertyChanged("HighestNumber");
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
