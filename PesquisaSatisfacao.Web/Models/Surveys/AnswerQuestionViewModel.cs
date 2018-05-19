using PesquisaSatisfacao.Core.Domain.Surveys;
using PesquisaSatisfacao.Core.Infrastructure.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class AnswerQuestionViewModel
    {
        public IList<QuestionAnswer> QuestionAnswers { get; set; }

        public IList<QuestionDTO> Questions { get; set; }
        public IList<Answer> Answers { get; set; }
        public IList<string> Categorys => Questions != null ? Questions.Select(q => q.Category).Distinct().ToList() : new List<string>();
    }
}
