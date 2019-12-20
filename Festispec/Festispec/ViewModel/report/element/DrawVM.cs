using Festispec.Lib.Enums;
using Festispec.Message;
using Festispec.View.Pages.Report.element.Edit;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Festispec.ViewModel.report.element
{
    public class DrawVM : ReportElementVM
    {
        private byte[] _photo;
        private ObservableCollection<DrawPoint> _dotCollection;

        public DrawVM()
        {
            Type = ReportElementType.Draw;
        }

        public ObservableCollection<DrawPoint> DotCollection 
        {
            get
            {
                return _dotCollection;
            }
            set 
            {
                _dotCollection = value;
                RaisePropertyChanged("DotCollection");
            }
        }

        public byte[] Photo 
        {
            get 
            {
                return _photo;
            }
            set 
            {
                _photo = value;
                RaisePropertyChanged("Photo");
            }
        }

        public DrawVM(ReportElementVM element)
        {
            EditElement = new RelayCommand(() => Edit());
            Id = element.Id;
            Type = element.Type;
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            ReportId = element.ReportId;
            X_as = element.X_as;
            Y_as = element.Y_as;
            if (element.DataParser != null)
            {
                DataParser = element.DataParser;
                Data = DataParser.ParseData();
                Photo = DataParser.Question.QuestionDetails.Images[0];
            }
            DotCollection = new ObservableCollection<DrawPoint>();
        }

        public void Edit()
        {
            MessengerInstance.Send<ChangePageMessage>(new ChangePageMessage() { NextPageType = typeof(EditDrawPage) });
            MessengerInstance.Send<ChangeSelectedReportElementMessage>(new ChangeSelectedReportElementMessage()
            {
                ReportElementVM = this
            });
        }

        public void ApplyChanges()
        {
            try
            {
                DotCollection.Clear();
                foreach (var item in Data)
                {
                    var selectedData = item[0];
                    var ParsedItems = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(selectedData);
                    foreach (var ParsedItem in ParsedItems)
                    {
                        DotCollection.Add(
                            new DrawPoint() { XPos = ParsedItem["x"], YPos = ParsedItem["y"] }
                        );
                    }

                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
