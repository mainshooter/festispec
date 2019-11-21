using CommonServiceLocator;
using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee;
using Festispec.View.Pages.Employee.Availability;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.ClosedQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.CommentField;
using Festispec.View.Pages.Survey.QuestionTypes.DrawQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.ImageGalleryQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.MultipleChoiceQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.SliderQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.TableQuestion;
using Festispec.ViewModel.auth;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.report;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question.QuestionTypes.ClosedQuestion;
using Festispec.ViewModel.survey.question.QuestionTypes.CommentField;
using Festispec.ViewModel.survey.question.QuestionTypes.DrawQuestion;
using Festispec.ViewModel.survey.question.QuestionTypes.ImageGalleryQuestion;
using Festispec.ViewModel.survey.question.QuestionTypes.OpenQuestion;
using Festispec.ViewModel.survey.question.QuestionTypes.SliderQuestion;
using GalaSoft.MvvmLight.Ioc;

namespace Festispec.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<DashboardPage>();
            SimpleIoc.Default.Register<ReportPage>();
            SimpleIoc.Default.Register<CustomerPage>();
            SimpleIoc.Default.Register<EventPage>();
            SimpleIoc.Default.Register<AvailablePage>();
            SimpleIoc.Default.Register<EmployeePage>();
            SimpleIoc.Default.Register<SickPage>();
            SimpleIoc.Default.Register<AddElementPage>();
            SimpleIoc.Default.Register<EmployeePage>();
            SimpleIoc.Default.Register<AddEmployeePage>();
            SimpleIoc.Default.Register<SingleEmployeePage>();
            SimpleIoc.Default.Register<EditEmployeePage>();
            SimpleIoc.Default.Register<SurveyPage>();
            SimpleIoc.Default.Register<AddClosedQuestionPage>();
            SimpleIoc.Default.Register<EditClosedQuestionPage>();
            SimpleIoc.Default.Register<AddCommentFieldPage>();
            SimpleIoc.Default.Register<EditCommentFieldPage>();
            SimpleIoc.Default.Register<AddDrawQuestionPage>();
            SimpleIoc.Default.Register<EditDrawQuestionPage>();
            SimpleIoc.Default.Register<AddImageGalleryQuestionPage>();
            SimpleIoc.Default.Register<EditImageGalleryQuestionPage>();
            SimpleIoc.Default.Register<AddMultipleChoiceQuestionPage>();
            SimpleIoc.Default.Register<EditMultipleChoiceQuestionPage>();
            SimpleIoc.Default.Register<AddOpenQuestionPage>();
            SimpleIoc.Default.Register<EditOpenQuestionPage>();
            SimpleIoc.Default.Register<AddSliderQuestionPage>();
            SimpleIoc.Default.Register<EditSliderQuestionPage>();
            SimpleIoc.Default.Register<AddTableQuestionPage>();
            SimpleIoc.Default.Register<EditTableQuestionPage>();
            SimpleIoc.Default.Register<LoginPage>();

            SimpleIoc.Default.Register<ReportVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<EmployeeVM>();
            SimpleIoc.Default.Register<EmployeeListVM>();
            SimpleIoc.Default.Register<AddEmployeeVM>();
            SimpleIoc.Default.Register<AddElementVM>();
            SimpleIoc.Default.Register<EmployeeInfoVM>();
            SimpleIoc.Default.Register<EditEmployeeVM>();
            SimpleIoc.Default.Register<SurveyVM>();
            SimpleIoc.Default.Register<SurveyInfoVM>();

            SimpleIoc.Default.Register<AddOpenQuestionVM>();
            SimpleIoc.Default.Register<EditOpenQuestionVM>();
            SimpleIoc.Default.Register<AddClosedQuestionVM>();
            SimpleIoc.Default.Register<EditClosedQuestionVM>();
            SimpleIoc.Default.Register<AddSliderQuestionVM>();
            SimpleIoc.Default.Register<EditSliderQuestionVM>();
            SimpleIoc.Default.Register<AddCommentFieldVM>();
            SimpleIoc.Default.Register<EditCommentFieldVM>();
            SimpleIoc.Default.Register<AddImageGalleryQuestionVM>();
            SimpleIoc.Default.Register<EditImageGalleryQuestionVM>();
            SimpleIoc.Default.Register<AddDrawQuestionVM>();
            SimpleIoc.Default.Register<EditDrawQuestionVM>();

            SimpleIoc.Default.Register<UserLoginVM>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ReportVM ReportVM => ServiceLocator.Current.GetInstance<ReportVM>();
        public UserLoginVM UserLoginVM => ServiceLocator.Current.GetInstance<UserLoginVM>();
        public AddEmployeeVM AddEmployeeVM => ServiceLocator.Current.GetInstance<AddEmployeeVM>();
        public EmployeeListVM EmployeeListVM => ServiceLocator.Current.GetInstance<EmployeeListVM>();
        public EmployeeInfoVM EmployeeInfoVM => ServiceLocator.Current.GetInstance<EmployeeInfoVM>();
        public EditEmployeeVM EditEmployeeVM => ServiceLocator.Current.GetInstance<EditEmployeeVM>();
        public SurveyInfoVM SurveyInfoVM => ServiceLocator.Current.GetInstance<SurveyInfoVM>();

        public AddOpenQuestionVM AddOpenQuestionVM => ServiceLocator.Current.GetInstance<AddOpenQuestionVM>();
        public EditOpenQuestionVM EditOpenQuestionVM => ServiceLocator.Current.GetInstance<EditOpenQuestionVM>();
        public AddClosedQuestionVM AddClosedQuestionVM => ServiceLocator.Current.GetInstance<AddClosedQuestionVM>();
        public EditClosedQuestionVM EditClosedQuestionVM => ServiceLocator.Current.GetInstance<EditClosedQuestionVM>();
        public AddSliderQuestionVM AddSliderQuestionVM => ServiceLocator.Current.GetInstance<AddSliderQuestionVM>();
        public EditSliderQuestionVM EditSliderQuestionVM => ServiceLocator.Current.GetInstance<EditSliderQuestionVM>();
        public AddCommentFieldVM AddCommentFieldVM => ServiceLocator.Current.GetInstance<AddCommentFieldVM>();
        public EditCommentFieldVM EditCommentFieldVM => ServiceLocator.Current.GetInstance<EditCommentFieldVM>();
        public AddImageGalleryQuestionVM AddImageQuestionVM => ServiceLocator.Current.GetInstance<AddImageGalleryQuestionVM>();
        public EditImageGalleryQuestionVM EditImageQuestionVM => ServiceLocator.Current.GetInstance<EditImageGalleryQuestionVM>();
        public AddDrawQuestionVM AddDrawQuestionVM => ServiceLocator.Current.GetInstance<AddDrawQuestionVM>();
        public EditDrawQuestionVM EditDrawQuestionVM => ServiceLocator.Current.GetInstance<EditDrawQuestionVM>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
