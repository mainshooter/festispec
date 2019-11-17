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
using Festispec.ViewModel.employee;
using Festispec.ViewModel.report;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question.QuestionTypes;
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

            SimpleIoc.Default.Register<ReportVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddElementVM>();
            SimpleIoc.Default.Register<EmployeeListVM>();
            SimpleIoc.Default.Register<AddEmployeeVM>();
            SimpleIoc.Default.Register<EmployeeVM>();
            SimpleIoc.Default.Register<EmployeeInfoVM>();
            SimpleIoc.Default.Register<EditEmployeeVM>();
            SimpleIoc.Default.Register<SurveyVM>();
            SimpleIoc.Default.Register<SurveyInfoVM>();
            SimpleIoc.Default.Register<ClosedQuestionVM>();
            SimpleIoc.Default.Register<CommentFieldVM>();
            SimpleIoc.Default.Register<DrawQuestionVM>();
            SimpleIoc.Default.Register<ImageGalleryQuestionVM>();
            SimpleIoc.Default.Register<MultipleChoiceQuestionVM>();
            SimpleIoc.Default.Register<OpenQuestionVM>();
            SimpleIoc.Default.Register<SliderQuestionVM>();
            SimpleIoc.Default.Register<TableQuestionVM>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();
        public ReportVM ReportVM => ServiceLocator.Current.GetInstance<ReportVM>();
        public AddEmployeeVM AddEmployeeVM => ServiceLocator.Current.GetInstance<AddEmployeeVM>();
        public EmployeeListVM EmployeeListVM => ServiceLocator.Current.GetInstance<EmployeeListVM>();
        public EmployeeInfoVM EmployeeInfoVM => ServiceLocator.Current.GetInstance<EmployeeInfoVM>();
        public EditEmployeeVM EditEmployeeVM => ServiceLocator.Current.GetInstance<EditEmployeeVM>();
        public SurveyInfoVM SurveyInfoVM => ServiceLocator.Current.GetInstance<SurveyInfoVM>();
        public CommentFieldVM CommentFieldVM => ServiceLocator.Current.GetInstance<CommentFieldVM>();
        public DrawQuestionVM DrawQuestionVM => ServiceLocator.Current.GetInstance<DrawQuestionVM>();
        public ImageGalleryQuestionVM ImageGalleryQuestionVM => ServiceLocator.Current.GetInstance<ImageGalleryQuestionVM>();
        public MultipleChoiceQuestionVM MultipleChoiceQuestionVM => ServiceLocator.Current.GetInstance<MultipleChoiceQuestionVM>();
        public OpenQuestionVM OpenQuestionVM => ServiceLocator.Current.GetInstance<OpenQuestionVM>();
        public SliderQuestionVM SliderQuestionVM => ServiceLocator.Current.GetInstance<SliderQuestionVM>();
        public TableQuestionVM TableQuestionVM => ServiceLocator.Current.GetInstance<TableQuestionVM>();
        public ClosedQuestionVM ClosedQuestionVM => ServiceLocator.Current.GetInstance<ClosedQuestionVM>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
