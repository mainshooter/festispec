﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element.Add;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddPieChartVM : ViewModelBase
    {
        private PieChartVM _reportElementVM;
        public PieChartVM ReportElementVM
        {
            get
            {
                return _reportElementVM;
            }
            set
            {
                _reportElementVM = value;
                RaisePropertyChanged("ReportElementVM");
            }
        }

        public ICommand SaveElementCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public AddPieChartVM()
        {
            ReportElementVM = new PieChartVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message => {
                if (message.NextPageType == typeof(AddPieChartPage))
                {
                    ReportElementVM = new PieChartVM();
                }
            });

            SaveElementCommand = new RelayCommand(SaveElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseAddElement);
        }

        public void SaveElement()
        {
            using (var context = new Entities())
            {
                context.ReportElements.Add(ReportElementVM.ToModel());
                context.SaveChanges();
            }
            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation("Rapportelement is toegevoegd.");
            CloseAddElement();
        }

        public void CloseAddElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }
    }
}