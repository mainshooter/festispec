using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Festispec.Web.Models
{
    public class VerifyResetCodeModel
    {
        public string Code { get; set; }
    }
}