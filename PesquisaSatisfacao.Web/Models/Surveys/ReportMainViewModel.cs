using PesquisaSatisfacao.Core.Domain.Surveys;
using PesquisaSatisfacao.Core.Infrastructure.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class ReportMainViewModel
    {
        public ReportMainDTO Item { get; set; }

        public int CategoryId { get; set; }
        public IList<QuestionCategory> Categorys { get; set; }

        public int SurveyId { get; set; }
        public IList<Survey> Surveys { get; set; }
    }
}
