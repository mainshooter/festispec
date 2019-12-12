using Festispec.Domain;
using Festispec.Factory;
using Festispec.Message;
using Festispec.Repository;
using Festispec.View.Pages.Customer.Event;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element.Add;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.report
{
    public class ReportInfoVM : ViewModelBase
    {
        private ReportVM _reportVM;
        private ReportRepository _reportRepository;

        public ReportVM ReportVM
        {
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

        public ICommand ExportToPDFCommand { get; set; }

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
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
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
            ExportToPDFCommand = new RelayCommand<StackPanel>(ExportToPDF);

            GoBackCommand = new RelayCommand(() =>
            {
                MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EventPage) });
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
                case "table":
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddTablePage) });
                    break;
                case "linechart":
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddLineChartPage) });
                    break;
                case "piechart":
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddPieChartPage) });
                    break;
                case "barchart":
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddBarChartPage) });
                    break;
                case "image":
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddImagePage) });
                    break;
                case "text":
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
            List<TextBlock> frontpageTextBlocks = new List<TextBlock>();
            FixedPage frontPage = new FixedPage();
            StackPanel horizontalPanel = new StackPanel();
            horizontalPanel.Orientation = Orientation.Horizontal;
            StackPanel frontPanel = new StackPanel();
            var frontPageText1 = new TextBlock();
            frontPageText1.Text = ("Festispec Rapportage - " + ReportVM.Order.Event.BeginDate.ToString("dd-MMM-yyy") + " tot " + ReportVM.Order.Event.EndDate.ToString("dd-MM-yyyy"));
            frontpageTextBlocks.Add(frontPageText1);
            var frontPageText6 = new TextBlock();
            frontPageText6.Text = ("Rapportage - ");
            frontpageTextBlocks.Add(frontPageText6);
            var frontPageText2 = new TextBlock();
            frontPageText2.Text = ("Klant gegevens");
            frontpageTextBlocks.Add(frontPageText2);
            var frontPageText8 = new TextBlock();
            frontPageText8.Text = (ReportVM.Order.Event.Customer.Name);
            frontpageTextBlocks.Add(frontPageText8);
            var frontPageText3 = new TextBlock();
            frontPageText3.Text = ("Inspectie gegevens");
            frontpageTextBlocks.Add(frontPageText3);
            var frontPageText9 = new TextBlock();
            frontPageText9.Text = (ReportVM.Order.Description);
            frontpageTextBlocks.Add(frontPageText9);
            var frontPageText4 = new TextBlock();
            frontPageText4.Text = ("Datum");
            frontpageTextBlocks.Add(frontPageText4);
            var frontPageText7 = new TextBlock();
            frontPageText7.Text = DateTime.Today.ToString("dd-MM-yyyy");
            frontpageTextBlocks.Add(frontPageText7);
            var frontPageText5 = new TextBlock();
            frontPageText5.Text = ("Festispec contactpersoon - " + ReportVM.Order.Event.ContactPerson.Fullname);
            frontpageTextBlocks.Add(frontPageText5);


            foreach (var text in frontpageTextBlocks)
            {
                text.FontSize = 18;
                text.Margin = new Thickness(10);
                text.FontWeight = FontWeights.SemiBold;
                frontPanel.Children.Add(text);
            }

            frontPageText7.Margin = new Thickness(10, 0, 10, 10);
            frontPageText7.FontWeight = FontWeights.Normal;
            frontPageText8.Margin = new Thickness(10, 0, 10, 10);
            frontPageText8.FontWeight = FontWeights.Normal;
            frontPageText9.Margin = new Thickness(10, 0, 10, 10);
            frontPageText9.FontWeight = FontWeights.Normal;
            frontPageText6.Margin = new Thickness(10, 10, 10, 30);

            horizontalPanel.Children.Add(frontPanel);
            Image logo = new Image();
            logo.Source = ConvertToPNGVM.ConvertImage(Lib.Images.FestispecLogo);
            logo.Margin = new Thickness(25, 100, 25, 0);

            horizontalPanel.Children.Add(logo);
            horizontalPanel.Margin = new Thickness(50, 50, 0, 0);

            frontPage.Children.Add(horizontalPanel);

            PageContent frontPageContent = new PageContent();
            ((IAddChild)frontPageContent).AddChild(frontPage);
            fixedDocument.Pages.Add(frontPageContent);

            var image = ConvertToPNGVM.SnapShotPng(document, 1);

            FixedPage page = new FixedPage();
            StackPanel stackPanelSecondPage = new StackPanel();

            var header = new TextBlock();
            header.Text = ("Festispec Rapportage - " + ReportVM.Order.Event.BeginDate.ToString("dd-MM-yyyy") + " tot " + ReportVM.Order.Event.EndDate.ToString("dd-MM-yyyy"));
            header.FontSize = 18;
            header.Margin = new Thickness(60, 60, 10, 10);
            header.FontWeight = FontWeights.SemiBold;
            image.Margin = new Thickness(25);
            stackPanelSecondPage.Children.Add(header);
            stackPanelSecondPage.Children.Add(image);
            page.Height = document.ActualHeight + 100;
            page.Width = document.ActualWidth;
            page.Children.Add(stackPanelSecondPage);
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
