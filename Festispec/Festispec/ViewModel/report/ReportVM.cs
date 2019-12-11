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
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Collections.Generic;
using System.Xml;
using System;
using System.IO;
using System.Windows.Threading;
using System.CodeDom.Compiler;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        public ObservableCollection<ReportElementVM> ReportElements {
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

        public ReportVM(Report report)
        {
            _report = report;
            ReportElements = new ObservableCollection<ReportElementVM>(_report.ReportElements.Select(e => new ReportElementVM(e)).ToList());
        public ObservableCollection<ReportElementVM> ReportElements { get; set; }

        public ObservableCollection<UserControl> ReportElementUserControlls { get; set; }

        public MainViewModel MainViewModel { get; set; }

        public ICommand SaveReportCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public ICommand ExportToPDFCommand { get; set; }

        public ReportVM()
        {
            _report = new Report();
            var reportRepository = new ReportRepository();
            this.ReportElements = new ObservableCollection<ReportElementVM>(reportRepository.GetReportElements());
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            _reportElementFactory = new ReportElementFactory();
            ReportElements.CollectionChanged += RenderReportElements;
            SaveReportCommand = new RelayCommand(Save);
            AddElementCommand = new RelayCommand(GoToAddElementPage);
            ExportToPDFCommand = new RelayCommand<StackPanel>(ExportToPDF);
            _report.Title = "Test titel";
            this.RenderReportElements(null, null);
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

        public void ExportToPDF(StackPanel document)
        {
            foreach (var userControl in ReportElementUserControlls)
            {
                ReportElementVM reportElementVM = (ReportElementVM)userControl.DataContext;
                reportElementVM.VisibilityButtons = Visibility.Collapsed;
            }

            PrintDialog printDialog = new PrintDialog();
            var fixedDocument = new FixedDocument();
            fixedDocument.DocumentPaginator.PageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            FixedPage frontPage = new FixedPage();
            Paragraph frontParagraph = new Paragraph();
            frontParagraph.Inlines.Add("Festispec Rapportage - ");
            frontParagraph.Inlines.Add("Klant gegevens - " + Order.Event.Customer.Name);
            frontParagraph.Inlines.Add("Inspectie gegevens - " + Order.Description);
            frontParagraph.Inlines.Add("Datum - " + DateTime.Today);
            frontParagraph.Inlines.Add("Festispec contactpersoon - " + Order.Event.ContactPerson.Fullname);
            PageContent frontPageContent = new PageContent();
            ((IAddChild)frontPageContent).AddChild(frontPage);
            fixedDocument.Pages.Add(frontPageContent);

            var image = ConvertToPNGVM.SnapShotPng(document, 1);

            FixedPage page = new FixedPage();
            page.Height = image.ActualHeight + 50;
            page.Width = image.ActualWidth + 50;
            page.Margin = new Thickness(25);
            page.Children.Add(image);
            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(page);
            fixedDocument.Pages.Add(pageContent);
            printDialog.PrintDocument(fixedDocument.DocumentPaginator, "Rapport");

            foreach (var userControl in ReportElementUserControlls)
            {
                ReportElementVM reportElementVM = (ReportElementVM)userControl.DataContext;
                reportElementVM.VisibilityButtons = Visibility.Visible;
            }
        }
    }
}
