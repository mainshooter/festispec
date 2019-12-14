using Festispec.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Festispec.Repository
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
