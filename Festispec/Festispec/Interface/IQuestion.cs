﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey.question.questionTypes
{
    public interface IQuestion
    {
        void GetQuestion();
        void Save();
        void Delete();
        void Refresh();
    }
}
