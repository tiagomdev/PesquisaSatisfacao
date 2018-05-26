using PesquisaSatisfacao.Core.Domain.Surveys;
using PesquisaSatisfacao.Core.Infrastructure.Database.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Core.Application
{
    public class SurveyAppService
    {
        readonly ISurveyRepository repository;
        public SurveyAppService(ISurveyRepository repository)
        {
            this.repository = repository;
        }

        public async Task Add(Survey survey)
        {
            await repository.Add(survey);
        }

        public async Task<IList<Survey>> GetBy(int userId, DateTime? beginDate, DateTime? endDate)
        {
            return await repository.GetBy(userId, beginDate, endDate);
        }

        public async Task Desactive(int id)
        {
            if (id == default(int)) throw new ArgumentNullException(nameof(id));

            await repository.Desactive(id);
        }

        #region [ Question ]

        public async Task AddQuestion(Question question)
        {
            await repository.AddQuestion(question);
        }

        public async Task<IList<Question>> GetQuestionsBy(int surveyId)
        {
            if (surveyId == default(int)) throw new ArgumentNullException(nameof(surveyId));

            return await repository.GetQuestionsBy(surveyId);
        }

        public async Task<IList<QuestionDTO>> GetQuestionsBy(string surveyCode)
        {
            if (string.IsNullOrWhiteSpace(surveyCode)) throw new ArgumentNullException(nameof(surveyCode));

            return await repository.GetQuestionsBy(surveyCode);
        }

        #endregion

        #region [ Category ]

        public async Task AddCategory(QuestionCategory category)
        {
            await repository.AddCategory(category);
        }

        public async Task<IList<QuestionCategory>> GetCategorys(int userId) => await repository.GetCategorys(userId);

        #endregion

        public async Task<IList<Answer>> GetAnswers() => await repository.GetAnswers();
        public async Task AddQuestionAnswer(IList<QuestionAnswer> questionAnswers)
        {
            if (questionAnswers == null || questionAnswers.Count == 0) throw new ArgumentException("Respostas nao enviadas");

            foreach (var item in questionAnswers)
            {
                await repository.AddQuestionAnswer(item);
            }
        }

        public async Task<IList<SurveyReportChartDTO>> SurveyChartBy(int surveyId, int answerId)
        {
            if (surveyId == default(int)) throw new ArgumentNullException(nameof(surveyId));
            if (answerId == default(int)) throw new ArgumentNullException(nameof(answerId));

            var results = new List<SurveyReportChartDTO>();

            if(DateTime.Now.Month < 3)
            {
                var result = await repository.SurveyChartBy(surveyId, DateTime.Now.Month, answerId);
                if (result != null) results.Add(result);
            }
            else
            {
                for (var i = DateTime.Now.AddMonths(-3).Month; i <= DateTime.Now.Month; i++)
                {
                    var result = await repository.SurveyChartBy(surveyId, i, answerId);
                    if (result != null) results.Add(result);
                }
            }
            return results;
        }

        public async Task<ReportMainDTO> ReportMain(int surveyId, int categoryId)
        {
            if (surveyId == default(int)) throw new ArgumentNullException(nameof(surveyId));
            if (categoryId == default(int)) throw new ArgumentNullException(nameof(categoryId));

            return await repository.ReportMain(surveyId, categoryId);
        }
    }
}
