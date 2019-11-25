using System;
using Festispec.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Festispec.Interface;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.survey.question.QuestionTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM : ViewModelBase
    {
        private readonly Survey _survey;
        private ObservableCollection<IQuestion> _questions;
        private ObservableCollection<string> _statuses;

        public int Id => _survey.Id;
        public string EventName => OrderVM.Event.Name;

        public ObservableCollection<string> Statuses
        {
            get => _statuses;
            set
            {
                _statuses = value;
                RaisePropertyChanged(() => Statuses);
            }
        }

        public string Description
        {
            get => _survey.Description;
            set => _survey.Description = value;
        }

        public string Status
        {
            get => _survey.Status;
            set
            {
                _survey.Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public ObservableCollection<IQuestion> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        public OrderVM OrderVM { get; set; }

        [PreferredConstructor]
        public SurveyVM(OrderVM order)
        {
            _survey = new Survey();
            Questions = new ObservableCollection<IQuestion>(Questions.OrderBy(q => q.Order));
            OrderVM = order;
            SetStatuses();
        }

        public SurveyVM(OrderVM order, Survey survey)
        {
            _survey = survey;
            Questions = new ObservableCollection<IQuestion>(survey.Questions.ToList().Select(CreateQuestionType).OrderBy(q => q.Order));
            OrderVM = order;
            SetStatuses();
        }

        public Survey ToModel()
        {
            return _survey;
        }

        private IQuestion CreateQuestionType(Question question)
        {
            switch (question.Type)
            {
                case "Open vraag":
                    return new OpenQuestionVM(question);
                case "Gesloten vraag":
                    return new ClosedQuestionVM(question);
                case "Schuifbalk vraag":
                    return new SliderQuestionVM(question);
                case "Opmerking vraag":
                    return new CommentFieldVM(question);
                case "Afbeelding galerij vraag":
                    return new ImageGalleryQuestionVM(question);
                case "Teken vraag":
                    return new DrawQuestionVM(question);
                case "Meerkeuze vraag":
                    return new MultipleChoiceQuestionVM(question);
                case "Tabel vraag":
                    return new TableQuestionVM(question);
                default:
                    throw new NotSupportedException("Question type not supported.");
            }
        }

        public void RefreshQuestions()
        {
            using (var context = new Entities())
            {
                Questions = new ObservableCollection<IQuestion>(context.Questions.Where(s => s.SurveyId == _survey.Id).ToList().Select(CreateQuestionType));
            }
        }

        public void SetStatuses()
        {
            Statuses = new ObservableCollection<string>();

            using (var context = new Entities())
            {
                var statuses = context.SurveyStatus.ToList();

                foreach (var status in statuses)
                {
                    Statuses.Add(status.Status);
                }
            }
        }

        public bool ValidateInputs()
        {
            if (Description == null || Description.Equals(""))
            {
                MessageBox.Show("Instructies mag niet leeg zijn.");
            }

            if (Description.Length > 10000)
            {
                MessageBox.Show("Instructies mag niet langer zijn dan 10.000 karakters.");
            }

            return true;
        }
    }
}
