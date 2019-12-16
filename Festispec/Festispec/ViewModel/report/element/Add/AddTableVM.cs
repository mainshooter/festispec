﻿using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element.Add;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddTableVM : BaseElementAdd
    {
        public ICommand SaveElementCommand { get; set; }
        public ICommand ReturnCommand { get; set; }

        public AddTableVM() : base()
        {
            ReportElementVM = new TableVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });
            MessengerInstance.Register<ChangePageMessage>(this, message => {
                if (message.NextPageType == typeof(AddTablePage))
                {
                    ReportElementVM = new TableVM();
                }
            });

            SaveElementCommand = new RelayCommand(SaveElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseAddElement);
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
                CloseAddElement();
            }
            else
            {
                toast.ShowError("Deze query kan niet met dit element en/of vraag word niet ondersteunt door deze query");
            }
        }

        public void CloseAddElement()
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
