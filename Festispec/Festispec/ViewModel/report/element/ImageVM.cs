using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Festispec.ViewModel.rapport.element;

namespace Festispec.ViewModel.report.element
{
    class ImageVM : ReportElementVM
    {
        private object _data;


        public string Photo { get; set; }


        public override Object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
        public ImageVM(ReportElementVM element)
        {
            Photo = element.Content;
            Title = element.Title;
            Data = element.Data;

        }
    }
}
