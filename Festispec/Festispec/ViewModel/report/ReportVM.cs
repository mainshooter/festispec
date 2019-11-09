using Festispec.Domain;
using Festispec.View.Report.Element;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.rapport.element;
using Festispec.ViewModel.report.element;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec.ViewModel.rapport
{
    public class ReportVM
    {
        private Report _report;

        public int Id {
            get {
                return _report.Id;
            }
            private set {
                _report.Id = value;
            }
        }

        public OrderVM Order { get; set; }

        public string Title {
            get {
                return _report.Title;
            }
            set {
                _report.Title = value;
            }
        }

        public string Status {
            get {
                return _report.Status;
            }
            set {
                _report.Status = value;
            }
        }

        public ObservableCollection<ReportElementVM> ReportElements { get; set; }
        private ObservableCollection<UserControl> _reportElementUserControlls;
        public ObservableCollection<UserControl> ReportElementUserControlls {
            get {
                return _reportElementUserControlls;
            }
            set {
                _reportElementUserControlls = value;
            }
        }

        public ReportVM(Report report)
        {
            _report = report;
            ReportElements = new ObservableCollection<ReportElementVM>(report.ReportElements.ToList().Select(e => new ReportElementVM(e)));
        }

        public ReportVM()
        {
            _report = new Report();
            test();
        }

        public void test()
        {
            ReportElementUserControlls = new ObservableCollection<UserControl>();
            Table table = new Table();
            TableVM tablevm = new TableVM();
            tablevm.Title = "Titeltje";
            tablevm.Content = "Lorem ipsum da set a mon";
            var newList = new List<string>();
            newList.Add("1");
            newList.Add("2");
            tablevm.Dictionary.Add("id", newList);
            tablevm.ApplyChanges();
            table.DataContext = tablevm;
            ReportElementUserControlls.Add(table);
        }
    }
}
