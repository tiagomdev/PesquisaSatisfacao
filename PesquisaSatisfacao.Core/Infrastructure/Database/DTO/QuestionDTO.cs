using System;
using System.Collections.Generic;
using System.Text;

namespace PesquisaSatisfacao.Core.Infrastructure.Database.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
