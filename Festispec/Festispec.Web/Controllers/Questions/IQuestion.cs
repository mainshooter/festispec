using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Festispec.Domain;

namespace Festispec.Web.Controllers.Questions
{
    public interface IQuestion
    {
        PartialViewResult GetPartial();
    }
}
