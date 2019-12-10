using Festispec.Domain;
using Festispec.ViewModel.report.element;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Festispec.Repository;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.toast;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Festispec.ViewModel.report
{
    public class ReportVM : ViewModelBase
    {
        private Report _report;
        private ObservableCollection<ReportElementVM> _reportElements;
        

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


        public ObservableCollection<ReportElementVM> ReportElements {
            get {
                return _reportElements;
            }
            set {
                _reportElements = value;
                RaisePropertyChanged();
            }
        }

        public ReportVM()
        {
            ReportElements = new ObservableCollection<ReportElementVM>();
        }

        public ReportVM(Report report)
        {
            _report = report;
            ReportElements = new ObservableCollection<ReportElementVM>(_report.ReportElements.Select(e => new ReportElementVM(e)).ToList());         
        }

        public ReportVM(OrderVM OrderVM)
        {
            _report = new Report();
            _report.Order = OrderVM.ToModel();
            ReportElements = new ObservableCollection<ReportElementVM>();
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
                context.SaveChangesAsync();
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
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ReportInfoVM>().RefreshElements();
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

                context.Entry(element1.ToModel()).State = EntityState.Modified;

                context.SaveChanges();
            }
            using (var context = new Entities())
            {
                context.ReportElements.Attach(element2.ToModel());

                context.Entry(element2.ToModel()).State = EntityState.Modified;

                context.SaveChanges();
            }
        }


        public bool ValidateInputs()
        {
            if (Title == null || Title.Equals(""))
            {
                MessageBox.Show("Titel mag niet leeg zijn.");
                return false;
            }

            if (Title.Length > 100)
            {
                MessageBox.Show("Titel mag niet langer zijn dan 100 karakters.");
                return false;
            }

            return true;
        }
    }
}
