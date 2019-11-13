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
using ImageUserControl = Festispec.View.Report.Element.ImageUserControl;

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
            else if (type.Equals("barchart"))
            {
                BarChartUserControl barChart = new BarChartUserControl();
                BarChartVM barChartVm = new BarChartVM(element);
                barChart.DataContext = barChartVm;
                returningUserControl = barChart;

            }
            else if (type.Equals("image"))
            {
                ImageUserControl image = new ImageUserControl();
                ImageVM imageVm = new ImageVM(element);
                image.DataContext = imageVm;
                returningUserControl = image;
            }
            else if (type.Equals("text"))
            {
                TextUserControl text = new TextUserControl();
                TextVM textVM = new TextVM(element);
                text.DataContext = textVM;
                returningUserControl = text;
            }
            return returningUserControl;
        }
    }
}
