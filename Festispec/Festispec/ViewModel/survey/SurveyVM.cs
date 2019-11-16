using Festispec.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using Festispec.Interface;
using Festispec.ViewModel.survey.question.QuestionTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Festispec.ViewModel.survey.question;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM : ViewModelBase
    {
        private Survey _survey;
        private ObservableCollection<IQuestion> _questions;

        public int Id => _survey.Id;
        public ObservableCollection<CaseVM> Cases { get; set; }
        public ObservableCollection<string> QuestionTypes { get; set; }

        public string Description
        {
            get => _survey.Description;
            set => _survey.Description = value;
        }

        public string Status
        {
            get => _survey.Status;
            set => _survey.Status = value;
        }

        public ObservableCollection<IQuestion> Questions
        {
            get => _questions;
            set {
                _questions = value;
                RaisePropertyChanged();
            }
        }

        [PreferredConstructor]
        public SurveyVM()
        {
            _survey = new Domain.Survey();
            Cases = new ObservableCollection<CaseVM>();
            Questions = new ObservableCollection<IQuestion>();
        }

        public SurveyVM(Survey survey)
        {
            _survey = survey;
            Cases = new ObservableCollection<CaseVM>(survey.Cases.ToList().Select(c => new CaseVM(c)));
            RefreshQuestions();
        }

        public Survey ToModel()
        {
            return _survey;
        }

        public void RefreshQuestions()
        {
            Questions = new ObservableCollection<IQuestion>(_survey.Questions.ToList().Select(q => CreateQuestionType(new QuestionVM(q))));
        }

        private IQuestion CreateQuestionType(QuestionVM question)
        {
            switch (question.Type)
            {
                case "Open vraag":
                    return new OpenQuestionVM(this, question.ToModel());
                case "Gesloten vraag":
                    return new ClosedQuestionVM(this, question.ToModel());
                case "Schuifbalk vraag":
                    return new SliderQuestionVM(this, question.ToModel());
                case "Opmerking veld":
                    return new CommentFieldVM(this, question.ToModel());
                case "Afbeelding galerij vraag":
                    return new ImageGalleryQuestionVM(this, question.ToModel());
                case "Teken vraag":
                    return new DrawQuestionVM(this, question.ToModel());
                case "Meerkeuze vraag":
                    return new MultipleChoiceQuestionVM(this, question.ToModel());
                case "Tabel vraag":
                    return new TableQuestionVM(this, question.ToModel());
                default:
                    return new OpenQuestionVM(this, question.ToModel());
            }
        }
    }
}
