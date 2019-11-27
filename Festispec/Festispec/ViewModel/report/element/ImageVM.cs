using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    class ImageVM : ReportElementVM
    {
        private object _data;

        public byte[] Photo { get; set; }

        public Dictionary<string, Object> Dictionary { get; set; }

        public ImageVM(ReportElementVM element)
        {
            Data = element.Data;
            Title = element.Title;
            Content = element.Content;
            EditElementCommand = new RelayCommand(GoToEdit);
        }

        private void ApplyChanges()
        {
            Photo = (byte[])Dictionary["image"];
        }
    }
}
