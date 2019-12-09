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
        private ReportElementFactory _reportElementFactory;

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
            }
        }

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

        private void GoToAddElementPage()
        {
            Page addElementPage = new AddElementPage();
            AddElementVM addElementVM = new AddElementVM();
            addElementVM.Report = this;
            addElementPage.DataContext = addElementVM;
            MainViewModel.Page = addElementPage;
        }

        public Report ToModel()
        {
            return _report;
        }

        public void Save()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage { NextPageType = typeof(ReportPage) });
            return;
            if (Id == 0)
            {
                Insert();
                return;
            }
            using (var context = new Entities())
            {
                context.Reports.Attach(_report);
                context.Entry(_report).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
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
                ReportElementUserControlls.Add(_reportElementFactory.CreateElement(element));
            }
        }

        public void ExportToPDF(StackPanel document)
        {
            /// casting the UIElementCollection into List
            List<UserControl> lstElement = ReportElementUserControlls.ToList();

            /// Geting all Control from list


            foreach (var control in lstElement)
            {

                //var buttons = .OfType<Button>().FirstOrDefault();
                //control = System.Windows.Visibility.Hidden;

                ///// Geting all Control from list

                //foreach (var item in buttons)
                //{
                //    item.Visibility = System.Windows.Visibility.Hidden;
                //}
                ///Hide all Controls

            }


            //foreach (var item in ReportElementUserControlls)
            //{
            //    fd.Blocks.Add(new BlockUIContainer(item));
            //}
            ConvertToPNG.SnapShotPng(document, 1);
            //IDocumentPaginatorSource idocument = fd as IDocumentPaginatorSource;
            //printDialog.PrintDocument(idocument.DocumentPaginator, "Rapport");

            //printDialog.PrintVisual(document, "Rapport");
        }


    }
}
