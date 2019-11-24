using System.Linq;
using Festispec.View.Pages.Employee;
using Festispec.ViewModel.employee;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using CommonServiceLocator;
using Festispec.View.Pages;
using Festispec.View.Pages.Customer;
using Festispec.View.Pages.Employee.Availability;
using Festispec.View.Pages.Customer.Event;
using Festispec.Message;
using Festispec.View.Pages.Planning;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.customer.customerEvent;
using Festispec.Domain;
using System.Collections.Generic;
using Festispec.View.Pages.Survey;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //privates
        private Page _page;
        private EmployeeVM _loggedInEmployee;

        //publics
        public ICommand CloseApplication { get; set; }
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenEmployee { get; set; }
        public ICommand OpenCustomer { get; set; }
        public ICommand OpenAvailability { get; set; }
        public ICommand OpenEvent { get; set; }
        public ICommand OpenSick { get; set; }
        public ICommand OpenPlanning { get; set; }
        public ICommand OpenSurvey { get; set; }

        public Page Page
        {
            get => _page;
            set { _page = value; RaisePropertyChanged("Page"); }
        }

        public EmployeeVM LoggedInEmployee {
            get {
                return _loggedInEmployee;
            }
            set {
                _loggedInEmployee = value;
                RaisePropertyChanged("LoggedInEmployee");
            }
        }

        //constructor
        public MainViewModel()
        {
            CloseApplication = new RelayCommand(CloseApp);
            OpenDashboard = new RelayCommand(OpenDashboardTab);
            OpenEmployee = new RelayCommand(OpenEmployeeTab);
            OpenCustomer = new RelayCommand(OpenCustomerTab);
            OpenEvent = new RelayCommand(OpenEventTab);
            OpenAvailability = new RelayCommand(OpenAvailabilityTab);
            OpenSick = new RelayCommand(OpenSickTab);
            OpenPlanning = new RelayCommand(OpenPlanningTab);
            OpenSurvey = new RelayCommand(OpenSurveyTab);

            Page = ServiceLocator.Current.GetInstance<LoginPage>();

            this.MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                this.Page = ServiceLocator.Current.GetInstance(message.NextPageType) as Page;
            });

            this.MessengerInstance.Register<ChangeLoggedinUserMessage>(this, message =>
            {
                LoggedInEmployee = message.LoggedinEmployee;
            });
        }


        //methodes
        private void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OpenDashboardTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(DashboardPage) });
        }

        public void OpenEmployeeTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EmployeePage) });
        }

        private void OpenCustomerTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(CustomerPage) });
        }

        private void OpenAvailabilityTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AvailablePage) });
        }

        private void OpenEventTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
        }

        private void OpenSickTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SickPage)});
        }

        private void OpenPlanningTab()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
        }

        //Voorbeeld van specifieke event
        private void OpenSpecificPlanningTab()
        {
            using (var context = new Entities())
            {
                List<Order> order = context.Orders.ToList();
                OrderVM orderVM = new OrderVM(order.FirstOrDefault());
                EventVM eventVM = orderVM.Event;
                eventVM.OrderVM = orderVM;
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(PlanningOverviewPage) });
                MessengerInstance.Send<ChangeSelectedEventVM>(new ChangeSelectedEventVM() { NextEvent = eventVM });
            }
        }

        private void OpenSurveyTab()
        {
            using (var context = new Entities())
            {

                var evenement = context.Events.ToList().Select(e => new EventVM(e)).Last();
                var order = context.Orders.ToList().Select(o => new OrderVM(o)).Last();
                order.Event = evenement;
                evenement.OrderVM = order;
                //var surveyDomain = context.Surveys.First();
                //var survey = new SurveyVM(order, surveyDomain);
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(SurveyPage) });
                MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = order.Survey });

//                var evenement = context.Events.ToList().Select(e => new EventVM(e)).Last();
//                var order = context.Orders.ToList().Select(o => new OrderVM(o)).Last();
//                order.Event = evenement;
//                evenement.OrderVM = order;
//                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddSurveyPage)});
//                MessengerInstance.Send<ChangeSelectedSurveyMessage>(new ChangeSelectedSurveyMessage() { NextSurvey = evenement.OrderVM.Survey });
            }
        }
    }
}
