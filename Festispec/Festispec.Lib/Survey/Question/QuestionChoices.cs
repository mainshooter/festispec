using System.Collections.ObjectModel;

namespace Festispec.Lib.Survey.Question
{
    public class QuestionChoices
    {
        public ObservableCollection<string> Cols { get; set; }
        public ObservableCollection<string> Options { get; set; }
        public string SelectedCol { get; set; }

        public QuestionChoices()
        {
            Cols = new ObservableCollection<string>();
            Options = new ObservableCollection<string>();
        }
    }
}
