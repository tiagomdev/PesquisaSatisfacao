using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Domain.Surveys
{
    public class Answer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Value { get; set; }

        public const int Bom = 1;
        public const int Regular = 2;
        public const int Ruim = 3;
    }
}
