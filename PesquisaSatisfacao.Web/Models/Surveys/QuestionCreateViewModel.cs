using PesquisaSatisfacao.Core.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Models.Surveys
{
    public class QuestionCreateViewModel
    {
        public QuestionCreateViewModel()
        {

        }

        public QuestionCreateViewModel(int surveyId)
        {
            SurveyId = surveyId;
        }

        public int SurveyId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public IList<QuestionCategory> Categorys { get; set; }

        public Question ToDomain()
        {
            return new Question(Description, SurveyId, CategoryId);
        }
    }
}
