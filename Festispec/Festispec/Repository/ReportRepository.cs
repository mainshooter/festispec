using Festispec.ViewModel.report.element;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;

namespace Festispec.Repository
{
    public class ReportRepository
    {
        public List<ReportElementVM> GetReportElements()
        {
            var reportElements = new List<ReportElementVM>();
            reportElements.Add(
                new ReportElementVM()
                {
                    Title = "text",
                    Content = "test text",
                    Type = "Text",
                    Data = new Dictionary<string, Object>()
                    {
                        ["text"] = "test text smiley"
                    }
                }
            );
            reportElements.Add(
                new ReportElementVM()
                {
                    Title = "image",
                    Content = "local image",
                    Type = "Image",
                    Data = new Dictionary<string, Object>()
                    {
                        ["image"] = new byte[0]
                    }
                }
            );
            return reportElements;
        }
    }
}
