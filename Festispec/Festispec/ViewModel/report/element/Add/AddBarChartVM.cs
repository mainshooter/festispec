﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element.Add;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddBarChartVM : BaseElementAdd
    {
        private BarChartVM _reportElementVM;

        public BarChartVM ReportElementVM
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

        public AddBarChartVM(): base()
        {
            ReportElementVM = new BarChartVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message => { 
                if (message.NextPageType == typeof(AddBarChartPage))
                {
                    ReportElementVM = new BarChartVM();
                }
            });

            SaveElementCommand = new RelayCommand(SaveElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseSaveElement);
        }

        public void SaveElement()
        {
            ToastVM toast = CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>();
            ReportElementVM.DataParser.Question = ReportElementVM.SelectedSurveyQuestion;
            if (CanUseOptions())
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Add(ReportElementVM.ToModel());
                    context.SaveChanges();
                }
                toast.ShowInformation("Rapportelement is toegevoegd.");
                CloseSaveElement();
            }
            else
            {
                toast.ShowError("Deze query kan niet met dit element en/of vraag word niet ondersteunt door deze query");
            }
        }

        public void CloseSaveElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }

        private bool CanUseOptions()
        {
            if (ReportElementVM.DataParser != null && ReportElementVM.DataParser.QuestionTypeIsSupported)
            {
                var dataParser = ReportElementVM.DataParser;
                bool containsSupportedType = dataParser.SupportedVisuals.Contains(ReportElementVM.Type);
                if (containsSupportedType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}