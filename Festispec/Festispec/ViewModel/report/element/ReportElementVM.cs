﻿using Festispec.Domain;
using Festispec.Factory;
using Festispec.Interface;
using Festispec.Lib.Enums;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class ReportElementVM : ViewModelBase, IDataErrorInfo  
    {
        private ReportElement _reportElement;
        private IDataParser _dataParser;
        private List<List<string>> _data;
        private IQuestion _selectedSurveyQuestion;
        private Visibility _visibilityButtons;

        public ICommand EditElement { get; set; }
        public ICommand DeleteElement { get; set; }
        public ICommand ElementUpCommand { get; set; }
        public ICommand ElementDownCommand { get; set; }

        public ReportVM ReportVM 
        { 
            get 
            {
                return CommonServiceLocator.ServiceLocator.Current.GetInstance<ReportInfoVM>().ReportVM;
            }
        }

        public int Id
        {
            get
            {
                return _reportElement.Id;
            }
            set
            {
                _reportElement.Id = value;
            }
        }

        public int ReportId
        {
            get
            {
                return _reportElement.ReportId;
            }
            set
            {
                _reportElement.ReportId = value;
            }
        }

        public byte[] Image 
        {
            get 
            {
                return _reportElement.Image;
            }
            set 
            {
                _reportElement.Image = value;
                RaisePropertyChanged("Image");
            }
        }

        public string Type
        {
            get
            {
                return _reportElement.ElementType;
            }
            set
            {
                _reportElement.ElementType = value;
            }
        }

        public string Title
        {
            get
            {
                return _reportElement.Title;
            }
            set
            {
                _reportElement.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string Content
        {
            get
            {
                return _reportElement.Content;
            }
            set
            {
                _reportElement.Content = value;
                RaisePropertyChanged("Content");
            }
        }

        public int Order
        {
            get
            {
                return _reportElement.Order;
            }
            set
            {
                _reportElement.Order = value;
                RaisePropertyChanged("Order");
            }
        }

        public string X_as
        {
            get
            {
                return _reportElement.X_as;
            }
            set
            {
                _reportElement.X_as = value;
                RaisePropertyChanged("X_as");
            }
        }

        public string Y_as
        {
            get
            {
                return _reportElement.Y_as;
            }
            set
            {
                _reportElement.Y_as = value;
                RaisePropertyChanged("Y_as");
            }
        }

        public List<List<string>> Data 
        {
            get 
            {
                return _data;
            }
            set 
            {
                _data = value;
                RaisePropertyChanged("Data");
            }
        }

        public virtual IDataParser DataParser 
        { 
            get 
            {
                return _dataParser;
            }
            set 
            {
                _dataParser = value;
                RaisePropertyChanged("DataParser");
            }
        }

        public IQuestion SelectedSurveyQuestion 
        { 
            get 
            {
                return _selectedSurveyQuestion;
            }
            set 
            {
                _selectedSurveyQuestion = value;
                RaisePropertyChanged("SelectedSurveyQuestion");
            }
        }

        public ReportElementVM(ReportElement element)
        {
            _reportElement = element;
            DataParser = DataParserFactory.GetDataParserByJson(element.Data);
            
            DeleteElement = new RelayCommand(() => Delete());
            ElementUpCommand = new RelayCommand(MoveElementUp);
            ElementDownCommand = new RelayCommand(MoveElementDown);
        }

        public ReportElementVM()
        {
            DeleteElement = new RelayCommand(() => Delete());
            ElementUpCommand = new RelayCommand(MoveElementUp);
            ElementDownCommand = new RelayCommand(MoveElementDown);
            _reportElement = new ReportElement();
        }

        private void MoveElementUp()
        {
            ReportVM.MoveElement(this, -1);
        }

        private void MoveElementDown()
        {
            ReportVM.MoveElement(this, 1);
        }

        public void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u dit element wilt verwijderen?", "Element Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Attach(_reportElement);
                    context.ReportElements.Remove(_reportElement);
                    context.SaveChanges();

                    var newElementOrder = context.ReportElements.Where(r => r.ReportId == ReportVM.Id).OrderBy(e => e.Order).ToList();
                    int index = 1;
                    foreach (var item in newElementOrder)
                    {
                        item.Order = index;
                        index++;
                    }
                    context.SaveChanges();
                }
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ReportInfoVM>().RefreshElements();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Rapportelement verwijderd.");
            }
        }

        public ReportElement ToModel()
        {
            if (DataParser != null)
            {
                _reportElement.Data = DataParser.ToJson();
            }
            return _reportElement;
        }

        public Visibility VisibilityButtons
        {
            get
            {
                return _visibilityButtons;
            }
            set
            {
                _visibilityButtons = value;
                RaisePropertyChanged();
            }
        }

        #region Validation

        string IDataErrorInfo.Error => null;

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return GetValidationError(propertyName);
            }
        }

        private string ValidateTitle
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Title))
                {
                    return "Titel moet ingevuld zijn";
                }
                else if (Title.Length > 100)
                {
                    return "Titel mag niet langer zijn dan 100 karakters";
                }
                return null;
            }
        }

        private string ValidateContent
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Content))
                {
                    return "Beschrijving moet ingevuld zijn";
                }
                else if (Content.Length > 10000)
                {
                    return "Beschrijving mag niet langer zijn dan 10000 karakters";
                }
                return null;
            }
        }

        private string ValidateY_as
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Y_as))
                {
                    return "Y_as moet ingevuld zijn";
                }
                else if (Y_as.Length > 100)
                {
                    return "Y_as mag niet langer zijn dan 100 karakters";
                }
                return null;
            }
        }

        private string ValidateX_as
        {
            get
            {
                if (String.IsNullOrWhiteSpace(X_as))
                {
                    return "X_as moet ingevuld zijn";
                }
                else if (X_as.Length > 100)
                {
                    return "X_as mag niet langer zijn dan 100 karakters";
                }
                return null;
            }
        }

        private string Validate_image 
        {
            get 
            {
                if (Image == null || Image.Length < 0)
                {
                    return "Er is geen afbeelding ge-upload";
                }
                return null;
            }
        }

        string GetValidationError(string propertyName)
        {
            string error = null;
            switch (propertyName)
            {
                case "Title":
                    error = ValidateTitle;
                    break;
                case "Content":
                    error = ValidateContent;
                    break;
                case "Y_as":
                    error = ValidateY_as;
                    break;
                case "X_as":
                    error = ValidateX_as;
                    break;
                case "Image":
                    error = Validate_image;
                    break;
            }
            return error;
        }

        public static readonly string[] ValidatedProperties =
        {
            "Title", "Content", "Y_as", "X_as"
        };

        public static readonly string[] ValidatedPropertiesShort =
        {
            "Title", "Content"
        };

        public static readonly string[] ValidateImage = {
            "Image"
        };

        public bool IsValid
        {
            get
            {
                if (Type.Equals(ReportElementType.Barchart) || Type.Equals(ReportElementType.Linechart))
                {
                    foreach (var property in ValidatedProperties)
                    {
                        if (GetValidationError(property) != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    foreach (var property in ValidatedPropertiesShort)
                    {
                        if (GetValidationError(property) != null)
                        {
                            return false;
                        }
                    }
                    if (Type == ReportElementType.Image)
                    {
                        if (GetValidationError(ValidateImage[0]) != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
        #endregion
    }
}
