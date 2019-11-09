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
        public DataTable Data { get; set; }

        public TableVM()
        {
            Dictionary = new Dictionary<string, List<string>>();
        }

        public void ApplyChanges()
        {
            Data = new DataTable();

            foreach (var item in Dictionary)
            {
                Data.Columns.Add(item.Key);
            }

            foreach (var item in Dictionary)
            {
                foreach (var listItem in item.Value)
                {
                    Data.Rows.Add(listItem);
                }
            }
        }
    }
}
