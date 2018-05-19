using PesquisaSatisfacao.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Domain.Surveys
{
    public class QuestionCategory
    {
        public QuestionCategory()
        {

        }

        public QuestionCategory(string name, int userId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome nao enviado");
            Name = name;
            UserId = userId;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
