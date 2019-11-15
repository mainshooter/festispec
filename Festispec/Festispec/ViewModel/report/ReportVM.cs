using Festispec.Domain;
using Festispec.Factory;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.report.element;
using Festispec.ViewModel.report;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using Festispec.Singleton;
using Festispec.View.Pages;
using CommonServiceLocator;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel.report
{
    public class ReportVM : ViewModelBase
    {
        private PageSingleton pageSingleton;
        private Report _report;
        private ReportElementFactory _reportElementFactory;

        public int Id {
            get {
                return _report.Id;
            }
            private set {
                _report.Id = value;
            }
        }

        public OrderVM Order { get; set; }

        public string Title {
            get {
                return _report.Title;
            }
            set {
                _report.Title = value;
            }
        }

        public string Status {
            get {
                return _report.Status;
            }
            set {
                _report.Status = value;
            }
        }

        public ObservableCollection<ReportElementVM> ReportElements { get; set; }
        
        public ObservableCollection<UserControl> ReportElementUserControlls { get; set; }

        public MainViewModel MainViewModel { get; set; }

        public ICommand SaveReportCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public ReportVM(PageSingleton pageSingleton)
        {
            this.pageSingleton = pageSingleton;
            _report = new Report();
            this.ReportElements = new ObservableCollection<ReportElementVM>(pageSingleton.CurrentReportElements);
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            _reportElementFactory = new ReportElementFactory();
            //ReportElements = new ObservableCollection<ReportElementVM>();
            ReportElements.CollectionChanged += RenderReportElements;
            SaveReportCommand = new RelayCommand(Save);
            AddElementCommand = new RelayCommand(GoToAddElementPage);
            this.RenderReportElements(null, null);
        }

        private void GoToAddElementPage()
        {
            Page addElementPage = new AddElementPage();
            AddElementVM addElementVM = new AddElementVM();
            addElementVM.Report = this;
            addElementVM.MainViewModel = MainViewModel;
            addElementPage.DataContext = addElementVM;
            MainViewModel.Page = addElementPage;
        }

        public Report ToModel()
        {
            return _report;
        }

        public void Save()
        {
            //if (Id == 0)
            //{
            //    Insert();
            //    return;
            //}
            //using (var context = new Entities())
            //{
            //    context.Reports.Attach(_report);
            //    context.Entry(_report).State = System.Data.Entity.EntityState.Modified;
            //    context.SaveChanges();
            //}
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage { NextPageType = typeof(DashboardPage) });
        }

        private void Insert()
        {
            using (var context = new Entities())
            {
                context.Reports.Add(_report);
                context.SaveChanges();
            }
        }

        public void RenderReportElements(object sender, NotifyCollectionChangedEventArgs e)
        {
            ReportElementUserControlls.Clear();
            var reportElements = ReportElements.OrderBy(el => el.Order);
            foreach (var element in reportElements)
            {
                ReportElementUserControlls.Add(_reportElementFactory.CreateElement(element));
            }
        }
    }
}
