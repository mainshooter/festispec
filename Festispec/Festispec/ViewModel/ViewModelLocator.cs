using CommonServiceLocator;
using Festispec.ViewModel.report.element;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;

namespace Festispec.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LineChartVM>();
            SimpleIoc.Default.Register<PieChartVM>();
            SimpleIoc.Default.Register<TableVM>();
        }

        public LineChartVM LineChart {
            get {
                var chartElement = ServiceLocator.Current.GetInstance<LineChartVM>();
                chartElement.Title = "Test titel";
                chartElement.Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

                return chartElement;
            }
        }

        public TableVM Table {
            get {
                var table = ServiceLocator.Current.GetInstance<TableVM>();
                table.Title = "Titeltje";
                table.Content = "Lorem ipsum da set a mon";
                var newList = new List<string>();
                newList.Add("1");
                newList.Add("2");
                table.Dictionary.Add("id", newList);
                table.ApplyChanges();
                return table;
            }
        }


        public PieChartVM PieChart {
            get {
                var pieChart = ServiceLocator.Current.GetInstance<PieChartVM>();
                pieChart.SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Bier",
                    Values = new ChartValues<double> { 20 },
                    DataLabels = true,
                },
                new PieSeries
                {
                    Title = "Frisdrank",
                    Values = new ChartValues<double> { 12 },
                    DataLabels = true,
                },
                new PieSeries
                {
                    Title = "Cocktail",
                    Values = new ChartValues<double> { 8 },
                    DataLabels = true,
                },
                new PieSeries
                {
                    Title = "Wijn",
                    Values = new ChartValues<double> { 2 },
                    DataLabels = true,
                }
            };
                return pieChart;
            }
        }
        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
