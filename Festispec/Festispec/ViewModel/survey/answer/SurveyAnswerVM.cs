﻿using Festispec.Domain;
using Festispec.ViewModel.survey.question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.survey.answer
{
    public class SurveyAnswerVM
    {
        public int Id {
            get {
                return _answer.Id;
            }
            private set {
                _answer.Id = value;
            }
        }
        public string Answer {
            get {
                return _answer.Answer1;
            }
            set {
                _answer.Answer1 = value;
            }
        }
        public SurveyQuestionVM Question { get; set; }
        private Answer _answer;

        public SurveyAnswerVM(Answer answer)
        {
            _answer = answer;
            Question = new SurveyQuestionVM(answer.Question);
        }

        public SurveyAnswerVM()
        {
            _answer = new Answer();
        }
    }
}
