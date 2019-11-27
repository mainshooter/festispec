using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using System;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel.report.element
{
    public class PieChartVM: ReportElementVM
    {
        private Object _data;

        public ReportElementVM ReportElementVM { get; set; }
        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
                ApplyChanges();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }

        public PieChartVM(ReportElementVM element, ReportVM report)
        {
            EditElement = new RelayCommand(() => Edit());
            //Data = element.Data;
            Report = report;
            ReportElementVM = element;
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;

        }
        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditTextPage) });
            MessengerInstance.Send<ChangeSelectedReportMessage>(new ChangeSelectedReportMessage()
            {
                NextReportVM = Report,
                ReportElement = ReportElementVM,
            });
        }
        //public void Delete()
        //{
        //    MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze element wilt verwijderen?", "Element Verwijderen", MessageBoxButton.YesNo);
        //    if (result.Equals(MessageBoxResult.Yes))
        //    {
        //        using (var context = new Entities())
        //        {
        //            context.ReportElements.Remove(context.ReportElements.Where(reportElement => reportElement.Id == ReportElementVM.Id).First());
        //            context.SaveChanges();
        //        }
        //        CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Rapportelement verwijderd.");
        //    }
        //    Report.RefreshElements();
        //}

        private void ApplyChanges()
        {
            SeriesCollection = (SeriesCollection)Data;
        }
    }
}
