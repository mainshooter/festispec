using CommonServiceLocator;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question.QuestionTypes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OpenQuestionVM>();
            SimpleIoc.Default.Register<ClosedQuestionVM>();
            SimpleIoc.Default.Register(() => new SurveyVM());
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public OpenQuestionVM OpenQuestion => ServiceLocator.Current.GetInstance<OpenQuestionVM>();
        public ClosedQuestionVM ClosedQuestion => ServiceLocator.Current.GetInstance<ClosedQuestionVM>();
        public SurveyVM Survey => ServiceLocator.Current.GetInstance<SurveyVM>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}