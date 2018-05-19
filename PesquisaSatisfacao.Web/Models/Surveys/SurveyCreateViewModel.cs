using Microsoft.AspNetCore.Mvc;
using PesquisaSatisfacao.Core.Domain.Surveys;
using PesquisaSatisfacao.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class SurveyCreateViewModel
    {
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public Survey ToDomain(int userId)
        {
            return new Survey(Name, BeginDate, EndDate, userId);
        }
    }
}
