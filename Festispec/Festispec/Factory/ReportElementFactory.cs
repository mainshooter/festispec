using Festispec.View.Report.Element;
using Festispec.ViewModel.rapport.element;
using Festispec.ViewModel.report.element;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec.Factory
{
    public class ReportElementFactory
    {

        public UserControl CreateElement(ReportElementVM element)
        {
            UserControl returningUserControl = null;
            string type = element.Type;
            if (type.Equals("table"))
            {
                TableUserControl table = new TableUserControl();
                TableVM tableVm = new TableVM();
                Dictionary<string, List<string>> tableData = (Dictionary<string, List<string>>) element.Data;
                tableVm.Title = element.Title;
                tableVm.Content = element.Content;
                tableVm.Order = element.Order;
                tableVm.Dictionary = tableData;
                tableVm.ApplyChanges();
                table.DataContext = tableVm;
                returningUserControl = table;
            }
            else if (type.Equals("linechart"))
            {
                LineChartUserControl lineChart = new LineChartUserControl();
                LineChartVM lineChartVm = new LineChartVM();
                Dictionary<string, Object> lineChartData = (Dictionary<string, Object>) element.Data;
                lineChartVm.Title = element.Title;
                lineChartVm.Content = element.Content;
                lineChartVm.Order = element.Order;
                lineChartVm.XaxisName = (string) lineChartData["xaxisName"];
                lineChartVm.YaxisName = (string) lineChartData["yaxisName"];
                lineChartVm.SeriesCollection = (SeriesCollection)lineChartData["seriescollection"];
                lineChart.DataContext = lineChartVm;
                returningUserControl = lineChart;
            }
            else if (type.Equals("piechart"))
            {
                PieChartUserControl pieChart = new PieChartUserControl();
                PieChartVM pieChartVm = new PieChartVM();
                pieChartVm.SeriesCollection = (SeriesCollection)element.Data;
                pieChart.DataContext = pieChartVm;
                returningUserControl = pieChart;
            }
            return returningUserControl;
        }
    }
}
