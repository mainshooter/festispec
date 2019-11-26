using Festispec.View.Report.Element;
using Festispec.ViewModel.report.element;
using System.Windows.Controls;

namespace Festispec.Factory
{
    public class ReportElementFactory
    {
        public UserControl CreateElement(ReportElementVM element)
        {
            UserControl returningUserControl = null;
            string type = element.Type;
            if (type.Equals("Table"))
            {
                TableUserControl table = new TableUserControl();
                TableVM tableVm = new TableVM(element);
                tableVm.ApplyChanges();
                table.DataContext = tableVm;
                returningUserControl = table;
            }
            else if (type.Equals("Linechart"))
            {
                LineChartUserControl lineChart = new LineChartUserControl();
                LineChartVM lineChartVm = new LineChartVM(element);
                lineChartVm.ApplyChanges();
                lineChart.DataContext = lineChartVm;
                returningUserControl = lineChart;
            }
            else if (type.Equals("Piechart"))
            {
                PieChartUserControl pieChart = new PieChartUserControl();
                PieChartVM pieChartVm = new PieChartVM(element);
                pieChartVm.ApplyChanges();
                pieChart.DataContext = pieChartVm;
                returningUserControl = pieChart;
            }
            else if (type.Equals("Barchart"))
            {
                BarChartUserControl barChart = new BarChartUserControl();
                BarChartVM barChartVm = new BarChartVM(element);
                barChartVm.ApplyChanges();
                barChart.DataContext = barChartVm;
                returningUserControl = barChart;

            }
            else if (type.Equals("Image"))
            {
                ImageUserControl image = new ImageUserControl();
                ImageVM imageVm = new ImageVM(element);
                image.DataContext = imageVm;
                returningUserControl = image;
            }
            else if (type.Equals("Text"))
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
