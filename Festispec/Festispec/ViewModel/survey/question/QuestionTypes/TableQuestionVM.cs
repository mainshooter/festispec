using System.Collections.ObjectModel;
using System.Windows;
using Festispec.Domain;
using Festispec.Interface;
using Festispec.Lib.Survey.Question;
using Festispec.Message;
using Festispec.View.Pages.Survey;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Newtonsoft.Json;

namespace Festispec.ViewModel.survey.question.QuestionTypes
{
    public class TableQuestionVM : ViewModelBase, IQuestion
    {
        private readonly Question _surveyQuestion;
        private QuestionDetails _questionDetails;
        private string _optionName;
        private string _columnName;
        private ObservableCollection<string> _comboBoxItems;

        public int Id {
            get {
                return _surveyQuestion.Id;
            }
        }

        public QuestionDetails QuestionDetails
        {
            get => _questionDetails;
            set
            {
                _questionDetails = value;
                RaisePropertyChanged(() => QuestionDetails);
            }
        }

        public string QuestionType => _surveyQuestion.Type;

        public int SurveyId
        {
            get => _surveyQuestion.SurveyId;
            set => _surveyQuestion.SurveyId = value;
        }

        public string Question
        {
            get => _surveyQuestion.Question1;
            set => _surveyQuestion.Question1 = value;
        }

        public string Variables
        {
            get => _surveyQuestion.Variables;
            set => _surveyQuestion.Variables = value;
        }

        public string Type
        {
            get => _surveyQuestion.Type;
            set => _surveyQuestion.Type = value;
        }

        public int Order
        {
            get => _surveyQuestion.Order;
            set => _surveyQuestion.Order = value;
        }

        public string SelectedOptionName { get; set; }
        public string SelectedColumnName { get; set; }

        public string OptionName
        {
            get => _optionName;
            set
            {
                _optionName = value;
                RaisePropertyChanged(() => OptionName);
            }
        }

        public string ColumnName
        {
            get => _columnName;
            set
            {
                _columnName = value;
                RaisePropertyChanged(() => ColumnName);
            }
        }

        public string SelectedColumn
        {
            get => QuestionDetails.Choices.SelectedCol;
            set
            {
                QuestionDetails.Choices.SelectedCol = value;
                if (QuestionDetails.Choices.SelectedCol == "Geen") QuestionDetails.Choices.Options.Clear();
                RaisePropertyChanged(() => SelectedColumn);
            }
        }

        public ObservableCollection<string> ComboBoxItems {
            get => _comboBoxItems;
            set {
                _comboBoxItems = value;
                RaisePropertyChanged(() => ComboBoxItems);
            }
        }

        [PreferredConstructor]
        public TableQuestionVM()
        {
            _surveyQuestion = new Question();
            Type = Lib.Enums.QuestionType.TableQuestion;
            QuestionDetails = new QuestionDetails();
            ComboBoxItems = new ObservableCollection<string>();
        }

        public TableQuestionVM(Question surveyQuestion)
        {
            _surveyQuestion = surveyQuestion;
            QuestionDetails = _surveyQuestion.Question1 != null ? JsonConvert.DeserializeObject<QuestionDetails>(_surveyQuestion.Question1) : new QuestionDetails();
            ComboBoxItems = new ObservableCollection<string>();
        }

        public void GoBack()
        {
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

            if (QuestionDetails.Choices.Cols.Count < 2)
            {
                MessageBox.Show("Deze vraag moet minimaal 2 kolommen hebben.");
                return false;
            }

            if (SelectedColumn != "Geen" && QuestionDetails.Choices.Options.Count < 2)
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

        public void AddColumn()
        {
            if (string.IsNullOrEmpty(ColumnName) || ColumnName.Length > 50)
            {
                MessageBox.Show("De kolom mag niet leeg zijn of langer zijn dan 50 karakters.");
                return;
            }

            if (QuestionDetails.Choices.Cols.Contains(ColumnName))
            {
                MessageBox.Show("Deze kolom bestaat al.");
                return;
            }

            if (QuestionDetails.Choices.Cols.Count >= 5)
            {
                MessageBox.Show("Het maximum aantal kolommen is 5.");
                return;
            }

            QuestionDetails.Choices.Cols.Add(ColumnName);
            ColumnName = "";
            SetComboBox();
        }

        public void AddOption()
        {
            if (string.IsNullOrEmpty(OptionName) || OptionName.Length > 50)
            {
                MessageBox.Show("De optie mag niet leeg zijn of langer zijn dan 50 karakters.");
                return;
            }

            if (QuestionDetails.Choices.Options.Contains(OptionName))
            {
                MessageBox.Show("Deze optie bestaat al.");
                return;
            }

            if (QuestionDetails.Choices.Options.Count >= 10)
            {
                MessageBox.Show("Het maximum aantal opties is 10.");
                return;
            }

            QuestionDetails.Choices.Options.Add(OptionName);
            OptionName = "";
        }

        public bool CanAddOption()
        {
            return SelectedColumn != "Geen";
        }

        public void DeleteOption()
        {
            QuestionDetails.Choices.Options.Remove(SelectedOptionName);
        }

        public void DeleteColumn()
        {
            QuestionDetails.Choices.Cols.Remove(SelectedColumnName);
        }

        public void SetComboBox()
        {
            var selected = SelectedColumn;
            ComboBoxItems.Clear();
            ComboBoxItems.Add("Geen");

            foreach (var column in QuestionDetails.Choices.Cols)
            {
                ComboBoxItems.Add(column);
            }

            SelectedColumn = selected ?? ComboBoxItems[0];
        }
    }
}
