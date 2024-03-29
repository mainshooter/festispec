﻿using Festispec.Domain;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.survey.answer;
using System.Collections.ObjectModel;
using System.Linq;

namespace Festispec.ViewModel.survey
{
    public class CaseVM
    {
        private Case _surveyCase;

        public int Id {
            get {
                return _surveyCase.Id;
            }
            private set {
                _surveyCase.Id = value;
            }
        }

        public EmployeeVM Employee { get; set; }

        public ObservableCollection<SurveyAnswerVM> Answers { get; set; }

        public CaseVM(Case surveyCase)
        {
            _surveyCase = surveyCase;
            Employee = new EmployeeVM(_surveyCase.Employee);
            Answers = new ObservableCollection<SurveyAnswerVM>(_surveyCase.Answers.ToList().Select(a => new SurveyAnswerVM(a)));
        }

        public CaseVM()
        {
            _surveyCase = new Case();
        }
    }
}
