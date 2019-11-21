using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Domain;

namespace Festispec.Web.Models.Questions
{
    public interface IQuestion
    {
        string RenderHtml();
    }
}
