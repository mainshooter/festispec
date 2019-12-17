using Festispec.Domain;
using Festispec.ViewModel.report.element;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.toast;
using System;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;

namespace Festispec.ViewModel.report
{
    public class ReportVM : ViewModelBase
    {
        private Report _report;
        private ObservableCollection<ReportElementVM> _reportElements;

        public int Id
        {
            get
            {
                return _report.Id;
            }
            private set
            {
                _report.Id = value;
            }
        }

        public OrderVM Order { get; set; }

        public string Title
        {
            get
            {
                return _report.Title;
            }
            set
            {
                _report.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Status
        {
            get
            {
                return _report.Status;
            }
            set
            {
                _report.Status = value;
                RaisePropertyChanged("Status");
            }
        }

        public ObservableCollection<ReportElementVM> ReportElements
        {
            get
            {
                return _reportElements;
            }
            set
            {
                _reportElements = value;
                RaisePropertyChanged();
            }
        }

        public ReportVM(Report report, OrderVM order)
        {
            Order = order;
            _report = report;
            ReportElements = new ObservableCollection<ReportElementVM>(_report.ReportElements.Select(e => new ReportElementVM(e)).ToList());
        }

        public ReportVM()
        {
            _report = new Report();
        }

        public ReportVM(OrderVM OrderVM)
        {
            _report = new Report();
            Order = OrderVM;
            _report.OrderId = OrderVM.ToModel().Id;
            ReportElements = new ObservableCollection<ReportElementVM>();
        }

        public Report ToModel()
        {
            return _report;
        }

        public void MoveElement(ReportElementVM element, int direction)
        {
            try
            {
                var nextElement = ReportElements[ReportElements.IndexOf(ReportElements.Where(e => e.Id == element.Id).FirstOrDefault()) + direction];
                var nextElementOrder = nextElement.Order;
                nextElement.Order = element.Order;
                element.Order = nextElementOrder;
                SaveElementOrder(nextElement, element);
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
