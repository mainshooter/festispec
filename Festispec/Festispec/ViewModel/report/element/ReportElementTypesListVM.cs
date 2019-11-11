using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.report.element
{
    
    public class ReportElementTypesListVM
    {
        public List<string> ReportElementTypes { get; set; }
        public ReportElementTypesListVM()
        {
            ReportElementTypes = new List<string>();
            using (var context = new Entities())
            {
                var elementTypes = context.ElementTypes.ToList();
                foreach (var item in elementTypes)
                {
                    ReportElementTypes.Add(item.Type);
                }
            }
        }
    }
}
