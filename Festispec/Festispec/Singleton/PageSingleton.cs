using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Employee;
using System.Collections.Generic;
using System.Windows.Controls;
using Festispec.Domain;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee.Availability;
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.ClosedQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using Festispec.ViewModel;
using Festispec.ViewModel.survey;
using Festispec.ViewModel.survey.question.questionTypes;
using Festispec.ViewModel.survey.question.QuestionTypes;

namespace Festispec.Singleton
{
    public class PageSingleton
    {
        private Dictionary<string, Page> _pages;
        private MainViewModel _mainViewModel;

        public PageSingleton(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _pages = new Dictionary<string, Page>
            {
                {"dashboard", new DashboardPage()},
                {"employee", new EmployeePage()},
                {"customer", new CustomerPage()},
                {"availability", new AvailablePage()},
                {"event", new EventPage()},
                {"sick", new SickPage()},
                {"survey", new SurveyPage()}
            };
        }

        public Page GetPage(string pageName)
        {
            return _pages[pageName];
        }

        public void SetSurveyPages(SurveyVM surveyVm)
        {
            var addOpenQuestionPage = new AddOpenQuestionPage();
            var openQuestionVm = new OpenQuestionVM(surveyVm);
            openQuestionVm.MainViewModel = _mainViewModel;
            addOpenQuestionPage.DataContext = openQuestionVm;
            _pages["Add Open vraag"] = addOpenQuestionPage;

            _pages["Edit Open vraag"] = new EditOpenQuestionPage();

            var addClosedQuestionPage = new AddClosedQuestionPage();
            var closedQuestionVm = new ClosedQuestionVM(surveyVm);
            closedQuestionVm.MainViewModel = _mainViewModel;
            addClosedQuestionPage.DataContext = closedQuestionVm;
            _pages["Add Gesloten vraag"] = addClosedQuestionPage;

            _pages["Edit Gesloten vraag"] = new EditClosedQuestionPage();
        }
    }
}
