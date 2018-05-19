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

        public async Task<IList<Survey>> GetBy(DateTime? beginDate, DateTime? endDate)
        {
            return await repository.GetBy(beginDate, endDate);
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

        public async Task<IList<QuestionCategory>> GetCategorys() => await repository.GetCategorys();

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
    }
}
