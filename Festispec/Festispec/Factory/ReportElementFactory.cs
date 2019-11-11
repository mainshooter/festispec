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
                TableVM tableVm = new TableVM(element);
                table.DataContext = tableVm;
                returningUserControl = table;
            }
            else if (type.Equals("linechart"))
            {
                LineChartUserControl lineChart = new LineChartUserControl();
                LineChartVM lineChartVm = new LineChartVM(element);
                lineChart.DataContext = lineChartVm;
                returningUserControl = lineChart;
            }
            else if (type.Equals("piechart"))
            {
                PieChartUserControl pieChart = new PieChartUserControl();
                PieChartVM pieChartVm = new PieChartVM(element);
                
                pieChart.DataContext = pieChartVm;
                returningUserControl = pieChart;
            }
            return returningUserControl;
        }
    }
}
