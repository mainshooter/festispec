using Festispec.Domain;
using Festispec.Factory;
using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.Repository;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element.Add;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class ReportInfoVM:ViewModelBase
    {
        private ReportVM _reportVM;
        private ReportRepository _reportRepository;

        public ReportVM ReportVM {
            get 
            {
                return _reportVM;
            }
            set 
            {
                _reportVM = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddElementCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ICommand SaveReportCommand { get; set; }

        public ObservableCollection<string> Statuses { get; set; }

        public ObservableCollection<UserControl> ReportElementUserControlls { get; set; }

        public string SelectedElementType { get; set; }

        public List<string> ElementTypes { get; set; }


        public ReportInfoVM()
        {
            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            ElementTypes = elementTypesList.ReportElementTypes;

            GetStatuses();
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
            SaveReportCommand = new RelayCommand(SaveReport);

            GoBackCommand = new RelayCommand(() => {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage)});
            });

        }

        private void SaveReport()
        {
            using (var context = new Entities())
            {
                context.Reports.Attach(ReportVM.ToModel());
                context.Entry(ReportVM.ToModel()).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
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
            switch (SelectedElementType)
            {
                case ReportElementType.Table:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddTablePage) });
                    break;
                case ReportElementType.Linechart:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddLineChartPage) });
                    break;
                case ReportElementType.Piechart:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddPieChartPage) });
                    break;
                case ReportElementType.Barchart:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddBarChartPage) });
                    break;
                case ReportElementType.Image:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddImagePage) });
                    break;
                case ReportElementType.Text:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddTextPage) });
                    break;
            }
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
                ReportElementUserControlls.Add(ReportElementFactory.CreateElement(element));
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
