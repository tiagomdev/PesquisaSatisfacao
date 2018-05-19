using Microsoft.AspNetCore.Mvc;
using PesquisaSatisfacao.Core.Domain.Surveys;
using PesquisaSatisfacao.Web.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class SurveyListViewModel
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

        public IList<Survey> Surveys { get; set; }
    }
}
