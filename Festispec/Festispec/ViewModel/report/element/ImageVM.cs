using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    class ImageVM : ReportElementVM
    {
        private object _data;

        public byte[] Photo { get; set; }

        public override Object Data
        {
            get {
                return _data;
            }
            set {
                _data = value;
                Dictionary = (Dictionary<string, Object>)Data;
                ApplyChanges();
            }
        }

        public Dictionary<string, Object> Dictionary { get; set; }

        public ImageVM(ReportElementVM element)
        {
            Data = element.Data;
            Title = element.Title;
            Content = element.Content;
        }

        private void ApplyChanges()
        {
            Photo = (byte[])Dictionary["image"];
        }
    }
}
