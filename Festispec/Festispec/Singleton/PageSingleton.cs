﻿using Festispec.View.Pages;
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

namespace Festispec.Singleton
{
    public class PageSingleton
    {
        private Dictionary<string, Page> _pages;

        public PageSingleton()
        {
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
            _pages["Add Open vraag"] = new AddOpenQuestionPage();
            _pages["Edit Open vraag"] = new EditOpenQuestionPage();
            _pages["Add Gesloten vraag"] = new AddClosedQuestionPage();
            _pages["Edit Gesloten vraag"] = new EditClosedQuestionPage();
        }
    }
}
