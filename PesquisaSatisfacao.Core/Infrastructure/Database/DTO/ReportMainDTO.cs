using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Infrastructure.Database.DTO
{
    public class ReportMainDTO
    {
        public int Total { get; set; }
        public int Ruim { get; set; }
        public int Regular { get; set; }
        public int Bom { get; set; }
    }
}
