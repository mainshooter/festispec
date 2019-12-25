using GalaSoft.MvvmLight.CommandWpf;
﻿using Festispec.Message;
﻿using Festispec.Lib.Enums;
using Festispec.View.Pages.Report.element.Edit;

namespace Festispec.ViewModel.report.element
{
    public class ImageVM : ReportElementVM
    {
        public ImageVM()
        {
            Type = ReportElementType.Image;
        }

        public ImageVM(ReportElementVM element)
        {
            EditElement = new RelayCommand(() => Edit());
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            ReportId = element.ReportId;
            Image = element.Image;
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditImagePage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this
            });
        }
    }
}
