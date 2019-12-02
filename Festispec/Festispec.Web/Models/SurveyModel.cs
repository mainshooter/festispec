using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Festispec.Domain;
using Festispec.Web.Models.Questions;

namespace Festispec.Web.Models
{
    public class SurveyModel
    {
        public Survey Survey { get; set; }
        public ICollection<IQuestion> Questions { get; } = new List<IQuestion>();
    }
}