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
using Festispec.ViewModel.Customer.order;
using GalaSoft.MvvmLight.Ioc;
using Festispec.ViewModel.toast;
using System;
using System.Windows;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report
{
    public class ReportVM : ViewModelBase
    {
        private Report _report;
        

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
                SaveReportChangesAsync();
                RaisePropertyChanged("Title");
            }
        }

        public string Status {
            get {
                return _report.Status;
            }
            set {
                _report.Status = value;
                SaveReportChangesAsync();
                RaisePropertyChanged("Status");
            }
        }


        public ObservableCollection<ReportElementVM> ReportElements { get; set; }

        public ReportVM()
        {
            var reportRepository = new ReportRepository();
            this.ReportElements = new ObservableCollection<ReportElementVM>(reportRepository.GetReportElements(this));
        }

        public ReportVM(Report report)
        {
            _report = report;
            ReportElements = new ObservableCollection<ReportElementVM>(_report.ReportElements.Select(e => new ReportElementVM(e, this)).ToList());         
        }


        public Report ToModel()
        {
            return _report;
        }
     
        public async Task SaveReportChangesAsync()
        {
            using (var context = new Entities())
            {
                context.Reports.Attach(_report);
                context.Entry(_report).State = System.Data.Entity.EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }



        public void MoveElement(ReportElementVM element, int direction)
        {
            try
            {
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
            RaisePropertyChanged("ReportElements");
        }
    }
}
