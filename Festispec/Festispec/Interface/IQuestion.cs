namespace Festispec.ViewModel.survey.question.questionTypes
{
    public interface IQuestion
    {
        string QuestionString { get; set; }
        string QuestionType { get; }

        void GetQuestion();
        void Save();
        void Delete();
        void Refresh();
    }
}
