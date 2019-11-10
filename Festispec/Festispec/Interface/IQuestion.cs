using Festispec.Model;

namespace Festispec.ViewModel.survey.question.questionTypes
{
    public interface IQuestion
    {
        QuestionDetails QuestionDetails { get; set; }
        string QuestionType { get; }
        void Save();
        void Delete();
        void Refresh();
    }
}
