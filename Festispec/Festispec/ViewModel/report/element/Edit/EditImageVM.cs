using Festispec.Lib.Enums;
using Festispec.Message;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element.Edit
{
    public class EditImageVM : BaseElementEdit
    {
        public ICommand ChooseImageCommand { get; set; }
        
        public EditImageVM()
        {
            ReportElementVM = new ReportElementVM();
            ReportElementVM.Type = ReportElementType.Image;
            MessengerInstance.Register<ChangeSelectedReportElementMessage>(this, message => 
            {
                ReportElementVM = message.ReportElementVM;
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
