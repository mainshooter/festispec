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
        public Dictionary<string, List<string>> Dictionary { get; set; }
        private DataTable _dataTable;
        public DataTable DataTable { 
            get {
                return _dataTable;
            }
            set {
                _dataTable = value;
                RaisePropertyChanged("DataTable");
            }
        }

        public TableVM()
        {
            DataTable = new DataTable();
            Dictionary = new Dictionary<string, List<string>>();
        }

        public void ApplyChanges()
        {
            
            foreach (var item in Dictionary)
            {
                DataTable.Columns.Add(item.Key);
            }

            foreach (var item in Dictionary)
            {
                foreach (var listItem in item.Value)
                {
                    DataTable.Rows.Add(listItem);
                }
            }
        }
    }
}
