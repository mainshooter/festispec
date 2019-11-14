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
    public class ImageGalleryQuestionVM : ViewModelBase, IQuestion
    {
        private Question _surveyQuestion;
        private SurveyVM _surveyVm;
        private string _question;
        private int _maxImages;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType => _surveyQuestion.Type;
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }
        public int MaxImages { get; set; }

        public ImageGalleryQuestionVM(SurveyVM surveyVm, MainViewModel mainViewModel, Question surveyQuestion)
        {
            _surveyVm = surveyVm;
            _surveyQuestion = surveyQuestion;
            MainViewModel = mainViewModel;

            if (_surveyQuestion.Question1 != null)
            {
                QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
                MaxImages = Convert.ToInt32(QuestionDetails.Choices.Cols[0]);
            }
            else
            {
                QuestionDetails = new QuestionDetails();
            }

            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);

            // temp variables for when you want to go back and discard changes
            _question = QuestionDetails.Question;
            _maxImages = MaxImages;
        }

        public void Save()
        {
            using (var context = new Entities())
            {
                if (!ValidateQuestionDetails()) return;

                QuestionDetails.Choices.Cols.Clear();
                QuestionDetails.Choices.Cols.Add(MaxImages.ToString());
                _surveyQuestion.Question1 = JsonConvert.SerializeObject(QuestionDetails);

                if (_surveyQuestion.Id == 0)
                {
                    _question = QuestionDetails.Question;
                    _maxImages = MaxImages;
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
            MaxImages = _maxImages;
            RaisePropertyChanged("QuestionDetails");
            RaisePropertyChanged("MaxImages");
            MainViewModel.Page.NavigationService?.GoBack();
        }

        public bool ValidateQuestionDetails()
        {
            if (QuestionDetails.Question == "" || QuestionDetails.Question.Length > 255)
            {
                MessageBox.Show("De vraag mag niet leeg zijn of langer zijn dan 255 karakters.");
                return false;
            }

            if (MaxImages > 20)
            {
                MessageBox.Show("Het maximum aantal afbeeldingen is 20.");
                return false;
            }

            if (MaxImages <= 0)
            {
                MessageBox.Show("Het minimum aantal afbeeldingen is 1.");
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
