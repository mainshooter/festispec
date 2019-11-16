using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Employee;
using System.Collections.Generic;
using System.Windows.Controls;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Employee.Availability;
<<<<<<< HEAD
using Festispec.View.Pages.Survey;
using Festispec.View.Pages.Survey.QuestionTypes.ClosedQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.CommentField;
using Festispec.View.Pages.Survey.QuestionTypes.DrawQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.ImageGalleryQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.MultipleChoiceQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.OpenQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.SliderQuestion;
using Festispec.View.Pages.Survey.QuestionTypes.TableQuestion;
=======
using LiveCharts.Wpf;
using LiveCharts;
using Festispec.ViewModel.report.element;
using System;
using Festispec.ViewModel.report;
using Festispec.View.Report;
using Festispec.ViewModel;
>>>>>>> develop

namespace Festispec.Singleton
{
    public class PageSingleton
    {
        private Dictionary<string, Page> _pages;

<<<<<<< HEAD
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
=======
        public PageSingleton(MainViewModel mainViewModel)
        {          
            _pages = new Dictionary<string, Page>();
            _pages.Add("dashboard", new DashboardPage());
            _pages.Add("employee", new EmployeePage());
            _pages.Add("customer", new CustomerPage());
            _pages.Add("availability", new AvailablePage());
            _pages.Add("event", new EventPage());
            _pages.Add("sick", new SickPage());
            AddReportPage(mainViewModel);
        }

        private void AddReportPage(MainViewModel mainViewModel)
        {
            var report = new ReportPage();
            ReportVM reportVM = new ReportVM();

            reportVM.ReportElements.Add(
                    new ReportElementVM()
                    {
                        Title = "Leuke titel",
                        Content = "Hier maak ik titels van",
                        Type = "table",
                        Data = new Dictionary<string, List<string>>()
                        {
                            ["id"] = new List<string>() { "1", "2" },
                            ["namen"] = new List<string>() { "Peter", "Mike", "Wout" }
                        },
                    }
                );
            reportVM.ReportElements.Add(
                new ReportElementVM()
                {
                    Title = "Leuke piechart",
                    Content = "Taartje beschrijving",
                    Type = "piechart",
                    Data = new SeriesCollection
                    {
                        new PieSeries
                        {
                            Title = "Bier",
                            Values = new ChartValues<double> { 20 },
                            DataLabels = true,
                        },
                        new PieSeries
                        {
                            Title = "Frisdrank",
                            Values = new ChartValues<double> { 12 },
                            DataLabels = true,
                        },
                        new PieSeries
                        {
                            Title = "Cocktail",
                            Values = new ChartValues<double> { 8 },
                            DataLabels = true,
                        },
                        new PieSeries
                        {
                            Title = "Wijn",
                            Values = new ChartValues<double> { 2 },
                            DataLabels = true,
                        }
                    }
                }
            );
            reportVM.ReportElements.Add(
                new ReportElementVM()
                {
                    Title = "Line chart",
                    Content = "Wij linecharten",
                    Type = "linechart",
                    Data = new Dictionary<string, Object>()
                    {
                        ["xaxisName"] = "Test xas",
                        ["yaxisName"] = "Test yas",
                        ["seriescollection"] = new SeriesCollection
                        {
                            new LineSeries { Title = "Bezoekers", Values = new ChartValues<int> {40, 60, 50, 20, 40, 60}}
                        }
                    }
                }
            );
            reportVM.ReportElements.Add(
               new ReportElementVM()
               {
                   Title = "test",
                   Content = "ttest",
                   Type = "barchart",
                   Data = new Dictionary<string, Object>()
                   {
                       ["xaxisName"] = "Place",
                       ["yaxisName"] = "Amount",
                       ["labels"] = new List<string> { "test1", "test2", "test3", "test4", "test5" },
                       ["seriescollection"] = new SeriesCollection
                       {
                            new ColumnSeries {Title = "testdata" , Values = new ChartValues<int>{10,20,30,40,50} },

                            new ColumnSeries {Title = "testdata2" , Values = new ChartValues<int>{15,25,35,45,55} }
                       }
                   }
               }
            );
            reportVM.ReportElements.Add(
                new ReportElementVM()
                {
                    Title = "text",
                    Content = "test text",
                    Type = "text",
                    Data = new Dictionary<string, Object>()
                    {
                        ["text"] = "test text smiley"
                    }
                }
            );
            reportVM.ReportElements.Add(
                new ReportElementVM()
                {
                    Title = "image",
                    Content = "local image",
                    Type = "image",
                    Data = new Dictionary<string, Object>()
                    {
                        ["image"] = new byte[0]
                    }
                }
            );
            reportVM.MainViewModel = mainViewModel;
            report.DataContext = reportVM;

            _pages.Add("report", report);
>>>>>>> develop
        }

        public Page GetPage(string pageName)
        {
            return _pages[pageName];
        }

        public void SetSurveyPages()
        {
            _pages["Add Open vraag"] = new AddOpenQuestionPage();
            _pages["Edit Open vraag"] = new EditOpenQuestionPage();
            _pages["Add Gesloten vraag"] = new AddClosedQuestionPage();
            _pages["Edit Gesloten vraag"] = new EditClosedQuestionPage();
            _pages["Add Schuifbalk vraag"] = new AddSliderQuestionPage();
            _pages["Edit Schuifbalk vraag"] = new EditSliderQuestionPage();
            _pages["Add Opmerking veld"] = new AddCommentFieldPage();
            _pages["Edit Opmerking veld"] = new EditCommentFieldPage();
            _pages["Add Afbeelding galerij vraag"] = new AddImageGalleryQuestionPage();
            _pages["Edit Afbeelding galerij vraag"] = new EditImageGalleryQuestionPage();
            _pages["Add Teken vraag"] = new AddDrawQuestionPage();
            _pages["Edit Teken vraag"] = new EditDrawQuestionPage();
            _pages["Add Meerkeuze vraag"] = new AddMultipleChoiceQuestionPage();
            _pages["Edit Meerkeuze vraag"] = new EditMultipleChoiceQuestionPage();
            _pages["Add Tabel vraag"] = new AddTableQuestionPage();
            _pages["Edit Tabel vraag"] = new EditTableQuestionPage();
        }
    }
}
