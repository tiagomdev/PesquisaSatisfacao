using PesquisaSatisfacao.Core.Domain.Surveys;
using PesquisaSatisfacao.Core.Infrastructure.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class ReportChartViewModel
    {
        public IList<SurveyReportChartDTO> Items { get; set; }

        public int SurveyId { get; set; }
        public IList<Survey> Surveys { get; set; }

        public int AnswerId { get; set; }
        public IList<Answer> Answers { get; set; }
    }
}
