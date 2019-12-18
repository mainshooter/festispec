using Festispec.Message;
using Festispec.View.Pages.Report.element.Add;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Add
{
    public class AddImageVM : BaseElementAdd
    {
        public ICommand ChooseImageCommand { get; set; }
        
        public AddImageVM(): base()
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
            ChooseImageCommand = new RelayCommand(ChooseImage);
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
    }
}
