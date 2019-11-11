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
    public class OpenQuestionVM : IQuestion
    {
        private Question _surveyQuestion;

        public MainViewModel MainViewModel { get; set; }
        public QuestionDetails QuestionDetails { get; set; }
        public string QuestionType { get; }
        public ICommand SaveCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public OpenQuestionVM()
        {
            _surveyQuestion = new Question();
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public OpenQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionType = _surveyQuestion.Type;
            QuestionDetails = JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1);
            SaveCommand = new RelayCommand(Save);
            GoBackCommand = new RelayCommand(GoBack);
        }

        public void Save()
        {
            MessageBox.Show("Save");
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            MainViewModel.Page.NavigationService?.GoBack();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
