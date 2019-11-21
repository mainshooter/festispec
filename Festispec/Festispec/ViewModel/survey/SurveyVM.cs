using System;
using Festispec.Domain;
using System.Collections.ObjectModel;
using System.Linq;
using Festispec.Interface;
using Festispec.ViewModel.survey.question.QuestionTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM : ViewModelBase
    {
        private readonly Survey _survey;
        private ObservableCollection<IQuestion> _questions;

        public int Id => _survey.Id;

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
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        [PreferredConstructor]
        public SurveyVM()
        {
            _survey = new Survey();
            Questions = new ObservableCollection<IQuestion>();
        }

        public SurveyVM(Survey survey)
        {
            _survey = survey;
            Questions = new ObservableCollection<IQuestion>(survey.Questions.ToList().Select(CreateQuestionType));
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
                    return new DrawQuestionVM(this, question);
                case "Meerkeuze vraag":
                    return new MultipleChoiceQuestionVM(this, question);
                case "Tabel vraag":
                    return new TableQuestionVM(this, question);
                default:
                    throw new NotSupportedException("Question type not supported.");
            }
        }

        public void RefreshQuestions()
        {
            using (var context = new Entities())
            {
                Questions = new ObservableCollection<IQuestion>(context.Questions.Where(s => s.SurveyId == _survey.Id).ToList().Select(CreateQuestionType));
            };
        }
    }
}
