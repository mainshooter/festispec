using Festispec.Domain;
using Festispec.Factory;
using Festispec.Message;
using Festispec.Repository;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.Customer.order;
using Festispec.ViewModel.report.element;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class ReportInfoVM:ViewModelBase
    {
        private ReportElementFactory _reportElementFactory;
        private ReportVM _reportVM;
        private OrderVM _orderVM;
        private ReportRepository _reportRepository;
        public ReportVM ReportVM {
            get {
                return _reportVM;
            }
            set {
                _reportVM = value;
                RaisePropertyChanged();
            }
        }
        public OrderVM OrderVM {
            get {
                return _orderVM;
            }
            set {
                _orderVM = value;
                RaisePropertyChanged();
            }
        }
        public ICommand AddElementCommand { get; set; }

        public ObservableCollection<string> Statuses { get; set; }
        public ObservableCollection<UserControl> ReportElementUserControlls { get; set; }


        public ReportInfoVM()
        {
            GetStatuses();
            _reportElementFactory = new ReportElementFactory();
            _reportRepository = new ReportRepository();
            ReportElementUserControlls = new ObservableCollection<UserControl>();

            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                ReportVM = message.NextReportVM;
                ReportVM.ReportElements.CollectionChanged += RenderReportElements;
                RenderReportElements(null, null);
            });
            MessengerInstance.Register<ChangePageMessage>(this, message =>
            {
                if (message.NextPageType == typeof(ReportPage) && ReportVM != null)
                {
                    ReportVM.ReportElements = _reportRepository.GetReportElements(ReportVM);
                    RenderReportElements(null, null);
                }
            });
            AddElementCommand = new RelayCommand(GoToAddElementPage); 
        }

        private void GetStatuses()
        {
            using (var context = new Entities())
            {
                Statuses = new ObservableCollection<string>(context.ReportStatus.ToList().Select(status => status.Status));
            }
        }

        private void GoToAddElementPage()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddElementPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = ReportVM
            });
        }



        public void RenderReportElements(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ReportVM == null)
            {
                return;
            }
            ReportElementUserControlls.Clear();
            var reportElements = ReportVM.ReportElements.OrderBy(el => el.Order);
            foreach (var element in reportElements)
            {
                ReportElementUserControlls.Add(_reportElementFactory.CreateElement(element, ReportVM));
            }
        }
        public void RefreshElements()
        {
            ReportRepository reportRepository = new ReportRepository();
            ReportVM.ReportElements = new ObservableCollection<ReportElementVM>(reportRepository.GetReportElements(ReportVM));
            ReportVM.ReportElements.CollectionChanged += RenderReportElements;
            this.RenderReportElements(null, null);
            RaisePropertyChanged("ReportElements");
        }


    }
}
