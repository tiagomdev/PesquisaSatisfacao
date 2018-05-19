using PesquisaSatisfacao.Core.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class QuestionListViewModel
    {
        public IList<Question> Questions { get; set; }

        public int? SurveyId => Questions.FirstOrDefault()?.SurveyId;
    }
}
