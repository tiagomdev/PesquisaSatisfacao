using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Domain.Surveys
{
    public class QuestionAnswer
    {
        public QuestionAnswer()
        {
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public Question Question { get; set; }
        public int QuestionId { get; set; }

        public Answer Answer { get; set; }
        public int AnswerId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
