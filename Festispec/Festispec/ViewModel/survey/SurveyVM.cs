using Festispec.Domain;
using Festispec.ViewModel.employee.order;
using Festispec.ViewModel.survey.question;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey
{
    public class SurveyVM
    {
        public int Id {
            get {
                return _survey.Id;
            }
            private set {
                _survey.Id = value;
            }
        }
        public string Description {
            get {
                return _survey.Description;
            }
            set {
                _survey.Description = value;
            }
        }
        public OrderVM Order { get; set; }
        public string Status {
            get {
                return _survey.Status;
            }
            set {
                _survey.Status = value;
            }
        }

        private ObservableCollection<CaseVM> _cases;

        public ObservableCollection<CaseVM> Cases {
            get {
                return _cases;
            }
            set {
                _cases = value;
            }
        }

        private ObservableCollection<SurveyQuestionVM> _questions;
        public ObservableCollection<SurveyQuestionVM> Questions {
            get {
                return _questions;
            }
            set {
                _questions = value;
            }
        }


        private Survey _survey;
        public SurveyVM(Survey survey)
        {
            _survey = survey;
            Order = new OrderVM(survey.Order);
            Cases = new ObservableCollection<CaseVM>(survey.Cases.ToList().Select(c => new CaseVM(c)));
        }
    }
}
