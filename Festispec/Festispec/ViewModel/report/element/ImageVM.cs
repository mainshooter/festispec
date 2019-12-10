﻿using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report.element;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace Festispec.ViewModel.report.element
{
    public class ImageVM : ReportElementVM
    {
        private object _data;

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

        private void ApplyChanges()
        {
        }
    }
}
