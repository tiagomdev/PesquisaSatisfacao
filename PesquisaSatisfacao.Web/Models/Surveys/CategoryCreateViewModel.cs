using PesquisaSatisfacao.Core.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class CategoryCreateViewModel
    {
        public string Name { get; set; }

        public QuestionCategory ToDomain(int userId)
        {
            return new QuestionCategory(Name, userId);
        }
    }
}
