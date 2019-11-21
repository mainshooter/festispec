using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Repository
{
    public class DataTypesRepository
    {
        public List<string> DataTypes { get; set; }

        public DataTypesRepository()
        {
            DataTypes = new List<string>()
            {
                "SELECT",
                "MIN",
                "HIGH",
                "AVG",
                "COUNT",
                "COUNT_OPTIONS"
            };
        }
    }
}
