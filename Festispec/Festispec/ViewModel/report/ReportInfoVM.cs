using Festispec.Domain;
using Festispec.Factory;
using Festispec.Message;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.Customer.order;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class ReportInfoVM:ViewModelBase
    {
        private ReportElementFactory _reportElementFactory;
        public ReportVM ReportVM { get; set; }
        public OrderVM OrderVM { get; set; }
        public ICommand AddElementCommand { get; set; }

        public ObservableCollection<string> Statuses { get; set; }
        public ObservableCollection<UserControl> ReportElementUserControlls { get; set; }




        public ReportInfoVM()
        {
            _reportElementFactory = new ReportElementFactory();
            ReportElementUserControlls = new ObservableCollection<UserControl>();

            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message => {
                ReportVM = message.NextReportVM;
            });
            MessengerInstance.Register<ChangeSelectedOrderVM>(this, message => {
                OrderVM = message.NextOrderVM;
            });
            GetStatuses();
            AddElementCommand = new RelayCommand(GoToAddElementPage);
            ReportVM.ReportElements.CollectionChanged += RenderReportElements;
            RenderReportElements(null, null);
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

        public bool ValidateInputs()
        {
            if (ReportVM.Title == null || ReportVM.Title.Equals(""))
            {
                MessageBox.Show("Titel mag niet leeg zijn.");
                return false;
            }

            if (ReportVM.Title.Length > 100)
            {
                MessageBox.Show("Titel mag niet langer zijn dan 100 karakters.");
                return false;
            }

            return true;
        }

        public void RenderReportElements(object sender, NotifyCollectionChangedEventArgs e)
        {
            ReportElementUserControlls.Clear();
            var reportElements = ReportVM.ReportElements.OrderBy(el => el.Order);
            foreach (var element in reportElements)
            {
                ReportElementUserControlls.Add(_reportElementFactory.CreateElement(element, ReportVM));
            }
        }
    }
}
