using Festispec.ViewModel.rapport.element;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Festispec.ViewModel.report.element
{
    public class TableVM: ReportElementVM
    {
        private DataTable _dataTable;
        private Object _data;

        public Dictionary<string, List<string>> Dictionary { get; set; }
        
        public override Object Data {
            get {
                return _data;
            }
            set {
                _data = value;
                Dictionary = (Dictionary<string, List<string>>) _data;
                ApplyChanges();
            }
        }
        
        public DataTable DataTable { 
            get {
                return _dataTable;
            }
            set {
                _dataTable = value;
                RaisePropertyChanged("DataTable");
            }
        }

        public TableVM(ReportElementVM element)
        {
            DataTable = new DataTable();
            Dictionary = new Dictionary<string, List<string>>();
            Title = element.Title;
            Content = element.Content;
            Order = element.Order;
            Data = element.Data;
        }

        public void ApplyChanges()
        {
            foreach (var item in Dictionary)
            {
                DataTable.Columns.Add(item.Key);
            }

            int highestCount = 0;
            foreach (var item in Dictionary)
            {
                var listCount = item.Value.Count;
                if (listCount > highestCount)
                {
                    highestCount = listCount;
                }
            }

            for (int i = 0; i < highestCount; i++)
            {
                DataRow dataRow = DataTable.NewRow();
                int internIndex = 0;
                foreach (var item in Dictionary)
                {
                    var list = item.Value;
                    if (list.Count > i)
                    {
                        dataRow[internIndex] = list[i];
                    }
                    else
                    {
                        dataRow[internIndex] = "";
                    }
                    
                    internIndex++;
                }
                DataTable.Rows.Add(dataRow);
            }
        }
    }
}
