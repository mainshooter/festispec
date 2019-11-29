using Festispec.Domain;
using Festispec.Factory;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report
{
    public class AddElementVM : ViewModelBase
    {
        private ReportElementFactory _reportElementFactory;
        private string _axes;
        private int _selectedElementIndex;

        public List<string> ElementTypes { get; set; }

        public ReportElementVM ReportElement { get; set; }

        public string Axes
        {
            get
            {
                return _axes;
            }
            set
            {
                _axes = value;
                RaisePropertyChanged("Axes");
            }
        }

        public ReportVM Report { get; set; }

        public int SelectedElementIndex
        {
            get
            {
                return _selectedElementIndex;
            }
            set
            {
                _selectedElementIndex = value;
                RaisePropertyChanged("SelectedElementIndex");
                ChangeInput();
            }
        }

        public ICommand GoBackCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public AddElementVM()
        {
            Axes = "Hidden";
            ReportElement = new ReportElementVM();
            _reportElementFactory = new ReportElementFactory();
            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport);
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                Report = message.NextReportVM;
                ReportElement.ReportId = Report.Id;
                ReportElement.Order = Report.ReportElements.Count + 1;
            });
        }
        public void ChangeInput()
        {
            switch (ElementTypes[SelectedElementIndex])
            {
                case "image":
                    var fd = new OpenFileDialog { Filter = "All Image Files | *.*", Multiselect = false };
                    if (fd.ShowDialog() != true) return;
                    using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        var test = new byte[fs.Length];
                        fs.Read(test, 0, Convert.ToInt32(fs.Length));
                    }
                    Axes = "Hidden";
                    break;
                case "barchart":
                    Axes = "Visable";
                    break;
                case "linechart":
                    Axes = "Visable";
                    break;
                default:
                    Axes = "Hidden";
                    break;
            }
        }
        private void AddElementToReport()
        {
            ReportElement.Type = ElementTypes[SelectedElementIndex];

            using (var context = new Entities())
            {
                _reportElementFactory.CreateElement(ReportElement, Report);
                context.ReportElements.Add(ReportElement.ToModel());
                context.SaveChanges();
            }
            var userControl = _reportElementFactory.CreateElement(ReportElement, Report);
            Report.ReportElementUserControlls.Add(userControl);
            GoBackToReport();
        }

        private void GoBackToReport()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }
    }
}
