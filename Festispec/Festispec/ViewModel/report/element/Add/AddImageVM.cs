using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.View.Pages.Report.element.Add;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddImageVM : ViewModelBase
    {
        private ImageVM _reportElementVM;

        public ImageVM ReportElementVM
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
        public ICommand ChooseImageCommand { get; set; }
        
        public AddImageVM()
        {
            ReportElementVM = new ImageVM();
            MessengerInstance.Register<ChangeSelectedReportMessage>(this, message =>
            {
                ReportElementVM.ReportId = message.NextReportVM.Id;
                ReportElementVM.Order = message.NextReportVM.ReportElements.Count + 1;
            });

            MessengerInstance.Register<ChangePageMessage>(this, message => {
                if (message.NextPageType == typeof(AddImagePage))
                {
                    ReportElementVM = new ImageVM();
                }
            });

            SaveElementCommand = new RelayCommand(SaveElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseSaveElement);
            ChooseImageCommand = new RelayCommand(ChooseImage);
        }

        public void SaveElement()
        {
            using (var context = new Entities())
            {
                context.ReportElements.Add(ReportElementVM.ToModel());
                context.SaveChanges();
            }
            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation("Rapportelement is toegevoegd.");
            CloseSaveElement();
        }

        private void ChooseImage()
        {
            var fd = new OpenFileDialog { Filter = "All Image Files | *.*", Multiselect = false };
            if (fd.ShowDialog() != true) return;
            using (var fs = new FileStream(fd.FileName, FileMode.Open, FileAccess.Read))
            {
                var imageResult = new byte[fs.Length];
                fs.Read(imageResult, 0, Convert.ToInt32(fs.Length));
                ReportElementVM.Image = imageResult;
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
    }
}
