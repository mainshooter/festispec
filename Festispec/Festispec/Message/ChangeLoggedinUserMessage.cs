﻿using Festispec.ViewModel.employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Message
{
    public class ChangeLoggedinUserMessage
    {
        public EmployeeVM LoggedinEmployee { get; set; }
    }
}
