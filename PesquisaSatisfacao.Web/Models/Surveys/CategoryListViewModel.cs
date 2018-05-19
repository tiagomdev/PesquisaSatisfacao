using PesquisaSatisfacao.Core.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class CategoryListViewModel
    {
        public IList<QuestionCategory> Categorys { get; set; }
    }
}
