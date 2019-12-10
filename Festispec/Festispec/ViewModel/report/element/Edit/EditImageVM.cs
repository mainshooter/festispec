using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.Data.Entity;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class EditImageVM : ViewModelBase
    {
        private ReportElementVM _reportElementVM;

        public ReportElementVM ReportElementVM
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
        
        public EditImageVM()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = "image";
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => {
                ReportElementVM = message.ReportElementVM;
            });

            SaveElementCommand = new RelayCommand(EditElement, CanAddElement);
            ReturnCommand = new RelayCommand(CloseEditElement);
            ChooseImageCommand = new RelayCommand(ChooseImage);
        }

        public void EditElement()
        {
            using (var context = new Entities())
            {
                context.ReportElements.Attach(this.ReportElementVM.ToModel());
                context.Entry(ReportElementVM.ToModel()).State = EntityState.Modified;
                context.SaveChanges();
            }
            CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowInformation("Rapportelement bijgewerkt.");
            CloseEditElement();
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

        public void CloseEditElement()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(ReportPage) });
        }

        public bool CanAddElement()
        {
            return ReportElementVM.IsValid;
        }
    }
}
