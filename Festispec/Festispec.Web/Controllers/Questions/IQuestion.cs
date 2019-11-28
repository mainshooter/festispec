using System.Web.Mvc;

namespace Festispec.Web.Controllers.Questions
{
    public interface IQuestion
    {
        PartialViewResult GetPartial();
    }
}
