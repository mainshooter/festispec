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
        private string _imageButton;
        private string _imageText;

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
        public string ImageButton
        {
            get
            {
                return _imageButton;
            }
            set
            {
                _imageButton = value;
                RaisePropertyChanged("ImageButton");
            }
        }
        public string ImageText
        {
            get
            {
                return _imageText;
            }
            set
            {
                _imageText = value;
                RaisePropertyChanged("ImageText");
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
        public ICommand ChooseImageCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        public ICommand AddElementCommand { get; set; }

        public AddElementVM()
        {
            Axes = "Hidden";
            ImageButton = "Hidden";
            ReportElement = new ReportElementVM();
            _reportElementFactory = new ReportElementFactory();
            ReportElementTypesListVM elementTypesList = new ReportElementTypesListVM();
            ElementTypes = elementTypesList.ReportElementTypes;
            GoBackCommand = new RelayCommand(GoBackToReport);
            AddElementCommand = new RelayCommand(AddElementToReport, CanAddElement);
            ChooseImageCommand = new RelayCommand(ChooseImage);

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
                    ImageButton = "Visable";
                    Axes = "Hidden";
                    break;
                case "barchart":
                    ImageButton = "Hidden";
                    Axes = "Visable";
                    break;
                case "linechart":
                    ImageButton = "Hidden";
                    Axes = "Visable";
                    break;
                default:
                    ImageButton = "Hidden";
                    Axes = "Hidden";
                    break;
            }
        }

        public void ChooseImage()
        {
            var fd = new OpenFileDialog { Filter = "All Image Files | *.*", Multiselect = false };

            if (fd.ShowDialog() != true) return;
            using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
            {
                ImageText = fd.FileName;
                var test = new byte[fs.Length];
                fs.Read(test, 0, Convert.ToInt32(fs.Length));
            }
        }

        private void AddElementToReport()
        {

            using (var context = new Entities())
            {
                _reportElementFactory.CreateElement(ReportElement, Report);
                context.ReportElements.Add(ReportElement.ToModel());
                context.SaveChanges();
            }
            var userControl = _reportElementFactory.CreateElement(ReportElement, Report);
            GoBackToReport();
        }

        private void GoBackToReport()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            ReportElement.Type = ElementTypes[SelectedElementIndex];
            return ReportElement.IsValid;
        }
    }
}
