using Festispec.Domain;
using Festispec.Message;
using Festispec.View.Pages.Report;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    class TextVM : ReportElementVM
    {
        private object _data;
        private Boolean _readOnly;
        private int _elementId;

        public Boolean ReadOnly
        {
            get
            {
                return _readOnly;
            }
            set
            {
                _readOnly = value;
                RaisePropertyChanged("ReadOnly");
            }
        }
        public string ShowEditTitle { get; set; }

        public string ShowTitle { get; set; }

        public string ShowEditContent { get; set; }

        public string ShowContent { get; set; }

        public string Text { get; set; }

        public Dictionary<string, Object> Dictionary { get; set; }

        public ICommand EditElement { get; set; }

        public ICommand DeleteElement { get; set; }

        public override Object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                Dictionary = (Dictionary<string, Object>)Data;
                ApplyChanges();
            }
        }

        public TextVM(ReportElementVM element)
        {
            //Data = element.Data;
            _elementId = element.Id;
            Title = element.Title;
            Content = element.Content;
            ReadOnly = true;
            ShowEditTitle = "Hidden";
            ShowTitle = "Visable";
            ShowEditContent = "Hidden";
            ShowContent = "Visable";
            EditElement = new RelayCommand(() => Edit());
            DeleteElement = new RelayCommand(() => Delete());
        }
        private void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze element wilt verwijderen?", "Element Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Remove(context.ReportElements.Where(reportElement => reportElement.Id == _elementId).First());
                    context.SaveChanges();
                }
            }
        }

        public void Edit()
        {
            if (ReadOnly == true)
            {
                ShowEditTitle = "Visable";
                ShowTitle = "Hidden";
                ShowEditContent = "Visable";
                ShowContent = "Hidden";
                RaisePropertyChanged("ShowEditTitle");
                RaisePropertyChanged("ShowTitle");
                RaisePropertyChanged("ShowEditContent");
                RaisePropertyChanged("ShowContent");

                ReadOnly = false;
            }
            else
            {

                ShowEditTitle = "Hidden";
                ShowTitle = "Visable";
                ShowEditContent = "Hidden";
                ShowContent = "Visable";
                RaisePropertyChanged("ShowEditTitle");
                RaisePropertyChanged("ShowTitle");
                RaisePropertyChanged("ShowEditContent");
                RaisePropertyChanged("ShowContent");

                ReadOnly = true;

                using (var context = new Entities())
                { 
                    var element = context.ReportElements.SingleOrDefault(r => r.Id == _elementId);
                    element.Title = Title;
                    element.Content = Content;
                    context.SaveChanges();
                }
            }
        }

        private void ApplyChanges()
        {
            Text = (string)Dictionary["text"];
        }
    }
}
