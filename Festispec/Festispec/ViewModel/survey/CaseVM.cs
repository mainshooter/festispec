using Festispec.Domain;
using Festispec.ViewModel.employee;
using Festispec.ViewModel.survey.answer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey
{
    public class CaseVM
    {
        private Case _surveyCase;
        public CaseVM(Case surveyCase)
        {
            _surveyCase = surveyCase;
            Employee = new EmployeeVM(_surveyCase.Employee);
            Survey = new SurveyVM(_surveyCase.Survey);
            Answers = new ObservableCollection<SurveyAnswerVM>(_surveyCase.Answers.ToList().Select(a => new SurveyAnswerVM(a)));
        }

        public CaseVM()
        {
            _surveyCase = new Case();
        }

        public int Id {
            get {
                return _surveyCase.Id;
            }
            private set {
                _surveyCase.Id = value;
            }
        }

        public SurveyVM Survey { get; set; }

        public EmployeeVM Employee { get; set; }

        public string Status {
            get {
                return _surveyCase.Status;
            }
            set {
                _surveyCase.Status = value;
            }
        }

        public ObservableCollection<SurveyAnswerVM> Answers { get; set; }
    }
}
