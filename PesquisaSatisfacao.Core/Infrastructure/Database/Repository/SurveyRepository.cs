using PesquisaSatisfacao.Core.Domain.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PesquisaSatisfacao.Core.Infrastructure.Database.DTO;

namespace PesquisaSatisfacao.Core.Infrastructure.Database.Repository
{
    public class SurveyRepository : BaseDB, ISurveyRepository
    {
        public async Task Add(Survey survey)
        {
            var sql = @"INSERT INTO Survey([Name], BeginDate, EndDate, Active, UserId, Code, CreatedOn) 
                                        VALUES(@Name, @BeginDate, @EndDate, @Active, @UserId, @Code, @CreatedOn)";
            await ExecuteAsync(sql, survey);
        }

        public async Task AddQuestion(Question question)
        {
            var sql = @"INSERT INTO Question(Description, SurveyId, CategoryId) VALUES(@Description, @SurveyId, @CategoryId)";
            await ExecuteAsync(sql, question);
        }

        public async Task AddCategory(QuestionCategory category)
        {
            var sql = @"INSERT INTO QuestionCategory([Name], UserId) VALUES(@Name, @UserId)";
            await ExecuteAsync(sql, category);
        }

        public async Task<IList<Survey>> GetAll(int userId)
        {
            var result = await QueryAsync<Survey>(@"SELECT * FROM Survey WHERE UserId = @userId", new { userId });
            return result.ToList();
        }

        public async Task<IList<QuestionCategory>> GetCategorys(int userId)
        {
            var result = await QueryAsync<QuestionCategory>(@"SELECT * FROM QuestionCategory WHERE UserId = @userId", new { userId });
            return result.ToList();
        }

        public async Task<IList<Question>> GetQuestionsBy(int surveyId)
        {
            var result = await QueryAsync<Question>(@"SELECT * FROM Question WHERE SurveyId = @surveyId", new { surveyId });
            return result.ToList();
        }

        public async Task<IList<Survey>> GetBy(int userId, DateTime? beginDate, DateTime? endDate)
        {
            var sql = @"SELECT * FROM Survey WHERE Active=1 AND UserId = @userId";

            if (beginDate.HasValue)
                sql += " AND BeginDate = @beginDate";
            if (endDate.HasValue)
                sql += " AND EndDate = @endDate";

            var result = await QueryAsync<Survey>(sql, new { userId, beginDate, endDate });
            return result.ToList();
        }

        public async Task Desactive(int id)
        {
            var sql = @"UPDATE Survey SET Active = 0 WHERE Id = @id";
            await ExecuteAsync(sql, new { id });
        }

        public async Task<IList<QuestionDTO>> GetQuestionsBy(string surveyCode)
        {
            var sql = @"SELECT Q.Id, Q.[Description], C.[Name] AS Category
			                    FROM Question Q
				                    INNER JOIN Survey S ON Q.SurveyId = S.Id
				                    INNER JOIN QuestionCategory C ON Q.CategoryId = C.Id
					                    WHERE S.Code = @surveyCode AND S.Active = 1";

            var result = await QueryAsync<QuestionDTO>(sql, new { surveyCode });

            return result.ToList();
        }

        public async Task<IList<Answer>> GetAnswers()
        {
            var result = await QueryAsync<Answer>("SELECT * FROM Answer");

            return result.ToList();
        }

        public async Task AddQuestionAnswer(QuestionAnswer questionAnswer)
        {
            var sql = @"INSERT INTO QuestionAnswer(QuestionId, AnswerId, CreatedOn) VALUES(@QuestionId, @AnswerId, @CreatedOn)";
            await ExecuteAsync(sql, questionAnswer);
        }

        public async Task<SurveyReportChartDTO> SurveyChartBy(int surveyId, int month, int answerId)
        {
            var sql = @"SELECT COUNT(QA.Id) AS Amount, (SELECT COUNT(QA.Id)
				                     FROM QuestionAnswer QA
					                    INNER JOIN Question Q ON QA.QuestionId = Q.Id
					                    INNER JOIN Survey S ON Q.SurveyId = S.Id
						                    WHERE MONTH(QA.CreatedOn) = @month AND S.Id = @surveyId) AS Total, MONTH(QA.CreatedOn) AS [Month]
		                    FROM QuestionAnswer QA
					                    INNER JOIN Question Q ON QA.QuestionId = Q.Id
					                    INNER JOIN Survey S ON Q.SurveyId = S.Id
						                    WHERE MONTH(QA.CreatedOn) = @month AND QA.AnswerId = @answerId AND S.Id = @surveyId
							                    GROUP BY MONTH(QA.CreatedOn)";

            var result = await QueryAsync<SurveyReportChartDTO>(sql, new { surveyId, month, answerId });

            return result.FirstOrDefault();
        }

        public async Task<ReportMainDTO> ReportMain(int surveyId, int categoryId)
        {
            var sql = @"SELECT  COUNT(QA.Id) AS Total,
	                            (SELECT COUNT(QA.Id)
		                                FROM QuestionAnswer QA
					                                INNER JOIN Question Q ON QA.QuestionId = Q.Id
					                                INNER JOIN Survey S ON Q.SurveyId = S.Id
						                                WHERE QA.AnswerId = 3 AND S.Id = @surveyId AND Q.CategoryId = @categoryId) AS Ruim,
			                            (SELECT COUNT(QA.Id)
				                            FROM QuestionAnswer QA
							                            INNER JOIN Question Q ON QA.QuestionId = Q.Id
							                            INNER JOIN Survey S ON Q.SurveyId = S.Id
								                            WHERE QA.AnswerId = 2 AND S.Id = @surveyId AND Q.CategoryId = @categoryId) AS Regular,
			                            (SELECT COUNT(QA.Id)
				                            FROM QuestionAnswer QA
							                            INNER JOIN Question Q ON QA.QuestionId = Q.Id
							                            INNER JOIN Survey S ON Q.SurveyId = S.Id
								                            WHERE QA.AnswerId = 1 AND S.Id = @surveyId AND Q.CategoryId = @categoryId) AS Bom
	                            FROM QuestionAnswer QA
			                            INNER JOIN Question Q ON QA.QuestionId = Q.Id
			                            INNER JOIN Survey S ON Q.SurveyId = S.Id
				                            WHERE S.Id = @surveyId AND S.Active = 1
					                            AND Q.CategoryId = @categoryId";

            var result = await QueryAsync<ReportMainDTO>(sql, new { surveyId, categoryId });

            return result.FirstOrDefault();
        }
    }
}
