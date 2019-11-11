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
                Table table = new Table();
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
                LineChart lineChart = new LineChart();
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
                PieChart pieChart = new PieChart();
                PieChartVM pieChartVm = new PieChartVM();
                pieChartVm.SeriesCollection = (SeriesCollection)element.Data;
                pieChart.DataContext = pieChartVm;
                returningUserControl = pieChart;
            }
            else if (type.Equals("barchart"))
            {
                BarChart barChart = new BarChart();
                BarChartVM barChartVm = new BarChartVM();
                Dictionary<string, Object> barChartData = (Dictionary<string, Object>) element.Data;
                barChartVm.Title = element.Title;
                barChartVm.Content = element.Content;
                barChartVm.Order = element.Order;
                barChartVm.Labels = (List<string>)barChartData["labels"];
                barChartVm.XaxisName = (string)barChartData["xaxisName"];
                barChartVm.YaxisName = (string)barChartData["yaxisName"];
                barChartVm.SeriesCollection = (SeriesCollection)barChartData["seriescollection"];
                barChart.DataContext = barChartVm;
                returningUserControl = barChart;

            }
            return returningUserControl;
        }
    }
}
