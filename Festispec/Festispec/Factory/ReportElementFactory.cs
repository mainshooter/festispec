using Festispec.Lib.Enums;
using Festispec.View.Report.Element;
using Festispec.ViewModel.report;
using Festispec.ViewModel.report.element;
using System.Windows.Controls;

namespace Festispec.Factory
{
    public class ReportElementFactory
    {
        public static UserControl CreateElement(ReportElementVM element)
        {
            UserControl returningUserControl = null;
            string type = element.Type;
            if (type.Equals(ReportElementType.Table))
            {
                TableUserControl table = new TableUserControl();
                TableVM tableVm = new TableVM(element);
                tableVm.ApplyChanges();
                table.DataContext = tableVm;
                returningUserControl = table;
            }
            else if (type.Equals(ReportElementType.Linechart))
            {
                LineChartUserControl lineChart = new LineChartUserControl();
                LineChartVM lineChartVm = new LineChartVM(element);
                lineChartVm.ApplyChanges();
                lineChart.DataContext = lineChartVm;
                returningUserControl = lineChart;
            }
            else if (type.Equals(ReportElementType.Piechart))
            {
                PieChartUserControl pieChart = new PieChartUserControl();
                PieChartVM pieChartVm = new PieChartVM(element);
                pieChartVm.ApplyChanges();
                pieChart.DataContext = pieChartVm;
                returningUserControl = pieChart;
            }
            else if (type.Equals(ReportElementType.Barchart))
            {
                BarChartUserControl barChart = new BarChartUserControl();
                BarChartVM barChartVm = new BarChartVM(element);
                barChartVm.ApplyChanges();
                barChart.DataContext = barChartVm;
                returningUserControl = barChart;

            }
            else if (type.Equals(ReportElementType.Image))
            {
                ImageUserControl image = new ImageUserControl();
                ImageVM imageVm = new ImageVM(element);
                image.DataContext = imageVm;
                returningUserControl = image;
            }
            else if (type.Equals(ReportElementType.Text))
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
