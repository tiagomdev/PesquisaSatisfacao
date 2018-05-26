using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Infrastructure.Database.DTO
{
    public class SurveyReportChartDTO
    {
        public int Amount { get; set; }
        public int Total { get; set; }
        public int Month { get; set; }

        public decimal Percent => Math.Round(((Convert.ToDecimal(Amount) / Convert.ToDecimal(Total)) * 100), 2);
    }
}
