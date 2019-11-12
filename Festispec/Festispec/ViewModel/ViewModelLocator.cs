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
            SimpleIoc.Default.Register<BarChartVM>();
            SimpleIoc.Default.Register<TextVM>();
            SimpleIoc.Default.Register<ImageVM>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
