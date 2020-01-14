using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.report.element
{
    public class SurveyImageVM : ReportElementVM
    {
        public ObservableCollection<BitmapImage> Images { get; set; }

        public SurveyImageVM()
        {
            Type = ReportElementType.SurveyImages;
        }

        public SurveyImageVM(ReportElementVM element)
        {
            Images = new ObservableCollection<BitmapImage>();
            EditElement = new RelayCommand(() => Edit());
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            ReportId = element.ReportId;
            Image = element.Image;
            DataParser = element.DataParser;
            if (DataParser != null)
            {
                Data = DataParser.ParseData();
            }
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditImagePage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this
            });
        }

        public void ApplyChanges()
        {
            try
            {
                Images.Clear();
                foreach (var item in Data)
                {
                    var bytes = Convert.FromBase64String(item[0]);
                    var image = new BitmapImage();
                    using (var mem = new MemoryStream(bytes))
                    {
                        mem.Position = 0;
                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = null;
                        image.StreamSource = mem;
                        image.EndInit();
                    }
                    image.Freeze();
                    Images.Add(image);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
