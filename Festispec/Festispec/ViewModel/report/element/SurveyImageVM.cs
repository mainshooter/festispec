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
        public ObservableCollection<byte[]> Images { get; set; }

        public SurveyImageVM()
        {
            Type = ReportElementType.SurveyImages;
        }

        public SurveyImageVM(ReportElementVM element)
        {
            Images = new ObservableCollection<byte[]>();
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
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditSurveyImagesPage) });
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
                    string base64 = item[0];
                    base64 = base64.Split(',')[1];

                    byte[] bytes = System.Convert.FromBase64String(base64);
                    Images.Add(bytes);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
