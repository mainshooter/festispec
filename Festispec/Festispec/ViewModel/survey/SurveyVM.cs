﻿using Festispec.ViewModel.employee.assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public AssignmentVM Assignment { get; set; }
        public string Status { get; set; }
    }
}
