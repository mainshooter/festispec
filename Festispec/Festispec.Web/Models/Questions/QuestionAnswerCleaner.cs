using Festispec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festispec.Web.Models.Questions
{
    public class QuestionAnswerCleaner
    {
        public QuestionAnswerCleaner()
        {

        }

        public Answer CleanAnswer(Question question, Answer answer)
        {
            switch (question.Type)
            {
                case "Tabel vraag":
                    return CleanTableAnswer(question, answer);
            }
            return answer;
        }

        private Answer CleanTableAnswer(Question question, Answer answer) {
            TableQuestionType tableQuestionType = new TableQuestionType(question);

            return answer;
        }
    }
}