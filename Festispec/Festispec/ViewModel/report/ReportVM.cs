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
using GalaSoft.MvvmLight.Ioc;
using Festispec.ViewModel.toast;
using System.Data.Entity;
using System;

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


        [PreferredConstructor]
        public ReportVM()
        {
            _report = new Report();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                _report = message.NextReportVM._report;
                Title = message.NextReportVM.Title;
                Id = message.NextReportVM.Id;
                RefreshElements();
            });

            var reportRepository = new ReportRepository();
            this.ReportElements = new ObservableCollection<ReportElementVM>(reportRepository.GetReportElements(this));
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            _reportElementFactory = new ReportElementFactory();
            ReportElements.CollectionChanged += RenderReportElements;
            SaveReportCommand = new RelayCommand(Save);
            AddElementCommand = new RelayCommand(GoToAddElementPage);
            this.RenderReportElements(null, null);
        }

        public ReportVM(Report report)
        {
            _report = report;
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

            using (var context = new Entities())
            {
                context.Reports.Attach(_report);
                context.Entry(_report).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation("Rapport bijgewerkt.");
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
                ReportElementUserControlls.Add(_reportElementFactory.CreateElement(element, this));
            }
        }
        public void MoveElement(ReportElementVM element, int direction)
        {
            try
            {
                Console.WriteLine(ReportElements.Count);
                Console.WriteLine(ReportElements.IndexOf(ReportElements.Where(e => e.Id == element.Id).FirstOrDefault()));
                var NextElement = ReportElements[ReportElements.IndexOf(ReportElements.Where(e => e.Id == element.Id).FirstOrDefault()) + direction];
                var NextElementOrder = NextElement.Order;
                NextElement.Order = element.Order;
                element.Order = NextElementOrder;
                SaveElementOrder(NextElement, element);
                RefreshElements();
            }
            catch (Exception)
            {
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowError("Dit element kan niet verder.");
            }
        }

        public void SaveElementOrder(ReportElementVM element1, ReportElementVM element2)
        {
            using (var context = new Entities())
            {
                context.ReportElements.Attach(element1.ToModel());
                context.Entry(element1.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                context.ReportElements.Attach(element2.ToModel());
                context.Entry(element2.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
