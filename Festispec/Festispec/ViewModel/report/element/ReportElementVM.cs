using Festispec.Domain;
using Festispec.ViewModel.toast;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.report.element
{
    public class ReportElementVM : ViewModelBase, IDataErrorInfo
    
    {
        private ReportElement _reportElement;

        public ReportVM ReportVM { get; set; }

        public ICommand EditElement { get; set; }
        public ICommand DeleteElement { get; set; }
        public ICommand ElementUpCommand { get; set; }
        public ICommand ElementDownCommand { get; set; }

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

        public byte[] Image { 
            get {
                return _reportElement.Image;
            }
            set {
                _reportElement.Image = value;
                RaisePropertyChanged("Image");
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
            }
        }

        public virtual Object Data { get; set; }
        
        public ReportElementVM(ReportElement element, ReportVM report)
        {
            _reportElement = element;
            ReportVM = report;
            DeleteElement = new RelayCommand(() => Delete());
            ElementUpCommand = new RelayCommand(() => ReportVM.MoveElement(this, -1));
            ElementDownCommand = new RelayCommand(() => ReportVM.MoveElement(this, 1));
        }

        public ReportElementVM()
        {
            DeleteElement = new RelayCommand(() => Delete());
            ElementUpCommand = new RelayCommand(() => ReportVM.MoveElement(this, -1));
            ElementDownCommand = new RelayCommand(() => ReportVM.MoveElement(this, 1));
            _reportElement = new ReportElement();
        }

        public void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Weet u zeker dat u deze element wilt verwijderen?", "Element Verwijderen", MessageBoxButton.YesNo);
            if (result.Equals(MessageBoxResult.Yes))
            {
                using (var context = new Entities())
                {
                    context.ReportElements.Remove(context.ReportElements.Where(reportElement => reportElement.Id == _reportElement.Id).First());
                    context.SaveChanges();
                }
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ReportInfoVM>().RefreshElements();
                CommonServiceLocator.ServiceLocator.Current.GetInstance<ToastVM>().ShowSuccess("Rapportelement verwijderd.");
            }
        }

        public ReportElement ToModel()
        {
            return _reportElement;
        }

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
                else if (Content.Length > 100)
                {
                    return "Beschrijving mag niet langer zijn dan 100 karakters";
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

        public bool IsValid
        {
            get
            {
                if (Type.Equals("barchart")|| Type.Equals("linechart"))
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
                    return true;
                }
            }
        }
    }
}
