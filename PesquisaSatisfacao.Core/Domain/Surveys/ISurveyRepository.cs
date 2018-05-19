using PesquisaSatisfacao.Core.Infrastructure.Database.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Core.Domain.Surveys
{
    public interface ISurveyRepository
    {
        Task Desactive(int id);
        Task<IList<Survey>> GetBy(DateTime? beginDate, DateTime? endDate);
        Task<IList<Survey>> GetAll();
        Task Add(Survey survey);
        Task AddCategory(QuestionCategory category);
        Task AddQuestion(Question question);
        Task AddQuestionAnswer(QuestionAnswer questionAnswer);
        Task<IList<Question>> GetQuestionsBy(int surveyId);
        Task<IList<QuestionCategory>> GetCategorys();
        Task<IList<QuestionDTO>> GetQuestionsBy(string surveyCode);
        Task<IList<Answer>> GetAnswers();
    }
}
