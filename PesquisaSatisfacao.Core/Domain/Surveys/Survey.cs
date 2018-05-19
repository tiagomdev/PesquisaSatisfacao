using PesquisaSatisfacao.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Domain.Surveys
{
    public class Survey
    {
        public Survey()
        {

        }

        public Survey(string name, DateTime beginDate, DateTime endDate, int userId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Nome na enviado");
            if (beginDate == default(DateTime)) throw new ArgumentNullException("Data inicial na enviada");
            if (endDate == default(DateTime)) throw new ArgumentNullException("Data final na enviada");

            if (beginDate > endDate) throw new ArgumentException("Data inicial nao pode ser maior que a data final");

            Name = name;
            BeginDate = beginDate;
            EndDate = endDate;
            UserId = userId;
            Code = Guid.NewGuid().ToString();
            Active = true;
            CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public IList<Question> Questions { get; set; }
    }
}
