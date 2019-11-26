using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report.element;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        
        private object _data;
        private Boolean _readOnly;

        public Boolean ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                RaisePropertyChanged("ReadOnly");
            }
        }

        public ReportElementVM ReportElementVM { get; set; }
        public string Text { get; set; }

        public Dictionary<string, Object> Dictionary { get; set; }

        public override Object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                Dictionary = (Dictionary<string, Object>)Data;
                ApplyChanges();
            }
        }

        public TextVM(ReportElementVM element, ReportVM report)
        {
            //Data = element.Data;
            EditElement = new RelayCommand(() => Edit());
            DeleteElement = new RelayCommand(() => Delete());
            //Report = report;

            //ReportId = Report.Id;
            //ReportElementVM = element;
            //Title = element.Title;
            //Content = element.Content;
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
            Text = (string)Dictionary["text"];
        }
    }
}
