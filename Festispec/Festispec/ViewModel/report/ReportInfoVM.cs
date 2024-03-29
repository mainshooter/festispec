﻿using Festispec.Domain;
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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Festispec.ViewModel.toast;
using Festispec.Lib.ConvertToImage;
using System.Windows.Xps.Packaging;
using System.IO;

namespace Festispec.ViewModel.report
{
    public class ReportInfoVM : ViewModelBase
    {
        private ReportVM _reportVM;
        private ReportRepository _reportRepository;
        private ToastVM _toast;

        public ICommand AddElementCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ICommand SaveReportCommand { get; set; }

        public ICommand ExportToPDFCommand { get; set; }

        public ObservableCollection<string> Statuses { get; set; }

        public ObservableCollection<UserControl> ReportElementUserControlls { get; set; }

        public string SelectedElementType { get; set; }

        public List<string> ElementTypes { get; set; }

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


        public ReportInfoVM()
        {
            _toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
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
                case ReportElementType.Draw:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddDrawPage) });
                    break;
                case ReportElementType.SurveyImages:
                    MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(AddSurveyImagesPage) });
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
            var fixedDocument = new System.Windows.Documents.FixedDocument();
            List<TextBlock> frontpageTextBlocks = new List<TextBlock>();
            System.Windows.Documents.FixedPage frontPage = new System.Windows.Documents.FixedPage();
            StackPanel horizontalPanel = new StackPanel();
            horizontalPanel.Orientation = Orientation.Horizontal;
            StackPanel frontPanel = new StackPanel();
            var frontPageText1 = new TextBlock();
            frontPageText1.Text = ("Festispec Rapportage - " + ReportVM.Order.Event.BeginDate.ToString("dd-MM-yyyy") + " tot " + ReportVM.Order.Event.EndDate.ToString("dd-MM-yyyy"));
            frontpageTextBlocks.Add(frontPageText1);
            var frontPageText6 = new TextBlock();
            frontPageText6.MaxWidth = 410;
            frontPageText6.TextWrapping = TextWrapping.Wrap;
            frontPageText6.Text = ("Rapportage - " + ReportVM.Title);
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
            frontPageText9.Text = ("Bezoekers aantal: " + ReportVM.Order.Event.AmountVisitors + ", Oppervlakte: " + ReportVM.Order.Event.SurfaceM2 + " m2");
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
            logo.Source = ConvertToImage.ConvertImage(Lib.Images.FestispecLogo);
            logo.Margin = new Thickness(25, 100, 25, 0);

            horizontalPanel.Children.Add(logo);
            horizontalPanel.Margin = new Thickness(50, 50, 0, 0);

            frontPage.Children.Add(horizontalPanel);

            System.Windows.Documents.PageContent frontPageContent = new System.Windows.Documents.PageContent();
            ((IAddChild)frontPageContent).AddChild(frontPage);
            fixedDocument.Pages.Add(frontPageContent);
            if (ReportElementUserControlls.Count < 1)
            {
                var label = (Label)document.Children[0];
                label.Height = 288;
                label.UpdateLayout();
            }
            else
            {
                var label = (Label)document.Children[0];
                label.Height = 45;
                label.UpdateLayout();
            }
            var test = document.Children;
            var image = ConvertToImage.SnapShotPng(document, 1);

            System.Windows.Documents.FixedPage page = new System.Windows.Documents.FixedPage();
            StackPanel stackPanelSecondPage = new StackPanel();

            var header = new TextBlock();
            header.Text = ("Festispec Rapportage - " + ReportVM.Order.Event.BeginDate.ToString("dd-MM-yyyy") + " tot " + ReportVM.Order.Event.EndDate.ToString("dd-MM-yyyy"));
            header.FontSize = 18;
            header.Margin = new Thickness(60, 60, 10, 10);
            header.FontWeight = FontWeights.SemiBold;
            image.Margin = new Thickness(25);
            stackPanelSecondPage.Children.Add(header);
            stackPanelSecondPage.Children.Add(image);
            stackPanelSecondPage.Margin = new Thickness((816 - document.ActualWidth) / 2, 0, 0, 0);
            page.Height = document.ActualHeight + 100;
            page.Width = 816;
            page.Children.Add(stackPanelSecondPage);
            System.Windows.Documents.PageContent pageContent = new System.Windows.Documents.PageContent();
            ((IAddChild)pageContent).AddChild(page);
            fixedDocument.Pages.Add(pageContent);

            try
            {
                SaveCurrentViewToXPS(fixedDocument);
            }
            catch
            {
                _toast.ShowError("Sluit eerst het bestand dat u probeerd te vervangen.");
            }

            foreach (var userControl in ReportElementUserControlls)
            {
                ReportElementVM reportElementVM = (ReportElementVM)userControl.DataContext;
                reportElementVM.VisibilityButtons = Visibility.Visible;
            }
        }

        public void SaveCurrentViewToXPS(System.Windows.Documents.FixedDocument document)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = String.Format("Rapport{0:ddMMyyyyHHmmss}", DateTime.Now);
            dlg.DefaultExt = ".pdf";
            dlg.Filter = "PDF Documents (.pdf)|*.pdf";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                string tempFilename = "temp.xps";
                File.Delete(tempFilename);
                XpsDocument xpsd = new XpsDocument(tempFilename, FileAccess.ReadWrite);
                System.Windows.Xps.XpsDocumentWriter xw = XpsDocument.CreateXpsDocumentWriter(xpsd);
                xw.Write(document);
                xpsd.Close();
                PdfSharp.Xps.XpsConverter.Convert(tempFilename, filename, 1);
            }
        }
    }
}
