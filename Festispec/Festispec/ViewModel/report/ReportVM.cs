using Festispec.Domain;
using Festispec.Factory;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Festispec.Repository;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.Customer.order;

namespace Festispec.ViewModel.report
{
    public class ReportVM : ViewModelBase
    {
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
                RaisePropertyChanged("Title");
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

        public ReportVM()
        {
            _report = new Report();
            _report.Id = 2;
            _report.Title = "Test titel";
            var reportRepository = new ReportRepository();
            this.ReportElements = new ObservableCollection<ReportElementVM>(reportRepository.GetReportElements(this));
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            _reportElementFactory = new ReportElementFactory();
            ReportElements.CollectionChanged += RenderReportElements;
            SaveReportCommand = new RelayCommand(Save);
            AddElementCommand = new RelayCommand(GoToAddElementPage);

            this.RenderReportElements(null, null);

            //ReportElementVM reportElementVM = (ReportElementVM) ReportElementUserControlls.First().DataContext;
            //ReportElementUserControlls.RemoveAt(0);
            //reportElementVM.Id;
        }

        private void GoToAddElementPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddElementPage)});
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
              NextReportVM = this
            });
        }

        public Report ToModel()
        {
            return _report;
        }

        public void Save()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage { NextPageType = typeof(ReportPage) });
            return;
            if (Id == 0)
            {
                Insert();
                return;
            }
            using (var context = new Entities())
            {
                context.Reports.Attach(_report);
                context.Entry(_report).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
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
        public void RefreshElements()
        {
            ReportRepository reportRepository = new ReportRepository();
            this.ReportElements = new ObservableCollection<ReportElementVM>(reportRepository.GetReportElements(this));
            this.RenderReportElements(null, null);
            //ReportElements.First().ReportVM = this;
            RaisePropertyChanged("ReportElements");
        }
    }
}
